using System;
using System.Collections.Generic;
using Pulse.Domain.Interfaces;

namespace Pulse.Domain.EntryItems.Entities
{
    public class TestResult : IEntryItem, IEntity
    {
        public Guid Id { get; set; }

        public DateTime SampleTaken { get; set; }

        public string Conclusion { get; set; }

        public string Status { get; set; }

        public string TestName { get; set; }

        public IList<ResultItem> Results { get; set; }

        public string PatientId { get; set; }

        public string Author { get; set; }

        public DateTime DateCreated { get; set; }

        public string Source { get; set; }

        public string SourceId { get; set; }
    }

    public class ResultItem
    {
        public string Result { get; set; }

        public string Value { get; set; }

        public string Unit { get; set; }

        public string NormalRange { get; set; }

        public string Comment { get; set; }
    }
}