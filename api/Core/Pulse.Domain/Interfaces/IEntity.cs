using System;
using MongoDB.Bson;

namespace Pulse.Domain.Interfaces
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}