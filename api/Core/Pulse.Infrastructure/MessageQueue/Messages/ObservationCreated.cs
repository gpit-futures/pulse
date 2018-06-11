using Pulse.Infrastructure.Fhir.Models;
using RawRabbit.Attributes;
using RawRabbit.Configuration.Exchange;

namespace Pulse.Infrastructure.MessageQueue.Messages
{
    [Exchange(Type = ExchangeType.Direct, Name = "observation.exchange")]
    [Routing(RoutingKey = "message.created", NoAck = true)]
    [Queue(Name = "created-observation-queue", Durable = true)]
    public class ObservationCreated : Observation
    {
    }
}