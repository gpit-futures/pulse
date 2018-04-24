namespace Pulse.Web.Controllers.Patients.RequestModels
{
    public class QuerySearchRequest
    {
        public int MaxValue { get; set; }

        public int MinValue { get; set; }

        public bool QueryContains { get; set; }

        public string QueryText { get; set; }

        public bool SexFemale { get; set; }

        public bool SexMale { get; set; }

        public string Type { get; set; }
    }
}