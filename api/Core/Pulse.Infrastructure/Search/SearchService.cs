using System.Collections.Generic;
using System.Threading.Tasks;
using Pulse.Domain.Patients.Entities;
using Pulse.Infrastructure.PatientDetails;
using Pulse.Infrastructure.Patients;

namespace Pulse.Infrastructure.Search
{
    public class SearchService : ISearchService
    {
        public SearchService(IPatientRepository patients)
        {
            this.Patients = patients;
        }

        private IPatientRepository Patients { get; }

        public Task<IEnumerable<Patient>> SearchPatient(string criteria)
        {
            return this.Patients.Search(criteria);
        }
    }
}