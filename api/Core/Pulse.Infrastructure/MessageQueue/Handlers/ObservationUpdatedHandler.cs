using System;
using Hl7.Fhir.Model;
using Pulse.Infrastructure.EntryItems;
using Pulse.Infrastructure.MessageQueue.Messages;
using Pulse.Infrastructure.Patients;
using Task = System.Threading.Tasks.Task;

namespace Pulse.Infrastructure.MessageQueue.Handlers
{
    public class ObservationUpdatedHandler : MessageHandlerBase<Observation>, IMessageHandler<ObservationUpdated>
    {
        public ObservationUpdatedHandler(
            IClinicalNoteRepository clinicalNotes, 
            IPatientRepository patients)
        {
            this.ClinicalNotes = clinicalNotes;
            this.Patients = patients;
        }

        private IClinicalNoteRepository ClinicalNotes { get; }

        private IPatientRepository Patients { get; }

        public async Task Handle(ObservationUpdated message)
        {
            var obj = this.ParseMessage(message);

            var nhsNumber = obj.Subject.Identifier.Value;
            var patient = await this.Patients.GetOne(nhsNumber);

            if (patient == null)
            {
                return;
            }

            var observationId = obj.Identifier[0].Value;
            var observation = await this.ClinicalNotes.GetOne(nhsNumber, $"{observationId}");

            if (observation == null)
            {
                return;
            }

            var value = (SimpleQuantity)obj.Value;

            observation.Author = obj.Performer[0].Display;
            observation.Notes = $"AMENDED: {obj.Code.Coding[0].Display}. Value: {value.Value}. Unit: {value.Unit}.";
            observation.DateCreated = obj.Meta?.LastUpdated?.DateTime ?? DateTime.UtcNow;

            await this.ClinicalNotes.AddOrUpdate(observation);
        }
    }
}