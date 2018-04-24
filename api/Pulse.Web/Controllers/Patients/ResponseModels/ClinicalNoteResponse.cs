using System;

namespace Pulse.Web.Controllers.Patients.ResponseModels
{
    public class ClinicalNoteResponse
    {
        public string Author { get; set; }

        public string ClinicalNotesType { get; set; }

        public DateTime DateCreated { get; set; }

        public string Source { get; set; }

        public string SourceId { get; set; }
    }
}
