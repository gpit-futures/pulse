using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pulse.Web.Controllers.Patients.RequestModels;
using Pulse.Web.Controllers.Patients.ResponseModels;

namespace Pulse.Web.Controllers.Patients
{
    [Produces("application/json")]
    [Route("api/patients")]
    public class PatientsController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetPatients()
        {
            var patients = new List<PatientResponse>();
            return this.Ok(patients);
        }

        [HttpPost("advancedSearch")]
        public async Task<IActionResult> AdvancedSearch([FromBody] AdvancedSearchRequest request)
        {
            var result = new List<AdvancedQuerySearchResponse>();
            return this.Ok(result);
        }

        [HttpPost("querySearch")]
        public async Task<IActionResult> QuerySearch([FromBody] QuerySearchRequest request)
        {
            var result = new List<AdvancedQuerySearchResponse>();
            return this.Ok(result);
        }

        [HttpGet("{patientId}")]
        public async Task<IActionResult> GetPatient(string patientId)
        {
            var patient = new PatientDetailResponse();
            return this.Ok(patient);
        }

        [HttpGet("{patientId}/counts")]
        public async Task<IActionResult> GetPatientCounts(string patientId)
        {
            var count = new
            {
                diagnosesCount = "",
                diagnosesDate = "1970-01-01T00:00:00-05:00",
                ordersCount = "",
                ordersDate = "1970-01-01T00:00:00-05:00",
                resultsCount = "",
                resultsDate = "1970-01-01T00:00:00-05:00",
                vitalsCount = 20,
                vitalsDate = "2017-05-04T08:54:14.698-04:00",
                source = "ethercis",
                sourceId = "ethercis-counts"
            };

            var result = new[] { count };

            return this.Ok(result);
        }

        [HttpGet("{patientId}/clinicalnotes")]
        public async Task<IActionResult> GetPatientClinicalNotes(string patientId)
        {
            var notes = new List<ClinicalNoteResponse>();
            return this.Ok(notes);
        }

        [HttpGet("{patientId}/clinicalnotes/{sourceId}")]
        public async Task<IActionResult> GetPatientClinicalNotesDetail(string patientId, string sourceId)
        {
            var notes = new List<ClinicalNoteDetailResponse>();
            return this.Ok(notes);
        }

        [HttpPut("{patientId}/clinicalnotes/{sourceId}")]
        public async Task<IActionResult> EditPatientClinicalNotesDetail(string patientId, string sourceId, [FromBody] ClinicalNoteEditRequest note)
        {
            var result = new { Info = "clinicalnotes updated" };
            return this.Ok(result);
        }

        [HttpPost("{patientId}/clinicalnotes")]
        public async Task<IActionResult> CreatePatientClinicalNotesDetail(string patientId, [FromBody] ClinicalNoteCreateRequest note)
        {
            var result = new { Info = "clinicalnotes saved" };
            return this.Ok(result);
        }

        [HttpGet("{patientId}/problems")]
        public async Task<IActionResult> GetPatientProblems(string patientId)
        {
            var problems = new List<ProblemResponse>();
            return this.Ok(problems);
        }

        [HttpGet("{patientId}/problems/{sourceId}")]
        public async Task<IActionResult> GetPatientProblemDetail(string patientId, string sourceId)
        {
            var problem = new ProblemDeailResponse();
            return this.Ok(problem);
        }

        [HttpPut("{patientId}/problems/{sourceId}")]
        public async Task<IActionResult> EditPatientproblemDetail(string patientId, string sourceId, [FromBody] ProblemEditRequest problem)
        {
            var result = new { Info = "problems updated" };
            return this.Ok(result);
        }

        [HttpPost("{patientId}/problems")]
        public async Task<IActionResult> CreatePatientProblemDetail(string patientId, [FromBody] ProblemCreateRequest problem)
        {
            var result = new { Info = "problems saved" };
            return this.Ok(result);
        }
    }
}