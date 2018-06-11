using System.Collections.Generic;

namespace Pulse.Infrastructure.Fhir.Models.Primitives
{
    public class Address
    {
        public string Use { get; set; }

        public string Type { get; set; }

        public IList<string> Line { get; set; }

        public string City { get; set; }

        public string District { get; set; }

        public string PostalCode { get; set; }
    }
}