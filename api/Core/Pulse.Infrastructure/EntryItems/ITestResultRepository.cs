using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pulse.Domain.EntryItems.Entities;

namespace Pulse.Infrastructure.EntryItems
{
    public interface ITestResultRepository
    {
        Task<IEnumerable<TestResult>> GetAll(string patientId);

        Task<TestResult> GetOne(Guid id);

        Task<TestResult> GetOne(string patientId, string sourceId);

        Task AddOrUpdate(TestResult item);
    }
}