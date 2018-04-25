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

        public async Task<IEnumerable<ClinicalNote>> GetAll(Guid patientId)
        {
            var all = this.Collection.Where(x => x.PatientId == patientId);

            return await all.ToListAsync();
        }

        public Task<ClinicalNote> GetOne(Guid id)
        {
            return this.Collection.GetOneAsync(id);
        }

        public Task AddOrUpdate(ClinicalNote item)
        {
            return this.Collection.AddOrUpdate(item);
        }
    }
}