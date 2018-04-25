using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pulse.Domain.Patients.Entities;

namespace Pulse.Infrastructure.Patients
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAll();

        Task<Patient> GetOne(Guid id);

        Task AddOrUpdate(Patient item);
    }
}