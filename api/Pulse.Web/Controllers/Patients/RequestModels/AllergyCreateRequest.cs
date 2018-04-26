namespace Pulse.Web.Controllers.Patients.RequestModels
{
    public class AllergyCreateRequest
    {
        public string UserId { get; set; }

        public string Cause { get; set; }

        public string Reaction { get; set; }

        public string CauseTerminology { get; set; }

        public string CauseCode { get; set; }

        public bool IsImport { get; set; }

        public string SourceId { get; set; }
    }
}