using System.Threading.Tasks;

namespace Pulse.Infrastructure.MessageQueue
{
    public interface IMessageHandler<in T> where T : class
    {
        Task Handle(T message);
    }
}