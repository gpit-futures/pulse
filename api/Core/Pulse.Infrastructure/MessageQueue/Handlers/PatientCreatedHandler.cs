using System.Threading.Tasks;
using Pulse.Infrastructure.MessageQueue.Messages;

namespace Pulse.Infrastructure.MessageQueue.Handlers
{
    public class PatientCreatedHandler : IMessageHandler<PatientCreated>
    {
        public async Task Handle(PatientCreated message)
        {

        }
    }
}