using System;

namespace Pulse.Web.Controllers.Patients.RequestModels
{
    public class ProblemEditRequest
    {
        public string Author { get; set; }

        public string Code { get; set; }

        public DateTime DateOfOnset { get; set; }

        public string Description { get; set; }

        public string Problem { get; set; }

        public string Source { get; set; }

        public string SourceId { get; set; }

        public string Terminology { get; set; }
    }
}