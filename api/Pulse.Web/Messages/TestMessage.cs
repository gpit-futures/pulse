using RawRabbit.Attributes;
using RawRabbit.Configuration.Exchange;

namespace Pulse.Web.Messages
{
    [Exchange(Type = ExchangeType.Direct, Name = "encounter.exchange")]
    //[Routing(RoutingKey = "message.created", NoAck = true)]
    [Queue(Name = "created-encounter-queue", Durable = true)]
    public class TestMessage
    {
        public string Text = "HELP";
    }
}