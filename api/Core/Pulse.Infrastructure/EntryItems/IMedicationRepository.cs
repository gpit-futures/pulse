using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pulse.Domain.EntryItems.Entities;

namespace Pulse.Infrastructure.EntryItems
{
    public interface IMedicationRepository
    {
        Task<IEnumerable<Medication>> GetAll(string patientId);

        Task<Medication> GetOne(Guid id);

        Task<Medication> GetOne(string patientId, string sourceId);

        Task AddOrUpdate(Medication item);
    }
}