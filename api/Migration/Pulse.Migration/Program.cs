using System;
using Harness;
using Harness.Settings;

namespace Pulse.Migration
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Migrating data...");

            var settings = new SettingsBuilder()
                .AddDatabase("Pulse")
                .WithConnectionString("mongodb://localhost:27017")
                .DropDatabaseFirst()
                .AddCollection("patients", true, "./data/patients.json")
                .AddCollection("patientDetails", true, "./data/patientDetails.json")
                .Build();

            new HarnessManager()
                .UsingSettings(settings)
                .Build();

            Console.WriteLine("Import finished");
            Console.Read();
        }
    }
}
