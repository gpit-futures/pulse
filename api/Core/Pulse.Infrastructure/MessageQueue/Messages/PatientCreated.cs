using System.Collections.Generic;
using RawRabbit.Attributes;

namespace Pulse.Infrastructure.MessageQueue.Messages
{
    [Exchange(Name = "patient.exchange")]
    [Routing(RoutingKey = "patient.created", NoAck = true)]
    [Queue(Name = "created-patients-queue", Durable = true)]
    public class PatientCreated : IMessage
    {
        public string Source { get; set; }

        public string Destination { get; set; }

        public Dictionary<string, dynamic> Body { get; set; }
    }
}