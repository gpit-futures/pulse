namespace Pulse.Web.Controllers.Patients.RequestModels
{
    public class ClinicalNoteEditRequest
    {
        public string Author { get; set; }

        public string ClinicalNotesType { get; set; }

        public string Notes { get; set; }

        public string Source { get; set; }

        public string SourceId { get; set; }

        public string UserId { get; set; }
    }
}