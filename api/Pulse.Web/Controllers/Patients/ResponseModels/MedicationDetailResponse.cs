using System;

namespace Pulse.Web.Controllers.Patients.ResponseModels
{
    public class MedicationDetailResponse
    {
        public string Author { get; set; }

        public DateTime DateCreated { get; set; }

        public string DoseAmount { get; set; }

        public string DoseTiming { get; set; }

        public string MedicationCode { get; set; }

        public string MedicationTerminology { get; set; }

        public string Name { get; set; }

        public string Route { get; set; }

        public string Source { get; set; }

        public string SourceId { get; set; }

        public DateTime StartDate { get; set; }
    }
}