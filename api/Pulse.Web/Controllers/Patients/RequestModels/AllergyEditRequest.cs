namespace Pulse.Web.Controllers.Patients.RequestModels
{
    public class AllergyEditRequest
    {
        public string Cause { get; set; }

        public string CauseCode { get; set; }

        public string CauseTerminology { get; set; }

        public string Reaction { get; set; }

        public string Source { get; set; }

        public string SourceId { get; set; }

        public string UserId { get; set; }
    }

}