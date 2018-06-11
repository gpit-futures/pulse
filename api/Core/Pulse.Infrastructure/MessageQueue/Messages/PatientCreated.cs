using Pulse.Infrastructure.Fhir.Models;
using RawRabbit.Attributes;
using RawRabbit.Configuration.Exchange;

namespace Pulse.Infrastructure.MessageQueue.Messages
{
    [Exchange(Type = ExchangeType.Direct, Name = "patient.exchange")]
    [Routing(RoutingKey = "patient.created", NoAck = true)]
    [Queue(Name = "created-patients-queue", Durable = true)]
    public class PatientCreated : Patient
    {
    }
}