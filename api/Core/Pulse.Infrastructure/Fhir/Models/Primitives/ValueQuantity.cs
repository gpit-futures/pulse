namespace Pulse.Infrastructure.Fhir.Models.Primitives
{
    public class ValueQuantity
    {
        public double Value { get; set; }

        public string Unit { get; set; }

        public string System { get; set; }

        public string Code { get; set; }
    }
}