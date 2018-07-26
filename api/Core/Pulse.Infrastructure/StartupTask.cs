using Microsoft.Extensions.DependencyInjection;
using Pulse.Infrastructure.EntryItems;
using Pulse.Infrastructure.MessageQueue;
using Pulse.Infrastructure.MessageQueue.Handlers;
using Pulse.Infrastructure.MessageQueue.Messages;
using Pulse.Infrastructure.Mongo;
using Pulse.Infrastructure.PatientDetails;
using Pulse.Infrastructure.Patients;

namespace Pulse.Infrastructure
{
    public static class StartupTask
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IMongoConnectionFactory, MongoConnectionFactory>();
            services.AddScoped<IMongoDatabaseFactory, MongoDatabaseFactory>();


            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IPatientDetailsRepository, PatientDetailsRepository>();

            services.AddScoped<IAllergyRepository, AllergyRepository>();
            services.AddScoped<IClinicalNoteRepository, ClinicalNoteRepository>();
            services.AddScoped<IDiagnosisRepository, DiagnosisRepository>();
            services.AddScoped<IMedicationRepository, MedicationRepository>();
            services.AddScoped<ITestResultRepository, TestResultRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();

            services.AddScoped<IMongoIndexBuilder, MongoIndexBuilder>();


            services.AddTransient<IMessageHandler<ObservationCreated>, ObservationCreatedHandler>();
            services.AddTransient<IMessageHandler<ObservationUpdated>, ObservationUpdatedHandler>();
            services.AddTransient<IMessageHandler<CarePlanCreated>, CarePlanCreatedHandler>();
            //services.AddTransient<IMessageHandler<PatientCreated>, PatientCreatedHandler>();
            services.AddTransient<IMessageHandler<EncounterCreated>, EncounterCreatedHandler>();
        }
    }
}