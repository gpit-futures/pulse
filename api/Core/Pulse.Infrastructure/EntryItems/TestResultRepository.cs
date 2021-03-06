﻿using System;
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

        public async Task<IEnumerable<TestResult>> GetAll(string patientId)
        {
            var all = this.Collection.Where(x => x.PatientId == patientId);

            return await all.ToListAsync();
        }

        public Task<TestResult> GetOne(Guid id)
        {
            return this.Collection.GetOneAsync(id);
        }

        public async Task<TestResult> GetOne(string patientId, string sourceId)
        {
            return await this.Collection
                .Where(x => x.PatientId == patientId && x.SourceId == sourceId)
                .FirstOrDefaultAsync();
        }

        public Task AddOrUpdate(TestResult item)
        {
            return this.Collection.AddOrUpdate(item);
        }
    }
}