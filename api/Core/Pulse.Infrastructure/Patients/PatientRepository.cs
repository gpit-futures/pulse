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
            var all = this.Collection.FindAll();

            return await all.ToListAsync();
        }

        public Task<Patient> GetOne(Guid id)
        {
            return this.Collection.GetOneAsync(id);
        }

        public Task AddOrUpdate(Patient item)
        {
            return this.Collection.AddOrUpdate(item);
        }
    }
}