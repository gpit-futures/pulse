using System.Collections.Generic;

namespace Pulse.Infrastructure.Fhir.Models.Primitives
{
    public class Product
    {
        public IList<Coding> Coding { get; set; }
    }
}