using System;
using Harness;
using Harness.Settings;
using Pulse.Domain.Patients.Entities;

namespace Pulse.Migration
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var settings = new SettingsBuilder()
                .AddDatabase("Pulse")
                .WithConnectionString("mongodb://localhost:27017")
                .DropDatabaseFirst()
                .AddCollection("patients", true, "./data/patients.json")
                .Build();

            new HarnessManager()
                .UsingSettings(settings)
                .Build();

            Console.WriteLine("Import finished");
            Console.Read();
        }
    }
}
