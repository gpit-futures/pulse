using System.Collections.Generic;
using Pulse.Infrastructure.Fhir.Models.Primitives;

namespace Pulse.Infrastructure.Fhir.Models
{
    public class BaseModel
    {
        public string ResourceType { get; set; }

        public string Id { get; set; }

        public Text Text { get; set; }

        public Meta Meta { get; set; }

        public IList<Issue> Issue { get; set; }

        public IList<Identifier> Identifier { get; set; }
    }
}