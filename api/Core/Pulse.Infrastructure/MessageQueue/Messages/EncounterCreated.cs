using System.Collections.Generic;
using RawRabbit.Attributes;

namespace Pulse.Infrastructure.MessageQueue.Messages
{
    [Exchange(Name = "encounter.exchange")]
    [Routing(RoutingKey = "encounter.created", NoAck = true)]
    [Queue(Name = "created-encounter-queue", Durable = true)]
    public class EncounterCreated : IMessage
    {
        public string Source { get; set; }

        public string Destination { get; set; }

        public Dictionary<string, dynamic> Body { get; set; }
    }
}