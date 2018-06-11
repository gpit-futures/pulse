namespace Pulse.Infrastructure.Fhir.Models.Primitives
{
    public class ReferencedConcept
    {
        public string Reference { get; set; }

        public string Display { get; set; }

        public Identifier Identifier { get; set; }
    }
}