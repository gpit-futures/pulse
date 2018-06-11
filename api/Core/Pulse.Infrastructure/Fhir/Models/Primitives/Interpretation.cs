using System.Collections.Generic;

namespace Pulse.Infrastructure.Fhir.Models.Primitives
{
    public class Interpretation
    {
        public IList<Coding> Coding { get; set; }
    }
}