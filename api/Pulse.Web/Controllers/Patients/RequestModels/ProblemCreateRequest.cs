namespace Pulse.Web.Controllers.Patients.RequestModels
{
    public class ProblemCreateRequest
    {
        public string Code { get; set; }

        public string DateOfOnset { get; set; }

        public string Description { get; set; }

        public string Problem { get; set; }

        public string SourceId { get; set; }

        public string Terminology { get; set; }

        public bool IsImport { get; set; }
    }
}