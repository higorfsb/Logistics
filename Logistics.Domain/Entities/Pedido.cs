using Logistics.Domain.Entities.Base;

namespace Logistics.Domain.Entities
{
    public partial class Pedido : BaseEntity
    {
        public Pedido()
        {
            Ocorrencia = new HashSet<Ocorrencia>();
        }

        public int Id { get; set; }
        public int? NumeroPedido { get; set; }
        public DateTime? HoraPedido { get; set; }
        public bool IndCancelado { get; set; }
        public bool IndConcluido { get; set; }

        public virtual ICollection<Ocorrencia> Ocorrencia { get; set; }
    }
}
