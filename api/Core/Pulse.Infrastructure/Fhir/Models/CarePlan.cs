using System.Collections.Generic;
using Pulse.Infrastructure.Fhir.Models.Primitives;

namespace Pulse.Infrastructure.Fhir.Models
{
    public class CarePlan : BaseModel
    {
        public string Status { get; set; }

        public string Intent { get; set; }

        public string Title { get; set; }

        public ReferencedConcept Subject { get; set; }

        public Period Period { get; set; }

        public ReferencedConcept Context { get; set; }

        public IList<ReferencedConcept> Author { get; set; }

        public IList<ReferencedConcept> Addresses { get; set; }

        public IList<Activity> Activity { get; set; }
    }
}