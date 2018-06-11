using System.Threading.Tasks;

namespace Pulse.Infrastructure.MessageQueue
{
    public abstract class MessageHandler<T> : IMessageHandler<T> where T : class
    {
        public abstract Task Handle(T message);
    }
}