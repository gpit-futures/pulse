using System;
using System.Linq;
using Hl7.Fhir.Model;
using Pulse.Domain.EntryItems.Entities;
using Pulse.Infrastructure.EntryItems;
using Pulse.Infrastructure.MessageQueue.Messages;
using Pulse.Infrastructure.Patients;
using Task = System.Threading.Tasks.Task;

namespace Pulse.Infrastructure.MessageQueue.Handlers
{
    public class CarePlanCreatedHandler : MessageHandlerBase<CarePlan>, IMessageHandler<CarePlanCreated>
    {
        public CarePlanCreatedHandler(
            IClinicalNoteRepository clinicalNotes, 
            IPatientRepository patients)
        {
            this.ClinicalNotes = clinicalNotes;
            this.Patients = patients;
        }

        private IClinicalNoteRepository ClinicalNotes { get; }

        private IPatientRepository Patients { get; }

        public async Task Handle(CarePlanCreated message)
        {
            var obj = this.ParseMessage(message);

            var nhsNumber = obj.Subject.Identifier.Value;
            var patient = await this.Patients.GetOne(nhsNumber);

            if (patient == null)
            {
                return;
            }

            var activity = obj.Activity.Select(x =>
                    $"{((CodeableConcept)x.Detail.Product).Coding[0].Display} ({((CodeableConcept)x.Detail.Product).Coding[0].Code}) - {x.Detail.Status}")
                .ToArray();

            var clinicalNote = new ClinicalNote
            {
                ClinicalNotesType = "Care Plan",
                Notes = $"{obj.Title}. {obj.Period.Start} to {obj.Period.End}. {string.Join(", ", activity)}",
                PatientId = nhsNumber,
                Author = obj.Author[0].Display,
                DateCreated = obj.Meta?.LastUpdated?.DateTime ?? DateTime.UtcNow,
                Source = "INR",
                SourceId = obj.Identifier[0].Value
            };

            await this.ClinicalNotes.AddOrUpdate(clinicalNote);
        }
    }
}