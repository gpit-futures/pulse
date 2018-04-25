using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pulse.Domain.EntryItems.Entities;

namespace Pulse.Infrastructure.EntryItems
{
    public interface IDiagnosisRepository
    {
        Task<IEnumerable<Diagnosis>> GetAll(Guid patientId);

        Task<Diagnosis> GetOne(Guid id);

        Task AddOrUpdate(Diagnosis item);
    }
}