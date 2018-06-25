using System;
using System.Threading.Tasks;
using Pulse.Infrastructure.EntryItems;
using Pulse.Infrastructure.MessageQueue.Messages;
using Pulse.Infrastructure.Patients;

namespace Pulse.Infrastructure.MessageQueue.Handlers
{
    public class ObservationUpdatedHandler : MessageHandler<ObservationUpdated>
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

        public override async Task Handle(ObservationUpdated message)
        {
            var nhsNumber = message.Subject.Identifier.Value;
            var patient = await this.Patients.GetOne(nhsNumber);

            if (patient == null)
            {
                return;
            }

            var observationId = message.Identifier[0].Value;
            var observation = await this.ClinicalNotes.GetOne(nhsNumber, $"{observationId}");

            if (observation == null)
            {
                return;
            }

            observation.Author = message.Performer[0].Display;
            observation.Notes = $"UPDATED: {message.Code.Coding[0].Display}. {message.ValueQuantity.Value}";
            observation.DateCreated = DateTime.Parse(message.Meta.LastUpdated);

            await this.ClinicalNotes.AddOrUpdate(observation);
        }
    }
}