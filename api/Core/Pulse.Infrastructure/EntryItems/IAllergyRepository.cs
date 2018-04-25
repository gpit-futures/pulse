using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pulse.Domain.EntryItems.Entities;

namespace Pulse.Infrastructure.EntryItems
{
    public interface IAllergyRepository
    {
        Task<IEnumerable<Allergy>> GetAll(Guid patientId);

        Task<Allergy> GetOne(Guid id);

        Task AddOrUpdate(Allergy item);
    }
}