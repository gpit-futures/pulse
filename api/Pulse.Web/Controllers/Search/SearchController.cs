using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pulse.Infrastructure.Patients;
using Pulse.Web.Controllers.Search.RequestModels;
using Pulse.Web.Controllers.Search.ResponseModels;

namespace Pulse.Web.Controllers.Search
{
    [Produces("application/json")]
    [Route("api/search")]
    public class SearchController : Controller
    {
        public SearchController(IPatientRepository patients)
        {
            this.Patients = patients;
        }

        private IPatientRepository Patients { get; }

        [HttpPost("patient/table")]
        public async Task<IActionResult> MainSearch([FromBody] MainSearchRequest request)
        {
            var patients = await this.Patients.GetSome(20);

            var patientDetails = patients.Select(x => new PatientDetail
                {

                })
                .ToList();

            var result = new MainSearchResponse();
            return this.Ok(result);
        }
    }
}