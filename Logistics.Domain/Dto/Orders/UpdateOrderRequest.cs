namespace Logistics.Domain.Dto.Orders
{
    public class UpdateOrderRequest
    {
        public bool IndCancelado { get; set; }
        public bool IndConcluido { get; set; }
    }
}
