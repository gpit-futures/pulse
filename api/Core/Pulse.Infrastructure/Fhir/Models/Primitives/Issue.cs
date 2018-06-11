namespace Pulse.Infrastructure.Fhir.Models.Primitives
{
    public class Issue
    {
        public string Severity { get; set; }

        public string Code { get; set; }

        public string Diagnostics { get; set; }
    }
}