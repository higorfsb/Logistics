using Logistics.Domain.Constants;
using Logistics.Domain.Dto.Orders;
using Logistics.Domain.Entities;
using Logistics.Domain.Interfaces.Repositories;
using Logistics.Domain.Interfaces.Services;
using Logistics.Domain.Services;
using Logistics.Domain.Settings.ErrorHandler.ErrorStatusCode;
using LogisticsTests.Repositories;
using Moq;
using Xunit;

namespace LogisticsTests.Services
{
    public class OrderServiceTests
    {
        private readonly IOrderService _orderServices;
        private readonly Mock<IOrderRepository> _pedidoRepository;

        public OrderServiceTests()
        {
            _pedidoRepository = new Mock<IOrderRepository>();

            _orderServices = new OrderService(_pedidoRepository.Object);
        }
        [Fact]
        public async Task GetOrderById_WhenTheOrderIsFound_Success()
        {
            OrderResponse order = OrderRepositoryMock.GetOrderByIdMock();

            _pedidoRepository.Setup(x => x.GetOrderById(It.IsAny<int>()))
                .ReturnsAsync(order);

            OrderResponse response = await _orderServices
                .GetOrderById(It.IsAny<int>());

            Assert.Equal(order, response);
        }
        [Fact]
        public async Task GetOrderById_WhenOrderNotFound_Error()
        {

            Task act() => _orderServices.GetOrderById(It.IsAny<int>());

            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(act);

            Assert.Equal(ReturnMessageOrder.MessageOrderNotFound, exception.Errors[0]);
        }
        [Fact]
        public async Task GetOrder_WhenTheOrdersIsFound_Success()
        {
            IList<OrdersResponse> orders = OrderRepositoryMock.GetOrdersMock();

            _pedidoRepository.Setup(x => x.GetOrders())
                .ReturnsAsync(orders);

            IList<OrdersResponse> response = await _orderServices
                .GetOrders();

            Assert.Equal(orders.Count, response.Count);
            Assert.Equal(orders, response);
        }
        [Fact]
        public async Task GetOrders_WhenOrdersNotFound_Error()
        {
            _pedidoRepository.Setup(x => x.GetOrders())
            .ReturnsAsync(new List<OrdersResponse>());

            Task act() => _orderServices.GetOrders();

            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(act);

            Assert.Equal(ReturnMessageOrder.MessageOrdersNotFound, exception.Errors[0]);

        }
        [Fact]
        public async Task InsertOrder_WhenTheOrderIsInserted_Success()
        {
            InsertOrderRequest newOrder = OrderRepositoryMock.InsertOrderMock();

            _pedidoRepository.Setup(x => x.InsertAsync(It.IsAny<Pedido>()));

            await _orderServices.InsertOrder(newOrder);

            _pedidoRepository.Verify(x => x.InsertAsync(It.IsAny<Pedido>()), Times.Once);
        }
        [Fact]
        public async Task DeleteOrder_WhenTheOrderIsDeleted_Success()
        {
            Pedido order = OrderRepositoryMock.GetOrderByIdObjectMock();

            _pedidoRepository.Setup(x => x.GetOrderByIdObject(It.IsAny<int>()))
                .ReturnsAsync(order);

            _pedidoRepository.Setup(x => x.DeleteAsync(It.IsAny<Pedido>()));

            await _orderServices.DeleteOrder(It.IsAny<int>());

            _pedidoRepository.Verify(x => x.DeleteAsync(It.IsAny<Pedido>()));
        }
        [Fact]
        public async Task DeleteOrder_WhenOrderNotFound_Error()
        {
            Task act() => _orderServices.DeleteOrder(It.IsAny<int>());

            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(act);

            Assert.Equal(ReturnMessageOrder.MessageOrderNotFound, exception.Errors[0]);
        }
        [Fact]
        public async Task UpdateOrder_WhenOrderNotFound_Error()
        {
            Task act() => _orderServices.UpdateOrder(It.IsAny<UpdateOrderRequest>(), It.IsAny<int>());

            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(act);

            Assert.Equal(ReturnMessageOrder.MessageOrderNotFound, exception.Errors[0]);
        }
        [Fact]
        public async Task UpdateOrder_WhenTheOrderIsUpdated_Success()
        {
            UpdateOrderRequest orderRequest = OrderRepositoryMock.UpdatedOrder();

            Pedido order = OrderRepositoryMock.GetOrderByIdObjectMock();

            _pedidoRepository.Setup(x => x.GetOrderByIdObject(It.IsAny<int>()))
                .ReturnsAsync(order);

            _pedidoRepository.Setup(x => x.UpdateAsync(It.IsAny<Pedido>()));

            await _orderServices.UpdateOrder(orderRequest, It.IsAny<int>());

            _pedidoRepository.Verify(x => x.UpdateAsync(It.IsAny<Pedido>()));

            Assert.Equal(order.IndCancelado, orderRequest.IndCancelado);
            Assert.Equal(order.IndConcluido, orderRequest.IndConcluido);
        }
    }
}
