using System.Collections.Generic;

namespace Pulse.Infrastructure.Fhir.Models.Primitives
{
    public class Meta
    {
        public long VersionId { get; set; }

        public string LastUpdated { get; set; }

        public IList<string> Profile { get; set; }
    }
}