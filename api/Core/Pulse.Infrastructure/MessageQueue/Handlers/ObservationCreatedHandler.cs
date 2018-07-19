using System;
using Hl7.Fhir.Model;
using Pulse.Domain.EntryItems.Entities;
using Pulse.Infrastructure.EntryItems;
using Pulse.Infrastructure.MessageQueue.Messages;
using Pulse.Infrastructure.Patients;
using Task = System.Threading.Tasks.Task;

namespace Pulse.Infrastructure.MessageQueue.Handlers
{
    public class ObservationCreatedHandler : MessageHandlerBase<Observation>, IMessageHandler<ObservationCreated>
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

        public async Task Handle(ObservationCreated message)
        {
            var obj = this.ParseMessage(message);

            var nhsNumber = obj.Subject.Identifier.Value;
            var patient = await this.Patients.GetOne(nhsNumber);

            if (patient == null)
            {
                return;
            }

            var value = (SimpleQuantity)obj.Value;

            var clinicalNote = new ClinicalNote
            {
                ClinicalNotesType = "Observation",
                Notes = $"{obj.Code.Coding[0].Display}. Value: {value.Value}. Unit: {value.Unit}.",
                PatientId = nhsNumber,
                Author = obj.Performer[0].Display,
                DateCreated = obj.Meta?.LastUpdated?.DateTime ?? DateTime.UtcNow,
                Source = "INR",
                SourceId = obj.Identifier[0].Value
            };

            await this.ClinicalNotes.AddOrUpdate(clinicalNote);
        }
    }
}