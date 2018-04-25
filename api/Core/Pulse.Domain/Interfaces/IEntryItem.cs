using System;

namespace Pulse.Domain.Interfaces
{
    public interface IEntryItem
    {
        Guid PatientId { get; set; }

        string Author { get; set; }

        DateTime DateCreated { get; set; }

        string Source { get; set; }

        string SourceId { get; set; }
    }
}