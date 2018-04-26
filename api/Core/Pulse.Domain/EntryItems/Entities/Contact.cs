using System;
using Pulse.Domain.Interfaces;

namespace Pulse.Domain.EntryItems.Entities
{
    public class Contact : IEntryItem, IEntity
    {
        public Guid Id { get; set; }

        public string ContactInformation { get; set; }

        public string Name { get; set; }

        public bool NextOfKin { get; set; }

        public string Notes { get; set; }

        public string Relationship { get; set; }

        public string RelationshipCode { get; set; }

        public string RelationshipTerminology { get; set; }

        public string RelationshipType { get; set; }

        public string PatientId { get; set; }

        public string Author { get; set; }

        public DateTime DateCreated { get; set; }

        public string Source { get; set; }

        public string SourceId { get; set; }
    }
}