using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Pulse.Domain.EntryItems.Entities;
using Pulse.Infrastructure.Mongo;

namespace Pulse.Infrastructure.EntryItems
{
    public class MedicationRepository : IMedicationRepository
    {
        public MedicationRepository(IMongoDatabaseFactory factory)
        {
            this.Collection = factory
                .GetDatabase()
                .GetCollection<Medication>("medications");
        }

        private IMongoCollection<Medication> Collection { get; }

        public async Task<IEnumerable<Medication>> GetAll(string patientId)
        {
            var all = this.Collection.Where(x => x.PatientId == patientId);

            return await all.ToListAsync();
        }

        public Task<Medication> GetOne(Guid id)
        {
            return this.Collection.GetOneAsync(id);
        }

        public async Task<Medication> GetOne(string patientId, string sourceId)
        {
            return await this.Collection
                .Where(x => x.PatientId == patientId && x.SourceId == sourceId)
                .FirstOrDefaultAsync();
        }

        public Task AddOrUpdate(Medication item)
        {
            return this.Collection.AddOrUpdate(item);
        }
    }
}