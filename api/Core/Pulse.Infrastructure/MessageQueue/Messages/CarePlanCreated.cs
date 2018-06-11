using Pulse.Infrastructure.Fhir.Models;
using RawRabbit.Attributes;
using RawRabbit.Configuration.Exchange;

namespace Pulse.Infrastructure.MessageQueue.Messages
{
    [Exchange(Type = ExchangeType.Direct, Name = "care.plan.exchange")]
    [Routing(RoutingKey = "care.plan.created", NoAck = true)]
    [Queue(Name = "created-care-plan-queue", Durable = true)]
    public class CarePlanCreated : CarePlan
    {
    }
}