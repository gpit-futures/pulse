using System;
using Harness;
using Harness.Settings;

namespace Pulse.Migration
{
    class Program
    {
        static void Main(string[] args)
        {
            var connection = args.Length == 0
                ? "mongodb://localhost:27017"
                : args[0];

            Console.WriteLine("Migrating data...");
            Console.WriteLine(args.Length);

            var settings = new SettingsBuilder()
                .AddDatabase("Pulse")
                .WithConnectionString(connection)
                .DropDatabaseFirst()
                .AddCollection("patients", true, "./data/patients.json")
                .AddCollection("patientDetails", true, "./data/patientDetails.json")
                .Build();

            new HarnessManager()
                .UsingSettings(settings)
                .Build();

            Console.WriteLine("Import finished");
        }
    }
}
