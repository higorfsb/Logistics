namespace Logistics.Domain.Dto.Occurrences
{
    public class OccurrenceRequest
    {
        public string TipoOcorrencia { get; set; }
        public DateTime HoraOcorrencia { get; set; }
        public int IdPedido { get; set; }
    }
}
