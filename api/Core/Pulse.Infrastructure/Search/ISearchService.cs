using System.Collections.Generic;
using System.Threading.Tasks;
using Pulse.Domain.PatientDetails.Entities;

namespace Pulse.Infrastructure.Search
{
    public interface ISearchService
    {
        Task<IEnumerable<PatientDetail>> SearchPatient(string criteria);
    }
}