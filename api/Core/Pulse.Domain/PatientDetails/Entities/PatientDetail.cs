using System;
using Pulse.Domain.Interfaces;

namespace Pulse.Domain.PatientDetails.Entities
{
    public class PatientDetail : IEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public string Phone { get; set; }

        public string PasNumber { get; set; }

        public string NhsNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Address Address { get; set; }

        public Gp Gp { get; set; }
    }

    public class Address
    {
        public string Line1 { get; set; }

        public string Line2 { get; set; }

        public string Line3 { get; set; }

        public string Line4 { get; set; }

        public string Postcode { get; set; }
    }

    public class Gp
    {
        public string Name { get; set; }

        public Address Address { get; set; }
    }
}