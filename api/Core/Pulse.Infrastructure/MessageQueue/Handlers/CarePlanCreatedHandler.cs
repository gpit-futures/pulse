using System;
using System.Linq;
using System.Threading.Tasks;
using Pulse.Domain.EntryItems.Entities;
using Pulse.Infrastructure.EntryItems;
using Pulse.Infrastructure.MessageQueue.Messages;
using Pulse.Infrastructure.Patients;

namespace Pulse.Infrastructure.MessageQueue.Handlers
{
    public class CarePlanCreatedHandler : MessageHandler<CarePlanCreated>
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

        public override async Task Handle(CarePlanCreated message)
        {
            var nhsNumber = message.Subject.Identifier.Value;
            var patient = await this.Patients.GetOne(nhsNumber);

            if (patient == null)
            {
                return;
            }

            var activity = message.Activity.Select(x =>
                $"{x.Detail.Status} {x.Detail.ProductCodeableConcept.Coding[0].Display}")
                .ToArray();

            var clinicalNote = new ClinicalNote
            {
                ClinicalNotesType = "Care Plan",
                Notes = $"{message.Title}. {message.Period.Start} - {message.Period.End}. {string.Join(", ", activity)}",
                PatientId = nhsNumber,
                Author = message.Author[0].Display,
                DateCreated = DateTime.Parse(message.Meta.LastUpdated),
                Source = "INR",
                SourceId = message.Identifier[0].Value
            };

            await this.ClinicalNotes.AddOrUpdate(clinicalNote);
        }
    }
}