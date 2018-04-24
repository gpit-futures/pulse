using System;

namespace Pulse.Web.Controllers.Patients.ResponseModels
{
    public class ProblemDeailResponse
    {
        public string Problem { get; set; }

        public DateTime DateOfOnset { get; set; }

        public string Description { get; set; }

        public string Terminology { get; set; }

        public int Code { get; set; }

        public string Author { get; set; }

        public DateTime DateCreated { get; set; }

        public string Source { get; set; }

        public string SourceId { get; set; }

        public string OriginalComposition { get; set; }

        public string OriginalSource { get; set; }
    }
}