using System.Threading.Tasks;
using MongoDB.Driver;
using Pulse.Domain.PatientDetails.Entities;

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
                .GetCollection<PatientDetail>("patientDetails")
                .Indexes
                .CreateOneAsync(Builders<PatientDetail>.IndexKeys
                    .Text(p => p.FirstName)
                    .Text(p => p.LastName)
                    .Text(p => p.NhsNumber));
        }
    }
}