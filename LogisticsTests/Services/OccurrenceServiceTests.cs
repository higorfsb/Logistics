using Logistics.Domain.Constants;
using Logistics.Domain.Dto.Occurrences;
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
    public class OccurrenceServiceTests
    {
        private readonly Mock<IOccurrenceRepository> _ocorrenciaRepository;
        private readonly Mock<IOrderRepository> _pedidoRepository;
        private readonly IOccurrenceService _ocorrenciaService;
        public OccurrenceServiceTests()
        {
            _ocorrenciaRepository = new Mock<IOccurrenceRepository>();
            _pedidoRepository = new Mock<IOrderRepository>();
            _ocorrenciaService = new OccurrenceService(_ocorrenciaRepository.Object,
                                                        _pedidoRepository.Object);
        }
        [Fact]
        public async Task GetOccurenceById_WhenTheOccurrenceIsFound_Success()
        {
            OccurrenceResponse occurrence = OccurrenceRepositoryMock.GetOccurrenceByIdMock();

            _ocorrenciaRepository.Setup(x => x.GetOccurrenceById(It.IsAny<int>()))
                .ReturnsAsync(occurrence);

            OccurrenceResponse response = await _ocorrenciaService
                .GetOccurrenceById(It.IsAny<int>());

            Assert.Equal(occurrence, response);
        }
        [Fact]
        public async Task GetOccurenceById_WhenOrderNotFound_Error()
        {
            Task act() => _ocorrenciaService.GetOccurrenceById(It.IsAny<int>());

            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(act);

            Assert.Equal(ReturnMessageOccurrence.MessageOccurenceNotFound, exception.Errors[0]);
        }
        [Fact]
        public async Task GetOccurences_WhenTheOccurrencesIsFound_Success()
        {
            IList<OccurrencesResponse> occurrences = OccurrenceRepositoryMock.GetOccurrencesMock();

            _ocorrenciaRepository.Setup(x => x.GetOccurrences())
                .ReturnsAsync(occurrences);

            IList<OccurrencesResponse> response = await _ocorrenciaService
                .GetOccurrences();

            Assert.Equal(occurrences.Count, response.Count);
            Assert.Equal(occurrences, response);
        }
        [Fact]
        public async Task GetOccurences_WhenOccurrencesNotFound_Error()
        {
            _ocorrenciaRepository.Setup(x => x.GetOccurrences())
           .ReturnsAsync(new List<OccurrencesResponse>());

            Task act() => _ocorrenciaService.GetOccurrenceById(It.IsAny<int>());

            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(act);

            Assert.Equal(ReturnMessageOccurrence.MessageOccurenceNotFound, exception.Errors[0]);
        }
        [Fact]
        public async Task InsertOccurrence_WhenOrderIsNotFound_Error()
        {
            Task act() => _ocorrenciaService.InsertOccurrence(new OccurrenceRequest());

            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(act);

            Assert.Equal(ReturnMessageOrder.MessageOrderNotFound, exception.Errors[0]);
        }
        [Fact]
        public async Task InsertOccurence_WhenOccurrenceIsOfTheSameTypeAndIsLessThan10Minutes_Error()
        {
            bool occurrence = OrderRepositoryMock.CheckIfOrderExistsMock();

            _pedidoRepository.Setup(x => x.CheckIfOrderExists(It.IsAny<int>()))
                .ReturnsAsync(occurrence);

            OccurrenceRequest newOccurence = OccurrenceRepositoryMock.RequestOccurrenceMock();

            Ocorrencia typeOccurrence = OccurrenceRepositoryMock.GetOccurrenceByTypeMock();

            _ocorrenciaRepository.Setup(x => x.GetOccurrenceByType(It.IsAny<string>()))
                .ReturnsAsync(typeOccurrence);

            Task act() => _ocorrenciaService.InsertOccurrence(newOccurence);

            BadRequestException exception = await Assert.ThrowsAsync<BadRequestException>(act);
            Assert.Equal(ReturnMessageOccurrence.MessageOccurenceType, exception.Errors[0]);
        }
        [Fact]
        public async Task InsertOccurence_WhenOccurrenceIsFinisherIsStatusCompleted_Sucess()
        {
            OccurrenceRequest newOccurence = OccurrenceRepositoryMock.InsertRequestOccurrenceMock();

            bool order = OrderRepositoryMock.CheckIfOrderExistsMock();

            _pedidoRepository.Setup(x => x.CheckIfOrderExists(It.IsAny<int>()))
               .ReturnsAsync(order);

            Ocorrencia typeOccurrence = OccurrenceRepositoryMock.GetOccurrenceByTypeMock();

            _ocorrenciaRepository.Setup(x => x.GetOccurrenceByType(It.IsAny<string>()))
                .ReturnsAsync(typeOccurrence);


            Ocorrencia occurrence = OccurrenceRepositoryMock.GetOccurrenceByIdOrderMock();

            _ocorrenciaRepository.Setup(x => x.GetOccurrenceByIdOrder(It.IsAny<int>()))
                .ReturnsAsync(occurrence);

            await _ocorrenciaService.InsertOccurrence(newOccurence);

            _ocorrenciaRepository.Verify(x => x.InsertAsync(It.IsAny<Ocorrencia>()));

            _pedidoRepository.Verify(x => x.UpdateAsync(It.IsAny<Pedido>()));
        }
        [Fact]
        public async Task DeleteOccurence_WhenTheOccurrenceNotFound_Error()
        {

            Task act() => _ocorrenciaService.DeleteOccurrence(It.IsAny<int>());

            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(act);
            Assert.Equal(ReturnMessageOccurrence.MessageOccurenceNotFound, exception.Errors[0]);

        }
        [Fact]
        public async Task DeleteOccurence_WhenTheOrderstatusIsIncompleted_Error()
        {
            Ocorrencia occurrence = OccurrenceRepositoryMock.GetOccurrenceByIdObjectMock();
            _ocorrenciaRepository.Setup(x => x.GetOccurrenceByIdObject(It.IsAny<int>()))
                .ReturnsAsync(occurrence);

            OrderResponse order = OrderRepositoryMock.GetOrderByIdIsIncompletedMock();
            _pedidoRepository.Setup(x => x.GetOrderById(It.IsAny<int>()))
                .ReturnsAsync(order);

            Task act() => _ocorrenciaService.DeleteOccurrence(It.IsAny<int>());

            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(act);
            Assert.Equal(ReturnMessageOccurrence.MessageOccurrenceStatus, exception.Errors[0]);

        }
        [Fact]
        public async Task DeleteOccurence_WhenOccurenceIsDeleted_Sucess()
        {
            Ocorrencia occurrence = OccurrenceRepositoryMock.GetOccurrenceByIdObjectMock();

            _ocorrenciaRepository.Setup(x => x.GetOccurrenceByIdObject(It.IsAny<int>()))
                .ReturnsAsync(occurrence);

            OrderResponse order = OrderRepositoryMock.GetOrderByIdMock();
            _pedidoRepository.Setup(x => x.GetOrderById(It.IsAny<int>()))
             .ReturnsAsync(order);

            await _ocorrenciaService.DeleteOccurrence(It.IsAny<int>());

            _ocorrenciaRepository.Verify(x => x.DeleteAsync(It.IsAny<Ocorrencia>()));
        }
        [Fact]
        public async Task UpdateOccurence_WhenOccurenceIsUpdated_Sucess()
        {
            UpdateOccurrenceRequest updateOccurrence = OccurrenceRepositoryMock.UpdatedOccurrenceRequest();
            bool order = OrderRepositoryMock.CheckIfOrderExistsMock();

            _pedidoRepository.Setup(x => x.CheckIfOrderExists(It.IsAny<int>()))
               .ReturnsAsync(order);
            Ocorrencia occurrence = OccurrenceRepositoryMock.GetOccurrenceByIdObjectMock();

            _ocorrenciaRepository.Setup(x => x.GetOccurrenceByIdObject(It.IsAny<int>()))
                .ReturnsAsync(occurrence);

            Ocorrencia typeOccurrence = OccurrenceRepositoryMock.GetOccurrenceByTypeMock();

            _ocorrenciaRepository.Setup(x => x.GetOccurrenceByType(It.IsAny<string>()))
                .ReturnsAsync(typeOccurrence);

            await _ocorrenciaService.UpdateOccurrence(updateOccurrence, It.IsAny<int>());

            Assert.Equal(updateOccurrence.TipoOcorrencia, occurrence.TipoOcorrencia);
        }
    }
}