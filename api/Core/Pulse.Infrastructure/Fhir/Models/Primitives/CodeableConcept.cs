using System.Collections.Generic;

namespace Pulse.Infrastructure.Fhir.Models.Primitives
{
    public class CodeableConcept
    {
        public IList<Coding> Coding { get; set; } = new List<Coding>();
    }
}