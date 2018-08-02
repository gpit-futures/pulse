using System.Collections.Generic;
using Pulse.Domain.Patients.Entities;

namespace Pulse.Web.Controllers.Search.ResponseModels
{
    public class MainSearchResponse
    {
        public int TotalPatients => this.Patients.Count;

        public IList<PatientDetail> Patients { get; set; } = new List<PatientDetail>();
    }
}
