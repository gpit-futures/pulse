using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pulse.Domain.EntryItems.Entities;

namespace Pulse.Infrastructure.EntryItems
{
    public interface IMedicationRepository
    {
        Task<IEnumerable<Medication>> GetAll(Guid patientId);

        Task<Medication> GetOne(Guid id);

        Task AddOrUpdate(Medication item);
    }
}