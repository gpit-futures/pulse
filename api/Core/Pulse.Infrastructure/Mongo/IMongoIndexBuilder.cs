using System.Threading.Tasks;

namespace Pulse.Infrastructure.Mongo
{
    public interface IMongoIndexBuilder
    {
        Task Start();
    }
}