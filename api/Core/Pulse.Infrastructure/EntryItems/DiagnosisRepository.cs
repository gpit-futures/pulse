using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Pulse.Domain.EntryItems.Entities;
using Pulse.Infrastructure.Mongo;

namespace Pulse.Infrastructure.EntryItems
{
    public class DiagnosisRepository : IDiagnosisRepository
    {
        public DiagnosisRepository(IMongoDatabaseFactory factory)
        {
            this.Collection = factory
                .GetDatabase()
                .GetCollection<Diagnosis>("diagnoses");
        }

        private IMongoCollection<Diagnosis> Collection { get; }

        public async Task<IEnumerable<Diagnosis>> GetAll(string patientId)
        {
            var all = this.Collection.Where(x => x.PatientId == patientId);

            return await all.ToListAsync();
        }

        public Task<Diagnosis> GetOne(Guid id)
        {
            return this.Collection.GetOneAsync(id);
        }

        public async Task<Diagnosis> GetOne(string patientId, string sourceId)
        {
            return await this.Collection
                .Where(x => x.PatientId == patientId && x.SourceId == sourceId)
                .FirstOrDefaultAsync();
        }

        public Task AddOrUpdate(Diagnosis item)
        {
            return this.Collection.AddOrUpdate(item);
        }
    }
}