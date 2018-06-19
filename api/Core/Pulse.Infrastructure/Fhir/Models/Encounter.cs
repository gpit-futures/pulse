using System.Collections.Generic;
using Pulse.Infrastructure.Fhir.Models.Primitives;

namespace Pulse.Infrastructure.Fhir.Models
{
    public class Encounter : BaseModel
    {
        public string Status { get; set; }

        public ReferencedConcept Subject { get; set; }

        public IList<ReferencedConcept> Participant { get; set; } = new List<ReferencedConcept>();

        public Period Period { get; set; }

        public IList<CodeableConcept> Reason { get; set; } = new List<CodeableConcept>();

        public IList<ReferencedConcept> Diagnosis { get; set; } = new List<ReferencedConcept>();
    }
}