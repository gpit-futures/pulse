using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pulse.Domain.EntryItems.Entities;

namespace Pulse.Infrastructure.EntryItems
{
    public interface IClinicalNoteRepository
    {
        Task<IEnumerable<ClinicalNote>> GetAll(Guid patientId);

        Task<ClinicalNote> GetOne(Guid id);

        Task AddOrUpdate(ClinicalNote item);
    }
}