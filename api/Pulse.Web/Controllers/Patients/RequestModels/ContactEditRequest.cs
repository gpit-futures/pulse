using System;

namespace Pulse.Web.Controllers.Patients.RequestModels
{
    public class ContactEditRequest
    {
        public string Author { get; set; }

        public string ContactInformation { get; set; }

        public DateTime DateSubmitted { get; set; }

        public string Name { get; set; }

        public bool NextOfKin { get; set; }

        public string Notes { get; set; }

        public string Relationship { get; set; }

        public string RelationshipCode { get; set; }

        public string RelationshipTerminology { get; set; }

        public string Source { get; set; }

        public string SourceId { get; set; }

        public string UserId { get; set; }
    }

}