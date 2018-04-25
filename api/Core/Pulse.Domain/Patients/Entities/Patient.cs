using System;
using Pulse.Domain.Interfaces;

namespace Pulse.Domain.Patients.Entities
{
    public class Patient : IEntity
    {
        public Guid Id { get; set; }

        public string Address { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Department { get; set; }

        public string Gender { get; set; }

        public string GpAddress { get; set; }

        public string GpName { get; set; }

        public string Name { get; set; }

        public string NhsNumber { get; set; }

        public string PasNo { get; set; }

        public string Phone { get; set; }
    }
}