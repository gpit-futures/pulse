using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pulse.Domain.EntryItems.Entities;
using Pulse.Infrastructure.EntryItems;
using Pulse.Infrastructure.PatientDetails;
using Pulse.Infrastructure.Patients;
using Pulse.Web.Controllers.Patients.RequestModels;
using Pulse.Web.Controllers.Patients.ResponseModels;
using Pulse.Web.Extensions;
using RawRabbit;

namespace Pulse.Web.Controllers.Patients
{
    [Authorize(Policy = "Read")]
    [Produces("application/json")]
    [Route("api/patients")]
    public class PatientsController : Controller
    {
        public PatientsController(IPatientRepository patients, 
            IPatientDetailsRepository patientDetails,
            IAllergyRepository allergies, 
            IMedicationRepository medications, 
            IDiagnosisRepository diagnoses, 
            IClinicalNoteRepository clinicalNotes, 
            IContactRepository contacts, IBusClient bus)
        {
            this.Patients = patients;
            this.PatientDetails = patientDetails;
            this.Allergies = allergies;
            this.Medications = medications;
            this.Diagnoses = diagnoses;
            this.ClinicalNotes = clinicalNotes;
            this.Contacts = contacts;
            this.Bus = bus;
        }

        private IPatientRepository Patients { get; }

        private IPatientDetailsRepository PatientDetails { get; }

        private IAllergyRepository Allergies { get; }

        private IMedicationRepository Medications { get; }

        private IDiagnosisRepository Diagnoses { get; }

        private IClinicalNoteRepository ClinicalNotes { get; }

        private IContactRepository Contacts { get; }

        private IBusClient Bus { get; }

        [HttpGet]
        public async Task<IActionResult> GetPatients()
        {
            var patients = await this.Patients.GetAll();
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
            var patient = await this.Patients.GetOne(patientId);

            if (patient == null)
            {
                return this.NotFound();
            }

            var allergies = await this.Allergies.GetAll(patientId);
            var problems = await this.Diagnoses.GetAll(patientId);
            var medications = await this.Medications.GetAll(patientId);
            var contacts = await this.Contacts.GetAll(patientId);

            var patientResponse = new PatientDetailResponse
            {
                Address = patient.Address,
                DateOfBirth = patient.DateOfBirth,
                Gender = patient.Gender,
                GpAddress = patient.GpAddress,
                GpName = patient.GpName,
                Id = patient.NhsNumber,
                Name = patient.Name,
                PasNumber = patient.PasNo,
                NhsNumber = patient.NhsNumber,
                Telephone = patient.Phone,
                Allergies = allergies.ToSourceTextInfoList(),
                Problems = problems.ToSourceTextInfoList(),
                Medications = medications.ToSourceTextInfoList(),
                Contacts = contacts.ToSourceTextInfoList(),
                Transfers = new object[]{}
            };

            return this.Ok(patientResponse);
        }

        [HttpGet("{patientId}/banner")]
        public async Task<IActionResult> GetPatientDetails(string patientId)
        {
            var patient = await this.PatientDetails.GetOne(patientId);

            if (patient == null)
            {
                return this.NotFound();
            }

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
            var notes = await this.ClinicalNotes.GetAll(patientId);

            var clinicalNotes = new List<ClinicalNoteResponse>();

            if (notes.Any())
            {
                clinicalNotes = notes.Select(x => new ClinicalNoteResponse
                {
                    Author = x.Author,
                    ClinicalNotesType = x.ClinicalNotesType,
                    DateCreated = x.DateCreated,
                    SourceId = x.SourceId,
                    Source = x.Source
                }).ToList();
            }

            return this.Ok(clinicalNotes);
        }

        [HttpGet("{patientId}/clinicalnotes/{sourceId}")]
        public async Task<IActionResult> GetPatientClinicalNotesDetail(string patientId, string sourceId)
        {
            var note = await this.ClinicalNotes.GetOne(patientId, sourceId);

            if (note == null)
            {
                return this.NotFound();
            }

            return this.Ok(note);
        }

