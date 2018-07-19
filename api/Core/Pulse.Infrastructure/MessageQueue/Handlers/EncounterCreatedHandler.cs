using System;
using Hl7.Fhir.Model;
using Pulse.Domain.EntryItems.Entities;
using Pulse.Infrastructure.EntryItems;
using Pulse.Infrastructure.MessageQueue.Messages;
using Pulse.Infrastructure.Patients;
using Task = System.Threading.Tasks.Task;

namespace Pulse.Infrastructure.MessageQueue.Handlers
{
    public class EncounterCreatedHandler : MessageHandlerBase<Encounter>, IMessageHandler<EncounterCreated>
    {
        public EncounterCreatedHandler(
            IClinicalNoteRepository clinicalNotes,
            IPatientRepository patients)
        {
            this.ClinicalNotes = clinicalNotes;
            this.Patients = patients;
        }

        private IClinicalNoteRepository ClinicalNotes { get; }

        private IPatientRepository Patients { get; }

        public async Task Handle(EncounterCreated message)
        {
            var obj = this.ParseMessage(message);

            var nhsNumber = obj.Subject.Identifier.Value;
            var patient = await this.Patients.GetOne(nhsNumber);

            if (patient == null)
            {
                return;
            }

            var clinicalNote = new ClinicalNote
            {
                ClinicalNotesType = "Encounter",
                Notes = $"Encounter created from INR",
                PatientId = nhsNumber,
                DateCreated = obj.Meta?.LastUpdated?.DateTime ?? DateTime.UtcNow,
                Source = "INR",
                SourceId = obj.Identifier[0].Value
            };

            await this.ClinicalNotes.AddOrUpdate(clinicalNote);
        }
    }
}