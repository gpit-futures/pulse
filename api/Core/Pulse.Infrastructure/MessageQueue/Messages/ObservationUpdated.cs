using Pulse.Infrastructure.Fhir.Models;
using RawRabbit.Attributes;
using RawRabbit.Configuration.Exchange;

namespace Pulse.Infrastructure.MessageQueue.Messages
{
    [Exchange(Type = ExchangeType.Direct, Name = "observation.exchange")]
    [Routing(RoutingKey = "observation.updated", NoAck = true)]
    [Queue(Name = "updated-observation-queue", Durable = true)]
    public class ObservationUpdated : Observation
    {
    }
}