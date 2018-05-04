using System.Collections.Generic;

namespace Pulse.Web.Controllers.Search.ResponseModels
{
    public class MainSearchResponse
    {
        public int TotalPatients { get; set; }

        public IList<PatientDetail> PatientDetails { get; set; }
    }
}
