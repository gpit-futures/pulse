using System;
using System.Threading.Tasks;
using Pulse.Domain.EntryItems.Entities;
using Pulse.Infrastructure.EntryItems;
using Pulse.Infrastructure.MessageQueue.Messages;
using Pulse.Infrastructure.Patients;

namespace Pulse.Infrastructure.MessageQueue.Handlers
{
    public class ObservationCreatedHandler : MessageHandler<ObservationCreated>
    {
        public ObservationCreatedHandler(
            IClinicalNoteRepository clinicalNotes, 
            IPatientRepository patients)
        {
            this.ClinicalNotes = clinicalNotes;
            this.Patients = patients;
        }

        private IClinicalNoteRepository ClinicalNotes { get; }

        private IPatientRepository Patients { get; }

        public override async Task Handle(ObservationCreated message)
        {
            var nhsNumber = message.Subject.Identifier.Value;
            var patient = await this.Patients.GetOne(nhsNumber);

            if (patient == null)
            {
                return;
            }

            var clinicalNote = new ClinicalNote
            {
                ClinicalNotesType = "Observation",
                Notes = $"{message.Code.Coding[0].Display}. {message.ValueQuantity.Value}",
                PatientId = nhsNumber,
                Author = message.Performer[0].Display,
                DateCreated = DateTime.Parse(message.Meta.LastUpdated),
                Source = "INR",
                SourceId = message.Identifier[0].Value
            };

            await this.ClinicalNotes.AddOrUpdate(clinicalNote);
        }
    }
}