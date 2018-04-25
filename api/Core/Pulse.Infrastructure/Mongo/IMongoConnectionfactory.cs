using MongoDB.Driver;

namespace Pulse.Infrastructure.Mongo
{
    public interface IMongoConnectionFactory
    {
        IMongoClient GetClient();
    }
}