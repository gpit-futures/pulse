using System;

namespace Pulse.Web.Controllers.Search.ResponseModels
{
    public class Patientdetail
    {
        public string Source { get; set; }

        public string SourceId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; }

        public string NhsNumber { get; set; }

        public HeadlineEntry VitalsHeadline { get; set; }

        public HeadlineEntry ResultsHeadline { get; set; }

        public HeadlineEntry TreatmentsHeadline { get; set; }

        public HeadlineEntry MedsHeadline { get; set; }

        public HeadlineEntry OrdersHeadline { get; set; }
    }
}