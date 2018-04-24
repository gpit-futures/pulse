namespace Pulse.Web.Controllers.Patients.RequestModels
{
    public class ClinicalNoteCreateRequest
    {
        public string Author { get; set; }

        public string ClinicalNotesType { get; set; }

        public string Note { get; set; }

        public string Source { get; set; }

        public string UserId { get; set; }
    }
}