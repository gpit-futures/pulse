namespace Pulse.Infrastructure.Fhir.Models.Primitives
{
    public class Detail
    {
        public string ScheduledString { get; set; }

        public string Status { get; set; }

        public Product ProductCodeableConcept { get; set; }
    }
}