using System;
using MongoDB.Bson;
using Pulse.Domain.Common;
using Pulse.Domain.Interfaces;

namespace Pulse.Domain.EntryItems.Entities
{
    public class Allergy : IEntryItem, IEntity
    {
        public Guid Id { get; set; }

        public string Cause { get; set; }

        public string CauseCode { get; set; }

        public string CauseTerminology { get; set; }

        public string OriginalComposition { get; set; }

        public string OriginalSource { get; set; }

        public string Reaction { get; set; }

        public string TerminologyCode { get; set; }

        public Guid PatientId { get; set; }

        public string Author { get; set; }

        public DateTime DateCreated { get; set; }

        public string Source { get; set; }

        public string SourceId { get; set; }
    }
}