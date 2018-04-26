using System;
using Pulse.Domain.Interfaces;

namespace Pulse.Domain.EntryItems.Entities
{
    public class Medication : IEntryItem, IEntity
    {
        public Guid Id { get; set; }

        public string DoseAmount { get; set; }

        public string DoseDirections { get; set; }

        public string DoseTiming { get; set; }

        public string MedicationCode { get; set; }

        public string MedicationTerminology { get; set; }

        public string Name { get; set; }

        public string Route { get; set; }

        public DateTime StartDate { get; set; }

        public string PatientId { get; set; }

        public string Author { get; set; }

        public DateTime DateCreated { get; set; }

        public string Source { get; set; }

        public string SourceId { get; set; }
    }
}