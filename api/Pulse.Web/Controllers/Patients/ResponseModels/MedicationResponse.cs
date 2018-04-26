using System;

namespace Pulse.Web.Controllers.Patients.ResponseModels
{
    public class MedicationResponse
    {
        public DateTime DateCreated { get; set; }

        public string DoseAmount { get; set; }

        public string Name { get; set; }

        public string Source { get; set; }

        public string SourceId { get; set; }
    }
}