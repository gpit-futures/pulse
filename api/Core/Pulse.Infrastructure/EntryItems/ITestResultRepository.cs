using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pulse.Domain.EntryItems.Entities;

namespace Pulse.Infrastructure.EntryItems
{
    public interface ITestResultRepository
    {
        Task<IEnumerable<TestResult>> GetAll(Guid patientId);

        Task<TestResult> GetOne(Guid id);

        Task AddOrUpdate(TestResult item);
    }
}