        [Authorize(Policy = "Write")]
        [HttpPut("{patientId}/clinicalnotes/{sourceId}")]
        public async Task<IActionResult> EditPatientClinicalNotesDetail(string patientId, string sourceId, [FromBody] ClinicalNoteEditRequest note)
        {
            var existing = await this.ClinicalNotes.GetOne(patientId, sourceId)
                           ?? new ClinicalNote
                           {
                               DateCreated = DateTime.UtcNow,
                               PatientId = patientId,
                               Source = "some-other-source",
                               SourceId = $"{Guid.NewGuid()}"
                           };

            existing.Author = note.Author;
            existing.ClinicalNotesType = note.ClinicalNotesType;
            existing.Notes = note.Notes;

            await this.ClinicalNotes.AddOrUpdate(existing);

            return this.Ok();
        }

        [Authorize(Policy = "Write")]
        [HttpPost("{patientId}/clinicalnotes")]
        public async Task<IActionResult> CreatePatientClinicalNotesDetail(string patientId, [FromBody] ClinicalNoteCreateRequest note)
        {
            var clinicalNote = new ClinicalNote
            {
                Author = note.Author,
                ClinicalNotesType = note.ClinicalNotesType,
                DateCreated = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                Notes = note.Note,
                PatientId = patientId,
                SourceId = $"{Guid.NewGuid()}",
                Source = "ethercis"
            };

            await this.ClinicalNotes.AddOrUpdate(clinicalNote);

            return this.Ok();
        }

        [HttpGet("{patientId}/problems")]
        public async Task<IActionResult> GetPatientProblems(string patientId)
        {
            var all = await this.Diagnoses.GetAll(patientId); //

            var problems = new List<ProblemResponse>();

            if (all.Any())
            {
                problems = all.Select(x => new ProblemResponse
                    {
                        Problem = x.Problem,
                        Source = x.Source,
                        SourceId = x.SourceId
                    })
                    .ToList();
            }

            return this.Ok(problems);
        }

        [HttpGet("{patientId}/problems/{sourceId}")]
        public async Task<IActionResult> GetPatientProblemDetail(string patientId, string sourceId)
        {
            var problem = await this.Diagnoses.GetOne(patientId, sourceId);

            if (problem == null)
            {
                return this.NotFound();
            }

            return this.Ok(problem);
        }

        [Authorize(Policy = "Write")]
        [HttpPut("{patientId}/problems/{sourceId}")]
        public async Task<IActionResult> EditPatientproblemDetail(string patientId, string sourceId, [FromBody] ProblemEditRequest problem)
        {
            var existing = await this.Diagnoses.GetOne(patientId, sourceId)
                           ?? new Diagnosis
                           {
                               DateCreated = DateTime.UtcNow,
                               PatientId = patientId,
                               Source = "another-source",
                               SourceId = $"{Guid.NewGuid()}"
                           };

            existing.Author = problem.Author;
            existing.DateOfOnset = problem.DateOfOnset;
            existing.Code = problem.Code;
            existing.Description = problem.Description;
            existing.Problem = problem.Problem;
            existing.Terminology = problem.Terminology;

            await this.Diagnoses.AddOrUpdate(existing);

            return this.Ok();
        }

        [Authorize(Policy = "Write")]
        [HttpPost("{patientId}/problems")]
        public async Task<IActionResult> CreatePatientProblemDetail(string patientId, [FromBody] ProblemCreateRequest problem)
        {
            var diagnosis = new Diagnosis
            {
                Author = problem.Author,
                Code = problem.Code,
                DateCreated = DateTime.UtcNow,
                DateOfOnset = problem.DateOfOnset,
                Description = problem.Description,
                Problem = problem.Problem,
                PatientId = patientId,
                Source = "source-new",
                SourceId = $"{Guid.NewGuid()}",
                Terminology = problem.Terminology
            };

            await this.Diagnoses.AddOrUpdate(diagnosis);

            return this.Ok();
        }

