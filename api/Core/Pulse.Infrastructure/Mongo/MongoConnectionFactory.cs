using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Pulse.Infrastructure.Mongo
{
    public class MongoConnectionFactory : IMongoConnectionFactory
    {
        private const string DbConnectionKey = "Mongo";

        public MongoConnectionFactory(IConfiguration config)
        {
            var connection = config.GetConnectionString(DbConnectionKey);

            this.Client = new MongoClient(connection);
        }

        private IMongoClient Client { get; }

        public IMongoClient GetClient()
        {
            return this.Client;
        }
    }
}