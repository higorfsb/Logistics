using Logistics.Domain.Dto.Occurrences;
using Logistics.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Domain.Interfaces.Repositories
{
    public interface IOccurrenceRepository : IBaseRepository<Ocorrencia>
    {
        Task<OccurrenceResponse> GetOccurrenceById(int id);
        Task<IList<OccurrencesResponse>> GetOccurrences();
        Task<Ocorrencia>GetOccurrenceByType(string occurrenceType);
        Task<Ocorrencia> GetOccurrenceByIdObject(int id);
        Task<Ocorrencia> GetOccurrenceByIdOrder(int id);
    }
}
