﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Hl7.Fhir.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pulse.Infrastructure.PatientDetails;
using Pulse.Infrastructure.Patients;
using Pulse.Web.Controllers.Search.RequestModels;
using Pulse.Web.Controllers.Search.ResponseModels;
using Pulse.Web.Extensions;
using Patient = Pulse.Domain.Patients.Entities.Patient;

namespace Pulse.Web.Controllers.Search
{
    [Produces("application/json")]
    //[Authorize(Policy = "Read")]
    [Route("api/search")]
    public class SearchController : Controller
    {
        public SearchController(IPatientRepository patients, 
            IPatientDetailsRepository patientsDetail)
        {
            this.Patients = patients;
            this.PatientsDetail = patientsDetail;

            this.FhirSerializer = new FhirJsonSerializer(new ParserSettings{ AcceptUnknownMembers = false});
        }

        private IPatientRepository Patients { get; }

        private IPatientDetailsRepository PatientsDetail { get; }

        private FhirJsonSerializer FhirSerializer { get; }

        [HttpPost("patients")]
        public async Task<IActionResult> MainSearch([FromBody] MainSearchRequest request)
        {
            var patients = await this.Patients.Search(request.SearchString);

            if (patients == null)
            {
                return this.Ok(new MainSearchResponse());
            }

            var result = new MainSearchResponse
            {
                Patients = (IList<Patient>)patients
            };

            return this.Ok(result);
        }

        [HttpGet("patients/{id}")]
        public async Task<IActionResult> GetPatientFhir(string id)
        {
            var patient = await this.PatientsDetail.GetOne(id);

            if (patient == null)
            {
                return this.NotFound();
            }

            var fhir = patient.ToFhir();

            return this.Ok(this.FhirSerializer.SerializeToDocument(fhir));
        }
    }
}