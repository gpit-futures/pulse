using System;

namespace Pulse.Web.Controllers.Patients.ResponseModels
{
    public class AllergyDetailResponse
    {
        public string Author { get; set; }

        public string Cause { get; set; }

        public string CauseCode { get; set; }

        public string CauseTerminology { get; set; }

        public DateTime DateCreated { get; set; }

        public string OriginalComposition { get; set; }

        public string OriginalSource { get; set; }

        public string Reaction { get; set; }

        public string Source { get; set; }

        public string SourceId { get; set; }

        public string TerminologyCode { get; set; }
    }
}