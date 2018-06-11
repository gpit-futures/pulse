using System.Collections.Generic;

namespace Pulse.Infrastructure.Fhir.Models.Primitives
{
    public class Name
    {
        public string Use { get; set; }

        public string Family { get; set; }

        public IList<string> Given { get; set; }

        public IList<string> Prefix { get; set; }
    }
}