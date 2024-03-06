using Logistics.Domain.Entities.Base;

namespace Logistics.Domain.Entities
{
    public partial class Ocorrencia : BaseEntity
    {
        public int Id { get; set; }
        public string TipoOcorrencia { get; set; }
        public bool IndFinalizadora { get; set; }
        public DateTime HoraOcorrencia { get; set; }
        public int IdPedido { get; set; }

        public virtual Pedido IdPedidoNavigation { get; set; }
    }
}
