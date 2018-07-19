using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Newtonsoft.Json;

namespace Pulse.Infrastructure.MessageQueue
{
    public abstract class MessageHandlerBase<T> where T : Base
    {
        protected MessageHandlerBase()
        {
            this.Parser = new FhirJsonParser(new ParserSettings
            {
                AcceptUnknownMembers = true
            });
        }

        private FhirJsonParser Parser { get; }

        protected T ParseMessage(IMessage message)
        {
            var body = MessageHandlerBase<T>.IgnoreNulls(message.Body);
            return this.Parser.Parse<T>(JsonConvert.SerializeObject(body));
        }

        private static IDictionary<string, dynamic> IgnoreNulls(IDictionary<string, dynamic> body)
        {
            return body.Where(field => field.Value != null).ToDictionary(field => field.Key, field => field.Value);
        }
    }
}