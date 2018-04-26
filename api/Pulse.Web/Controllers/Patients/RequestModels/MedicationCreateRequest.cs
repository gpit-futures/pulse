using System;

namespace Pulse.Web.Controllers.Patients.RequestModels
{
    public class MedicationCreateRequest
    {
        public string Author { get; set; }

        public string DoseAmount { get; set; }

        public string DoseDirections { get; set; }

        public string DoseTiming { get; set; }

        public bool IsImport { get; set; }

        public string MedicationCode { get; set; }

        public string Name { get; set; }

        public string Route { get; set; }

        public long StartDate { get; set; }

        public long StartTime { get; set; }

        public string SourceId { get; set; }

        public string UserId { get; set; }
    }
}