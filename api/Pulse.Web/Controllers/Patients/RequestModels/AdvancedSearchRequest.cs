namespace Pulse.Web.Controllers.Patients.RequestModels
{
    public class AdvancedSearchRequest
    {
        public string NhsNumber { get; set; }

        public string Forename { get; set; }

        public int MaxValue { get; set; }

        public int MinValue { get; set; }

        public bool SexFemale { get; set; }

        public bool SexMale { get; set; }

        public string Surname { get; set; }
    }
}