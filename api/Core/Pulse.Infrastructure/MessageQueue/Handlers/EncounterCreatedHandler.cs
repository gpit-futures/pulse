using System;
using System.Linq;
using System.Threading.Tasks;
using Pulse.Domain.EntryItems.Entities;
using Pulse.Infrastructure.EntryItems;
using Pulse.Infrastructure.MessageQueue.Messages;
using Pulse.Infrastructure.Patients;

namespace Pulse.Infrastructure.MessageQueue.Handlers
{
    public class EncounterCreatedHandler : IMessageHandler<EncounterCreated>
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
            var nhsNumber = message.Subject.Identifier.Value;
            var patient = await this.Patients.GetOne(nhsNumber);

            if (patient == null)
            {
                return;
            }

            var clinicalNote = new ClinicalNote
            {
                ClinicalNotesType = "Encounter",
                Notes = $"{string.Join(", ", message.Reason.Select(x => x.Text).ToArray())}",
                PatientId = nhsNumber,
                DateCreated = DateTime.Parse(message.Meta.LastUpdated),
                Source = "INR",
                SourceId = message.Identifier[0].Value
            };

            await this.ClinicalNotes.AddOrUpdate(clinicalNote);
        }
    }
}