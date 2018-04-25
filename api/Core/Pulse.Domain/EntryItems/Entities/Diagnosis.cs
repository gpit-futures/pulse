using System;
using MongoDB.Bson;
using Pulse.Domain.Interfaces;

namespace Pulse.Domain.EntryItems.Entities
{
    public class Diagnosis : IEntryItem, IEntity
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public DateTime DateOfOnset { get; set; }

        public string Description { get; set; }

        public string Problem { get; set; }

        public string Terminology { get; set; }

        public Guid PatientId { get; set; }

        public string Author { get; set; }

        public DateTime DateCreated { get; set; }

        public string Source { get; set; }

        public string SourceId { get; set; }
    }
}