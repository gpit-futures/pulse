using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pulse.Domain.EntryItems.Entities;

namespace Pulse.Infrastructure.EntryItems
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> GetAll(string patientId);

        Task<Contact> GetOne(Guid id);

        Task<Contact> GetOne(string patientId, string sourceId);

        Task AddOrUpdate(Contact item);
    }
}