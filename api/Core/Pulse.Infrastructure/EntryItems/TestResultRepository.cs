using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Pulse.Domain.EntryItems.Entities;
using Pulse.Infrastructure.Mongo;

namespace Pulse.Infrastructure.EntryItems
{
    public class TestResultRepository : ITestResultRepository
    {
        public TestResultRepository(IMongoDatabaseFactory factory)
        {
            this.Collection = factory
                .GetDatabase()
                .GetCollection<TestResult>("testResults");
        }

        private IMongoCollection<TestResult> Collection { get; }

        public async Task<IEnumerable<TestResult>> GetAll(Guid patientId)
        {
            var all = this.Collection.Where(x => x.PatientId == patientId);

            return await all.ToListAsync();
        }

        public Task<TestResult> GetOne(Guid id)
        {
            return this.Collection.GetOneAsync(id);
        }

        public Task AddOrUpdate(TestResult item)
        {
            return this.Collection.AddOrUpdate(item);
        }
    }
}