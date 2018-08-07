using System.Collections.Generic;
using System.Threading.Tasks;
using Pulse.Domain.PatientDetails.Entities;
using Pulse.Infrastructure.PatientDetails;

namespace Pulse.Infrastructure.Search
{
    public class SearchService : ISearchService
    {
        public SearchService(IPatientDetailsRepository patients)
        {
            this.Patients = patients;
        }

        private IPatientDetailsRepository Patients { get; }

        public Task<IEnumerable<PatientDetail>> SearchPatient(string criteria)
        {
            return this.Patients.Search(criteria);
        }
    }
}