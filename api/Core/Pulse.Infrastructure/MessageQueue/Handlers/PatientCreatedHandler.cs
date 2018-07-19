using Hl7.Fhir.Model;
using Pulse.Infrastructure.MessageQueue.Messages;
using Task = System.Threading.Tasks.Task;

namespace Pulse.Infrastructure.MessageQueue.Handlers
{
    public class PatientCreatedHandler : MessageHandlerBase<Patient>, IMessageHandler<PatientCreated>
    {
        public async Task Handle(PatientCreated message)
        {
        }
    }
}