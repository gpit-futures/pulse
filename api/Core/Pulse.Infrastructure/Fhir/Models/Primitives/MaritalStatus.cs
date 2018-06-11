using System.Collections.Generic;

namespace Pulse.Infrastructure.Fhir.Models.Primitives
{
    public class MaritalStatus
    {
        public IList<Coding> Coding { get; set; }
    }
}