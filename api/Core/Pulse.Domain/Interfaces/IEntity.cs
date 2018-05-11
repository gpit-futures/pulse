using System;

namespace Pulse.Domain.Interfaces
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}