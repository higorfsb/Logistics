using Logistics.Domain.Dto.Orders;
using Logistics.Domain.Entities;
using Logistics.Domain.Interfaces.Repositories;
using Logistics.Insfrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Logistics.Insfrastructure.Repositores
{
    public class OrderRepository : BaseRepository<Pedido>, IOrderRepository
    {
        public OrderRepository(LogisticsContext context) : base(context)
        {
        }
        public async Task<bool> CheckIfOrderExists(int id)
        {
            return await _context.Pedidos
                .AnyAsync(x => x.Id == id);
        }

        public async Task<OrderResponse> GetOrderById(int id)
        {
            return await _context.Pedidos
                .Where(x => x.Id == id)
                .Select(x => new OrderResponse
                {
                    NumeroPedido = x.NumeroPedido,
                    HoraPedido = x.HoraPedido,
                    IndCancelado = x.IndCancelado,
                    IndConcluido = x.IndConcluido
                }).FirstOrDefaultAsync();
        }

        public async Task<Pedido> GetOrderByIdObject(int id)
        {
            return await _context.Pedidos
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IList<OrdersResponse>> GetOrders()
        {
            return await _context.Pedidos
                .Select(x => new OrdersResponse
                {
                    Id = x.Id,
                    NumeroPedido = x.NumeroPedido,
                    HoraPedido = x.HoraPedido,
                    IndCancelado = x.IndCancelado,
                    IndConcluido = x.IndConcluido
                }).ToListAsync();
        }
    }
}
