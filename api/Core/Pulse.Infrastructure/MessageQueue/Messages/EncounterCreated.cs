using Pulse.Infrastructure.Fhir.Models;
using RawRabbit.Attributes;
using RawRabbit.Configuration.Exchange;

namespace Pulse.Infrastructure.MessageQueue.Messages
{
    [Exchange(Type = ExchangeType.Direct, Name = "encounter.exchange")]
    [Routing(RoutingKey = "encounter.created", NoAck = true)]
    [Queue(Name = "created-encounter-queue", Durable = true)]
    public class EncounterCreated : Encounter
    {
        
    }
}