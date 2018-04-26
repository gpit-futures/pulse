using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Pulse.Domain.EntryItems.Entities;
using Pulse.Infrastructure.Mongo;

namespace Pulse.Infrastructure.EntryItems
{
    public class ContactRepository :  IContactRepository
    {
        public ContactRepository(IMongoDatabaseFactory factory)
        {
            this.Collection = factory
                .GetDatabase()
                .GetCollection<Contact>("contacts");
        }

        private IMongoCollection<Contact> Collection { get; }

        public async Task<IEnumerable<Contact>> GetAll(string patientId)
        {
            return await this.Collection
                .Where(x => x.PatientId == patientId)
                .ToListAsync();
        }

        public Task<Contact> GetOne(Guid id)
        {
            return this.Collection.GetOneAsync(id);
        }

        public async Task<Contact> GetOne(string patientId, string sourceId)
        {
            return await this.Collection
                .Where(x => x.PatientId == patientId && x.SourceId == sourceId)
                .FirstOrDefaultAsync();
        }

        public Task AddOrUpdate(Contact item)
        {
            return this.Collection.AddOrUpdate(item);
        }
    }
}