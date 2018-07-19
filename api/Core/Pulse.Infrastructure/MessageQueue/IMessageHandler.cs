using System.Threading.Tasks;

namespace Pulse.Infrastructure.MessageQueue
{
    public interface IMessageHandler<in T> where T : class, IMessage
    {
        Task Handle(T message);
    }
}