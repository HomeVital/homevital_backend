using HomeVital.Repositories.dbContext;
using HomeVital.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeVital.Repositories
{
    public static class DatabaseInitializer
    {
        public static void Initialize(HomeVitalDbContext context)
        {
            // Apply any pending migrations
            context.Database.Migrate();

            // Drop all tables
            context.Database.ExecuteSqlRaw("DROP SCHEMA public CASCADE;");
            context.Database.ExecuteSqlRaw("CREATE SCHEMA public;");

            // Reapply migrations to recreate the tables
            context.Database.Migrate();

            // Repopulate the tables with dummy data
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User {Kennitala = "1234567890", PatientID = 1 },
                    new User {Kennitala = "0987654321", PatientID = 2 },
                    new User {Kennitala = "1111111111", PatientID = 3 },
                    new User {Kennitala = "2222222222" , PatientID = 4 },
                    new User {Kennitala = "3333333333" , PatientID = 5 },
                    new User {Kennitala = "4444444444" , PatientID = 6 },
                    new User {Kennitala = "5555555555" , HealthcareWorkerID = 1 },
                    new User {Kennitala = "6666666666" , HealthcareWorkerID = 2 }
                );
            }

            if (!context.Patients.Any())
            {
                context.Patients.AddRange(
                    new Patient { Name = "Patient zero", Phone = "123456789", Status = "Active", Address = "123 Main St", TeamID = 1 },
                    new Patient { Name = "Patient 1", Phone = "987654321", Status = "Inactive", Address = "456 Elm St", TeamID = 2 },
                    new Patient { Name = "Jakub", Phone = "123456789", Status = "Active", Address = "123 Main Reykjavik", TeamID = 1 },
                    new Patient { Name = "þorir", Phone = "123456789", Status = "Active", Address = "123 Main Reykjavik", TeamID = 1 },
                    new Patient { Name = "sindri", Phone = "123456789", Status = "Active", Address = "123 Main Reykjavik", TeamID = 1 },
                    new Patient { Name = "aron", Phone = "123456789", Status = "Active", Address = "123 Main Reykjavik", TeamID = 1 }
                );
            }

            if (!context.Bloodsugars.Any())
            {
                context.Bloodsugars.AddRange(
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 5.5f, Date = DateTime.UtcNow },
                    new Bloodsugar { PatientID = 2, BloodsugarLevel = 6.2f, Date = DateTime.UtcNow },
                    new Bloodsugar { PatientID = 3, BloodsugarLevel = 7.5f, Date = DateTime.UtcNow },
                    new Bloodsugar { PatientID = 4, BloodsugarLevel = 8.2f, Date = DateTime.UtcNow },
                    new Bloodsugar { PatientID = 5, BloodsugarLevel = 9.5f, Date = DateTime.UtcNow },
                    new Bloodsugar { PatientID = 6, BloodsugarLevel = 10.2f, Date = DateTime.UtcNow }
                );
            }

            if (!context.BloodPressures.Any())
            {
                context.BloodPressures.AddRange(
                    new BloodPressure { PatientID = 1, Systolic = 120, Diastolic = 80, Date = DateTime.UtcNow, MeasureHand = "Left", BodyPosition = "Sitting", Pulse = 70, Status = "Normal" },
                    new BloodPressure { PatientID = 2, Systolic = 130, Diastolic = 85, Date = DateTime.UtcNow, MeasureHand = "Right", BodyPosition = "Standing", Pulse = 75, Status = "Elevated" },
                    new BloodPressure { PatientID = 3, Systolic = 140, Diastolic = 90, Date = DateTime.UtcNow, MeasureHand = "Left", BodyPosition = "Laying", Pulse = 80, Status = "Hypertension" },
                    new BloodPressure { PatientID = 4, Systolic = 150, Diastolic = 95, Date = DateTime.UtcNow, MeasureHand = "Right", BodyPosition = "Sitting", Pulse = 85, Status = "Hypertension" },
                    new BloodPressure { PatientID = 5, Systolic = 160, Diastolic = 100, Date = DateTime.UtcNow, MeasureHand = "Left", BodyPosition = "Standing", Pulse = 90, Status = "Hypertension" },
                    new BloodPressure { PatientID = 6, Systolic = 170, Diastolic = 105, Date = DateTime.UtcNow, MeasureHand = "Right", BodyPosition = "Laying", Pulse = 95, Status = "Hypertension" },
                    new BloodPressure { PatientID = 3, Systolic = 120, Diastolic = 80, Date = DateTime.UtcNow.AddDays(-1), MeasureHand = "Left", BodyPosition = "Sitting", Pulse = 70, Status = "Normal" },
                    new BloodPressure { PatientID = 3, Systolic = 130, Diastolic = 85, Date = DateTime.UtcNow.AddDays(-2), MeasureHand = "Right", BodyPosition = "Standing", Pulse = 75, Status = "Elevated" },
                    new BloodPressure { PatientID = 3, Systolic = 140, Diastolic = 90, Date = DateTime.UtcNow.AddDays(-3), MeasureHand = "Left", BodyPosition = "Laying", Pulse = 80, Status = "Hypertension" }
                );
            }

            if (!context.BodyWeights.Any())
            {
                context.BodyWeights.AddRange(
                    new BodyWeight { PatientID = 1, Weight = 70.5f, Date = DateTime.UtcNow },
                    new BodyWeight { PatientID = 2, Weight = 75.5f, Date = DateTime.UtcNow },
                    new BodyWeight { PatientID = 3, Weight = 80.5f, Date = DateTime.UtcNow },
                    new BodyWeight { PatientID = 4, Weight = 85.5f, Date = DateTime.UtcNow },
                    new BodyWeight { PatientID = 5, Weight = 90.5f, Date = DateTime.UtcNow },
                    new BodyWeight { PatientID = 6, Weight = 95.5f, Date = DateTime.UtcNow }
                );
            }

            if (!context.BodyTemperatures.Any())
            {
                context.BodyTemperatures.AddRange(
                    new BodyTemperature { PatientID = 1, Temperature = 36.5f, Date = DateTime.UtcNow },
                    new BodyTemperature { PatientID = 2, Temperature = 37.5f, Date = DateTime.UtcNow },
                    new BodyTemperature { PatientID = 3, Temperature = 38.5f, Date = DateTime.UtcNow },
                    new BodyTemperature { PatientID = 4, Temperature = 39.5f, Date = DateTime.UtcNow },
                    new BodyTemperature { PatientID = 5, Temperature = 40.5f, Date = DateTime.UtcNow },
                    new BodyTemperature { PatientID = 6, Temperature = 41.5f, Date = DateTime.UtcNow }
                );
            }

            if (!context.OxygenSaturations.Any())
            {
                context.OxygenSaturations.AddRange(
                    new OxygenSaturation { PatientID = 1, OxygenSaturationLevel = 100, Date = DateTime.UtcNow },
                    new OxygenSaturation { PatientID = 2, OxygenSaturationLevel = 98, Date = DateTime.UtcNow },
                    new OxygenSaturation { PatientID = 3, OxygenSaturationLevel = 97, Date = DateTime.UtcNow },
                    new OxygenSaturation { PatientID = 4, OxygenSaturationLevel = 96, Date = DateTime.UtcNow },
                    new OxygenSaturation { PatientID = 5, OxygenSaturationLevel = 95, Date = DateTime.UtcNow },
                    new OxygenSaturation { PatientID = 6, OxygenSaturationLevel = 94, Date = DateTime.UtcNow }
                );
            }

            if (!context.HealthcareWorkers.Any())
            {
                context.HealthcareWorkers.AddRange(
                    new HealthcareWorker { Name = "John Doe", Phone = "123456789", Status = "Active", TeamID = 1 },
                    new HealthcareWorker { Name = "Jane Smith", Phone = "987654321", Status = "Inactive", TeamID = 2 }
                );
            }

            context.SaveChanges();
        }
    }
}