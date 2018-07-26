using System.Collections.Generic;
using System.Threading.Tasks;
using Pulse.Domain.Patients.Entities;

namespace Pulse.Infrastructure.Search
{
    public interface ISearchService
    {
        Task<IEnumerable<Patient>> SearchPatient(string criteria);
    }
}