        [HttpGet("{patientId}/medications")]
        public async Task<IActionResult> GetPatientMedications(string patientId)
        {
            var all = await this.Medications.GetAll(patientId);

            var medications = new List<MedicationResponse>();

            if (all.Any())
            {
                medications = all.Select(x => new MedicationResponse
                    {
                        DateCreated = x.DateCreated,
                        DoseAmount = x.DoseAmount,
                        Name = x.Name,
                        Source = x.Source,
                        SourceId = x.SourceId
                    })
                    .ToList();
            }

            return this.Ok(medications);
        }

        [HttpGet("{patientId}/medications/{sourceId}")]
        public async Task<IActionResult> GetPatientMedicationDetail(string patientId, string sourceId)
        {
            var medication = await this.Medications.GetOne(patientId, sourceId);

            if (medication == null)
            {
                return this.NotFound();
            }

            return this.Ok(medication);
        }

        [Authorize(Policy = "Write")]
        [HttpPost("{patientId}/medications")]
        public async Task<IActionResult> CreatePatientMedicationDetail(string patientId,
            [FromBody] MedicationCreateRequest create)
        {
            var medication = new Medication
            {
                Author = create.Author,
                DateCreated = DateTime.UtcNow,
                DoseAmount = create.DoseAmount,
                DoseDirections = create.DoseDirections,
                DoseTiming = create.DoseTiming,
                MedicationCode = create.MedicationCode,
                Name = create.Name,
                PatientId = patientId,
                Route = create.Route,
                StartDate = DateTime.UtcNow,
                Source = "marand",
                SourceId = $"{Guid.NewGuid()}"
            };

            await this.Medications.AddOrUpdate(medication);

            return this.Ok();
        }

        [Authorize(Policy = "Write")]
        [HttpPut("{patientId}/medications/{sourceId}")]
        public async Task<IActionResult> EditPatientMedicationDetails(string patientId, string sourceId,
            [FromBody] MedicationEditRequest medication)
        {
            var existing = await this.Medications.GetOne(patientId, sourceId)
                           ?? new Medication
                           {
                               DateCreated = DateTime.UtcNow,
                               PatientId = patientId,
                               Source = "another-source",
                               SourceId = $"{Guid.NewGuid()}"
                           };

            existing.DoseAmount = medication.DoseAmount;
            existing.Author = medication.Author;
            existing.DoseDirections = medication.DoseDirections;
            existing.DoseTiming = medication.DoseTiming;
            existing.MedicationCode = medication.MedicationCode;
            existing.Name = medication.Name;
            existing.Route = medication.Route;
            existing.StartDate = DateTime.UtcNow;

            await this.Medications.AddOrUpdate(existing);

            return this.Ok();
        }

        [HttpGet("{patientId}/contacts")]
        public async Task<IActionResult> GetPatientContacts(string patientId)
        {
            var all = await this.Contacts.GetAll(patientId);

            var contacts = new List<ContactResponse>();

            if (all.Any())
            {
                contacts = all.Select(x => new ContactResponse
                {
                    Name = x.Name,
                    NextOfKin = x.NextOfKin,
                    Relationship = x.Relationship,
                    Source = x.Source,
                    SourceId = x.SourceId
                }).ToList();
            }

            return this.Ok(contacts);
        }

        [HttpGet("{patientId}/contacts/{sourceId}")]
        public async Task<IActionResult> GetPatientContactDetails(string patientId, string sourceId)
        {
            var contact = await this.Contacts.GetOne(patientId, sourceId);

            if (contact == null)
            {
                return this.NotFound();
            }

            return this.Ok(contact);
        }

