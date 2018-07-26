using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Pulse.Domain.Patients.Entities;
using Pulse.Infrastructure.Mongo;

namespace Pulse.Infrastructure.Patients
{
    public class PatientRepository : IPatientRepository
    {
        public PatientRepository(IMongoDatabaseFactory factory)
        {
            this.Collection = factory
                .GetDatabase()
                .GetCollection<Patient>("patients");
        }

        private IMongoCollection<Patient> Collection { get; }

        public async Task<IEnumerable<Patient>> GetAll()
        {
            return await this.Collection
                .FindAll()
                .ToListAsync();
        }

        public async Task<IEnumerable<Patient>> GetSome(int limit)
        {
            return await this.Collection
                .FindAll()
                .Limit(limit)
                .ToListAsync();
        }

        public Task<Patient> GetOne(string id)
        {
            return this.Collection
                .Where(x => string.Equals(x.NhsNumber, id))
                .FirstOrDefaultAsync();
        }

        public Task AddOrUpdate(Patient item)
        {
            return this.Collection.AddOrUpdate(item);
        }

        public async Task<IEnumerable<Patient>> Search(string criteria)
        {
            var filter = Builders<Patient>.Filter.Text(criteria, new TextSearchOptions {CaseSensitive = false});
            var items = await this.Collection.FindAsync(filter);

            return await items.ToListAsync();
        }
    }
}