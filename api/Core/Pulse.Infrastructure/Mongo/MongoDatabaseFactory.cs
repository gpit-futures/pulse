using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Pulse.Infrastructure.Mongo
{
    public class MongoDatabaseFactory : IMongoDatabaseFactory
    {
        private const string DbConfigKey = "DbNames:Pulse";

        public MongoDatabaseFactory(IMongoConnectionFactory factory, IConfiguration config)
        {
            this.Client = factory.GetClient();
            var name = config[DbConfigKey];

            this.DatabaseName = name;
        }

        private IMongoClient Client { get; }

        private string DatabaseName { get; }

        public IMongoDatabase GetDatabase()
        {
            return this.Client.GetDatabase(this.DatabaseName);
        }
    }
}