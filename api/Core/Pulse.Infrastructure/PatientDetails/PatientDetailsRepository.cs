﻿using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Pulse.Domain.PatientDetails.Entities;
using Pulse.Infrastructure.Mongo;

namespace Pulse.Infrastructure.PatientDetails
{
    public class PatientDetailsRepository : IPatientDetailsRepository
    {
        public PatientDetailsRepository(IMongoDatabaseFactory factory)
        {
            this.Collection = factory
                .GetDatabase()
                .GetCollection<PatientDetail>("patientDetails");
        }

        private IMongoCollection<PatientDetail> Collection { get; }

        public Task<PatientDetail> GetOne(string id)
        {
            return this.Collection
                .Where(x => string.Equals(x.NhsNumber, id))
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PatientDetail>> GetAll()
        {
            return await this.Collection
                .FindAll()
                .ToListAsync();
        }

        public async Task<IEnumerable<PatientDetail>> Search(string criteria)
        {
            var filter = Builders<PatientDetail>.Filter.Text(criteria, new TextSearchOptions { CaseSensitive = false });
            var items = await this.Collection.FindAsync(filter);

            return await items.ToListAsync();
        }
    }
}