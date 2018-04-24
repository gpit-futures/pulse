using System;
using System.Collections.Generic;

namespace Pulse.Web.Controllers.Patients.ResponseModels
{
    public class PatientDetailResponse
    {
        public IList<SourceTextInfo> Allergies { get; set; }

        public IList<SourceTextInfo> Problems { get; set; }

        public IList<SourceTextInfo> Medications { get; set; }

        public IList<SourceTextInfo> Contacts { get; set; }

        public object[] Transfers { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Id { get; set; }

        public string Address { get; set; }

        public int PasNumber { get; set; }

        public string NhsNumber { get; set; }

        public string GpName { get; set; }

        public string GpAddress { get; set; }

        public string Telephone { get; set; }
    }
}