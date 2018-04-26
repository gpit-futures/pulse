using System;

namespace Pulse.Web.Controllers.Patients.RequestModels
{
    public class MedicationEditRequest
    {
        public string UserId { get; set; }

        public string Name { get; set; }

        public string DoseAmount { get; set; }

        public string DoseTiming { get; set; }

        public string DoseDirections { get; set; }

        public string MedicationCode { get; set; }

        public string Route { get; set; }

        public string Author { get; set; }

        public long StartDate { get; set; }

        public int StartTime { get; set; }

        public DateTime DateCreated { get; set; }

        public object MedicationTerminology { get; set; }

        public string SourceId { get; set; }
    }
}