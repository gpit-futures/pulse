using System.Collections.Generic;
using System.Threading.Tasks;
using Pulse.Domain.PatientDetails.Entities;

namespace Pulse.Infrastructure.PatientDetails
{
    public interface IPatientDetailsRepository
    {
        Task<PatientDetail> GetOne(string id);

        Task<IEnumerable<PatientDetail>> GetAll();

        Task<IEnumerable<PatientDetail>> Search(string criteria);
    }
}