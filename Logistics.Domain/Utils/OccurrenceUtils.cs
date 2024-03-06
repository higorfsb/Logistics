using Logistics.Domain.Dto.Occurrences;
using Logistics.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Domain.Utils
{
    public static class OccurrenceUtils
    {
        public static Ocorrencia AddOccurrenceMapper(OccurrenceRequest occurence)
        {
            return new Ocorrencia
            {
                TipoOcorrencia = occurence.TipoOcorrencia,
                HoraOcorrencia = occurence.HoraOcorrencia,
                IdPedido = occurence.IdPedido,
                IndFinalizadora = false
            };
        }

        public static Ocorrencia OccurrenceDeleteMapper(int id)
        {
            return new Ocorrencia
            {
                Id = id
            };
        }
    }
}
