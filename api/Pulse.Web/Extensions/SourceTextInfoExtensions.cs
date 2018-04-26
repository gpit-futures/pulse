using System.Collections.Generic;
using System.Linq;
using Pulse.Domain.EntryItems.Entities;
using Pulse.Web.Controllers.Patients.ResponseModels;

namespace Pulse.Web.Extensions
{
    public static class SourceTextInfoExtensions
    {
        public static IList<SourceTextInfo> ToSourceTextInfoList(this IEnumerable<Allergy> allergies)
        {
            if (allergies == null || !allergies.Any())
            {
                return new List<SourceTextInfo>();
            }

            return allergies
                .Select(x => new SourceTextInfo
                {
                    Source = x.Source,
                    SourceId = x.SourceId,
                    Text = x.Cause
                }).ToList();
        }

        public static IList<SourceTextInfo> ToSourceTextInfoList(this IEnumerable<Diagnosis> diagnoses)
        {
            if (diagnoses == null || !diagnoses.Any())
            {
                return new List<SourceTextInfo>();
            }

            return diagnoses
                .Select(x => new SourceTextInfo
                {
                    Source = x.Source,
                    SourceId = x.SourceId,
                    Text = x.Description
                }).ToList();
        }

        public static IList<SourceTextInfo> ToSourceTextInfoList(this IEnumerable<Medication> medications)
        {
            if (medications == null || !medications.Any())
            {
                return new List<SourceTextInfo>();
            }

            return medications
                .Select(x => new SourceTextInfo
                {
                    Source = x.Source,
                    SourceId = x.SourceId,
                    Text = x.Name
                }).ToList();
        }

        public static IList<SourceTextInfo> ToSourceTextInfoList(this IEnumerable<Contact> contacts)
        {
            if (contacts == null || !contacts.Any())
            {
                return new List<SourceTextInfo>();
            }

            return contacts
                .Select(x => new SourceTextInfo
                {
                    Source = x.Source,
                    SourceId = x.SourceId,
                    Text = x.Name
                }).ToList();
        }
    }
}