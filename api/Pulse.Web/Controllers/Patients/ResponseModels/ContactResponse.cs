namespace Pulse.Web.Controllers.Patients.ResponseModels
{
    public class ContactResponse
    {
        public string Name { get; set; }

        public bool NextOfKin { get; set; }

        public string Relationship { get; set; }

        public string Source { get; set; }

        public string SourceId { get; set; }
    }
}