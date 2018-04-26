using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pulse.Domain.EntryItems.Entities;

namespace Pulse.Infrastructure.EntryItems
{
    public interface IDiagnosisRepository
    {
        Task<IEnumerable<Diagnosis>> GetAll(string patientId);

        Task<Diagnosis> GetOne(Guid id);

        Task<Diagnosis> GetOne(string patientId, string sourceId);

        Task AddOrUpdate(Diagnosis item);
    }
}