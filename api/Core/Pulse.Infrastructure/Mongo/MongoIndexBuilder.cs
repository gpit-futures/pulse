using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Pulse.Domain.Patients.Entities;

namespace Pulse.Infrastructure.Mongo
{
    public class MongoIndexBuilder : IMongoIndexBuilder
    {
        public MongoIndexBuilder(IMongoDatabaseFactory factory)
        {
            this.Database = factory.GetDatabase();
        }

        private IMongoDatabase Database { get; }

        public async Task Start()
        {
            await this.Database
                .GetCollection<Patient>("patients")
                .Indexes
                .CreateOneAsync(Builders<Patient>.IndexKeys.Text(p => p.Name).Text(p => p.NhsNumber));
        }
    }
}