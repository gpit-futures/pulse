using System.Collections.Generic;
using PatientDetailEntity = Pulse.Domain.PatientDetails.Entities.PatientDetail;

namespace Pulse.Web.Controllers.Search.ResponseModels
{
    public class MainSearchResponse
    {
        public int TotalPatients => this.Patients.Count;

        public IList<PatientDetailEntity> Patients { get; set; } = new List<PatientDetailEntity>();
    }
}
