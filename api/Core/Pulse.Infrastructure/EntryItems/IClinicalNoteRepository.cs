using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pulse.Domain.EntryItems.Entities;

namespace Pulse.Infrastructure.EntryItems
{
    public interface IClinicalNoteRepository
    {
        Task<IEnumerable<ClinicalNote>> GetAll(string patientId);

        Task<ClinicalNote> GetOne(Guid id);

        Task<ClinicalNote> GetOne(string patientId, string sourceId);

        Task AddOrUpdate(ClinicalNote item);
    }
}