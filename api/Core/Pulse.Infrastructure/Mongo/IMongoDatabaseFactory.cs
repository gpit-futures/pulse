using MongoDB.Driver;

namespace Pulse.Infrastructure.Mongo
{
    public interface IMongoDatabaseFactory
    {
        IMongoDatabase GetDatabase();
    }
}