namespace Logistics.Domain.Dto.Orders
{
    public class OrderResponse
    {
        public int? NumeroPedido { get; set; }
        public DateTime? HoraPedido { get; set; }
        public bool IndCancelado { get; set; }
        public bool IndConcluido { get; set; }
    }
}
