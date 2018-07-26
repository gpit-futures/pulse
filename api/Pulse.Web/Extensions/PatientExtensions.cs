using System;
using System.Collections.Generic;
using Hl7.Fhir.Model;

namespace Pulse.Web.Extensions
{
    public static class PatientExtensions
    {
        public static Patient ToFhir(this Domain.PatientDetails.Entities.PatientDetail patient)
        {
            return new Patient
            {
                Name = new List<HumanName>
                {
                    new HumanName
                    {
                        Family = patient.LastName,
                        Given = new[] {patient.FirstName},
                        Prefix = new[] {patient.Title},
                        Use = HumanName.NameUse.Usual
                    }
                },
                Active = true,
                BirthDate = $"{patient.DateOfBirth:s}",
                Identifier = new List<Identifier>
                {
                    new Identifier("https://fhir.nhs.uk/Id/nhs-number", patient.NhsNumber)
                },
                Gender = StringToGender(patient.Gender),
                Address = new List<Address>
                {
                    new Address
                    {
                        Use = Address.AddressUse.Work,
                        Type = Address.AddressType.Both,
                        Line = new []
                        {
                            patient.Address.Line1,
                            patient.Address.Line2
                        },
                        District = patient.Address.Line3 ?? "Some District",
                        City = patient.Address.Line4 ?? "Some City",
                        PostalCode = patient.Address.Postcode
                    }
                },
                Telecom = new List<ContactPoint>
                {
                    new ContactPoint(ContactPoint.ContactPointSystem.Phone, ContactPoint.ContactPointUse.Home, patient.Phone)
                },
                Id = patient.PasNumber,
                Meta = new Meta
                {
                    VersionId = "1",
                    LastUpdated = DateTimeOffset.UtcNow
                }
            };
        }

        private static AdministrativeGender StringToGender(string gender)
        {
            switch (gender)
            {
                case "Male":
                case "M":
                    return AdministrativeGender.Male;
                case "Female":
                case "F":
                    return AdministrativeGender.Female;
                case "Other":
                    return AdministrativeGender.Other;
                default:
                    return AdministrativeGender.Unknown;
            }
        }
    }
}