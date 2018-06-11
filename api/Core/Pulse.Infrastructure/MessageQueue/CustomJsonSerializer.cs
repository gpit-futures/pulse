using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RawRabbit.Serialization;

namespace Pulse.Infrastructure.MessageQueue
{
    public class CustomJsonSerializer : JsonMessageSerializer
    {
        public CustomJsonSerializer(JsonSerializer serializer, Action<JsonSerializer> config) : base(new JsonSerializer
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        }, config)
        {
        }
    }
}