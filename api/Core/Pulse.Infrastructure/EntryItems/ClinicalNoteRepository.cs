using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Pulse.Domain.EntryItems.Entities;
using Pulse.Infrastructure.Mongo;

namespace Pulse.Infrastructure.EntryItems
{
    public class ClinicalNoteRepository : IClinicalNoteRepository
    {
        public ClinicalNoteRepository(IMongoDatabaseFactory factory)
        {
            this.Collection = factory
                .GetDatabase()
                .GetCollection<ClinicalNote>("clinicalNotes");
        }

        private IMongoCollection<ClinicalNote> Collection { get; }

        public async Task<IEnumerable<ClinicalNote>> GetAll(string patientId)
        {
            var all = this.Collection
                .Where(x => x.PatientId == patientId);

            return await all.ToListAsync();
        }

        public Task<ClinicalNote> GetOne(Guid id)
        {
            return this.Collection.GetOneAsync(id);
        }

        public Task<ClinicalNote> GetOne(string patientId, string sourceId)
        {
            var note = this.Collection
                .Where(x => x.PatientId == patientId && string.Equals(x.SourceId, sourceId))
                .FirstOrDefaultAsync();

            return note;
        }

        public Task AddOrUpdate(ClinicalNote item)
        {
            return this.Collection.AddOrUpdate(item);
        }
    }
}