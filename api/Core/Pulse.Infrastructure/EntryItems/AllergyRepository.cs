using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Pulse.Domain.EntryItems.Entities;
using Pulse.Infrastructure.Mongo;

namespace Pulse.Infrastructure.EntryItems
{
    public class AllergyRepository : IAllergyRepository
    {
        public AllergyRepository(IMongoDatabaseFactory factory)
        {
            this.Collection = factory
                .GetDatabase()
                .GetCollection<Allergy>("allergies");
        }

        private IMongoCollection<Allergy> Collection { get; }

        public async Task<IEnumerable<Allergy>> GetAll(string patientId)
        {
            var all = this.Collection.Where(x => x.PatientId == patientId);

            return await all.ToListAsync();
        }

        public Task<Allergy> GetOne(Guid id)
        {
            return this.Collection.GetOneAsync(id);
        }

        public async Task<Allergy> GetOne(string patientId, string sourceId)
        {
            return await this.Collection
                .Where(x => x.PatientId == patientId && x.SourceId == sourceId)
                .FirstOrDefaultAsync();
        }

        public Task AddOrUpdate(Allergy item)
        {
            return this.Collection.AddOrUpdate(item);
        }
    }
}