        [Authorize(Policy = "Write")]
        [HttpPost("{patientId}/contacts")]
        public async Task<IActionResult> CreatePatientContactDetail(string patientId,
            [FromBody] ContactCreateRequest create)
        {
            var contact = new Contact
            {
                Name = create.Name,
                ContactInformation = create.ContactInformation,
                NextOfKin = create.NextOfKin,
                Notes = create.Notes,
                Relationship = create.Relationship,
                RelationshipCode = create.RelationshipCode,
                RelationshipTerminology = create.RelationshipTerminology,
                Author = create.Author,
                DateCreated = DateTime.UtcNow,
                Source = "qwed",
                SourceId = $"{Guid.NewGuid()}",
                PatientId = patientId
            };

            await this.Contacts.AddOrUpdate(contact);

            return this.Ok();
        }

        [Authorize(Policy = "Write")]
        [HttpPut("{patientId}/contacts/{sourceId}")]
        public async Task<IActionResult> EditPatientContactDetail(string patientId, string sourceId,
            [FromBody] ContactEditRequest contact)
        {
            var existing = await this.Contacts.GetOne(patientId, sourceId)
                           ?? new Contact
                           {
                               DateCreated = DateTime.UtcNow,
                               PatientId = patientId,
                               Source = "another-source",
                               SourceId = $"{Guid.NewGuid()}"
                           };

            existing.Author = contact.Author;
            existing.Name = contact.Name;
            existing.NextOfKin = contact.NextOfKin;
            existing.Relationship = contact.Relationship;

            await this.Contacts.AddOrUpdate(existing);

            return this.Ok();
        }

        [HttpGet("{patientId}/allergies")]
        public async Task<IActionResult> GetPatientAllergies(string patientId)
        {
            var all = await this.Allergies.GetAll(patientId);

            var allergies = new List<AllergyResponse>();

            if (all.Any())
            {
                allergies = all.Select(x => new AllergyResponse
                    {
                        Cause = x.Cause,
                        Reaction = x.Reaction,
                        Source = x.Source,
                        SourceId = x.SourceId
                    })
                    .ToList();
            }

            return this.Ok(allergies);
        }

        [HttpGet("{patientId}/allergies/{sourceId}")]
        public async Task<IActionResult> GetPatientAllergyDetail(string patientId, string sourceId)
        {
            var allergy = await this.Allergies.GetOne(patientId, sourceId);

            if (allergy == null)
            {
                return this.NotFound();
            }

            return this.Ok(allergy);
        }

        [Authorize(Policy = "Write")]
        [HttpPost("{patientId}/allergies")]
        public async Task<IActionResult> CreatePatientAllergyDetail(string patientId,
            [FromBody] AllergyCreateRequest create)
        {
            var allergy = new Allergy
            {
                Cause = create.Cause,
                Author = "some-author",
                CauseCode = create.CauseCode,
                CauseTerminology = create.CauseTerminology,
                Reaction = create.Reaction,
                Source = "ethercis",
                SourceId = $"{Guid.NewGuid()}",
                DateCreated = DateTime.UtcNow,
                PatientId = patientId
            };

            await this.Allergies.AddOrUpdate(allergy);

            return this.Ok();
        }

        [Authorize(Policy = "Write")]
        [HttpPut("{patientId}/allergies/{sourceId}")]
        public async Task<IActionResult> EditPatientAlleryDetail(string patientId, string sourceId,
            [FromBody] AllergyEditRequest edit)
        {
            var existing = await this.Allergies.GetOne(patientId, sourceId)
                           ?? new Allergy
                           {
                               DateCreated = DateTime.UtcNow,
                               PatientId = patientId,
                               Source = "ethercis",
                               SourceId = $"{Guid.NewGuid()}"
                           };

            existing.Cause = edit.Cause;
            existing.CauseCode = edit.CauseCode;
            existing.CauseTerminology = edit.CauseTerminology;
            existing.Reaction = edit.Reaction;

            await this.Allergies.AddOrUpdate(existing);

            return this.Ok();
        }
    }
}