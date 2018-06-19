using System.Collections.Generic;
using Pulse.Infrastructure.Fhir.Models.Primitives;

namespace Pulse.Infrastructure.Fhir.Models
{
    public class Observation : BaseModel
    {
        public CodeableConcept Code { get; set; }

        public ReferencedConcept Subject { get; set; }

        public ReferencedConcept Context { get; set; }

        public string Status { get; set; }

        public IList<Category> Category { get; set; } = new List<Category>();

        public string EffectiveDateTime { get; set; }

        public IList<Performer> Performer { get; set; } = new List<Performer>();

        public ValueQuantity ValueQuantity { get; set; }

        public Interpretation Interpretation { get; set; }

        public IList<ReferenceRange> ReferenceRange { get; set; } = new List<ReferenceRange>();
    }
}