using System.Collections.Generic;

namespace Pulse.Infrastructure.Fhir.Models.Primitives
{
    public class Category
    {
        public IList<Coding> Coding { get; set; }
    }
}