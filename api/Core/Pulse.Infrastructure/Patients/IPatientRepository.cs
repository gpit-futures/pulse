using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pulse.Domain.Patients.Entities;

namespace Pulse.Infrastructure.Patients
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAll();

        Task<IEnumerable<Patient>> GetSome(int limit);

        Task<Patient> GetOne(string id);

        Task AddOrUpdate(Patient item);

        Task<IEnumerable<Patient>> Search(string criteria);
    }
}