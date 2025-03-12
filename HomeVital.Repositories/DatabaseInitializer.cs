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
                    new User { Kennitala = "1234567890", PatientID = 1 },
                    new User { Kennitala = "0987654321", PatientID = 2 },
                    new User { Kennitala = "1111111111", PatientID = 3 },
                    new User { Kennitala = "2222222222", PatientID = 4 },
                    new User { Kennitala = "3333333333", PatientID = 5 },
                    new User { Kennitala = "4444444444", PatientID = 6 },
                    new User { Kennitala = "5555555555", HealthcareWorkerID = 1 },
                    new User { Kennitala = "6666666666", HealthcareWorkerID = 2 }
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
                    new Bloodsugar { ID = 1, PatientID = 1, MeasurementID = 1, BloodsugarLevel = 5.5f, Date = DateTime.UtcNow },
                    new Bloodsugar { ID = 2, PatientID = 2, MeasurementID = 2, BloodsugarLevel = 6.2f, Date = DateTime.UtcNow },
                    new Bloodsugar { ID = 3, PatientID = 3, MeasurementID = 3, BloodsugarLevel = 7.5f, Date = DateTime.UtcNow },
                    new Bloodsugar { ID = 4, PatientID = 4, MeasurementID = 4, BloodsugarLevel = 8.2f, Date = DateTime.UtcNow },
                    new Bloodsugar { ID = 5, PatientID = 5, MeasurementID = 5, BloodsugarLevel = 9.5f, Date = DateTime.UtcNow },
                    new Bloodsugar { ID = 6, PatientID = 6, MeasurementID = 6, BloodsugarLevel = 10.2f, Date = DateTime.UtcNow }
                );
            }

            if (!context.BloodPressures.Any())
            {
                context.BloodPressures.AddRange(
                    new BloodPressure { ID = 1, PatientID = 1, MeasurementID = 7, Systolic = 120, Diastolic = 80, Date = DateTime.UtcNow, MeasureHand = "Left", BodyPosition = "Sitting", Pulse = 70, Status = "Normal" },
                    new BloodPressure { ID = 2, PatientID = 2, MeasurementID = 8, Systolic = 130, Diastolic = 85, Date = DateTime.UtcNow, MeasureHand = "Right", BodyPosition = "Standing", Pulse = 75, Status = "Elevated" },
                    new BloodPressure { ID = 3, PatientID = 3, MeasurementID = 9, Systolic = 140, Diastolic = 90, Date = DateTime.UtcNow, MeasureHand = "Left", BodyPosition = "Laying", Pulse = 80, Status = "Hypertension" },
                    new BloodPressure { ID = 4, PatientID = 4, MeasurementID = 10, Systolic = 150, Diastolic = 95, Date = DateTime.UtcNow, MeasureHand = "Right", BodyPosition = "Sitting", Pulse = 85, Status = "Hypertension" },
                    new BloodPressure { ID = 5, PatientID = 5, MeasurementID = 11, Systolic = 160, Diastolic = 100, Date = DateTime.UtcNow, MeasureHand = "Left", BodyPosition = "Standing", Pulse = 90, Status = "Hypertension" },
                    new BloodPressure { ID = 6, PatientID = 6, MeasurementID = 12, Systolic = 170, Diastolic = 105, Date = DateTime.UtcNow, MeasureHand = "Right", BodyPosition = "Laying", Pulse = 95, Status = "Hypertension" }
                );
            }

            if (!context.BodyWeights.Any())
            {
                context.BodyWeights.AddRange(
                    new BodyWeight { ID = 1, PatientID = 1, MeasurementID = 13, Weight = 70.5f, Date = DateTime.UtcNow },
                    new BodyWeight { ID = 2, PatientID = 2, MeasurementID = 14, Weight = 75.5f, Date = DateTime.UtcNow },
                    new BodyWeight { ID = 3, PatientID = 3, MeasurementID = 15, Weight = 80.5f, Date = DateTime.UtcNow },
                    new BodyWeight { ID = 4, PatientID = 4, MeasurementID = 16, Weight = 85.5f, Date = DateTime.UtcNow },
                    new BodyWeight { ID = 5, PatientID = 5, MeasurementID = 17, Weight = 90.5f, Date = DateTime.UtcNow },
                    new BodyWeight { ID = 6, PatientID = 6, MeasurementID = 18, Weight = 95.5f, Date = DateTime.UtcNow }
                );
            }

            if (!context.BodyTemperatures.Any())
            {
                context.BodyTemperatures.AddRange(
                    new BodyTemperature { ID = 1, PatientID = 1, MeasurementID = 19, Temperature = 36.5f, Date = DateTime.UtcNow },
                    new BodyTemperature { ID = 2, PatientID = 2, MeasurementID = 20, Temperature = 37.5f, Date = DateTime.UtcNow },
                    new BodyTemperature { ID = 3, PatientID = 3, MeasurementID = 21, Temperature = 38.5f, Date = DateTime.UtcNow },
                    new BodyTemperature { ID = 4, PatientID = 4, MeasurementID = 22, Temperature = 39.5f, Date = DateTime.UtcNow },
                    new BodyTemperature { ID = 5, PatientID = 5, MeasurementID = 23, Temperature = 40.5f, Date = DateTime.UtcNow },
                    new BodyTemperature { ID = 6, PatientID = 6, MeasurementID = 24, Temperature = 41.5f, Date = DateTime.UtcNow }
                );
            }

            if (!context.OxygenSaturations.Any())
            {
                context.OxygenSaturations.AddRange(
                    new OxygenSaturation { ID = 1, PatientID = 1, MeasurementID = 25, OxygenSaturationValue = 98, Date = DateTime.UtcNow },
                    new OxygenSaturation { ID = 2, PatientID = 2, MeasurementID = 26, OxygenSaturationValue = 97, Date = DateTime.UtcNow },
                    new OxygenSaturation { ID = 3, PatientID = 3, MeasurementID = 27, OxygenSaturationValue = 96, Date = DateTime.UtcNow },
                    new OxygenSaturation { ID = 4, PatientID = 4, MeasurementID = 28, OxygenSaturationValue = 95, Date = DateTime.UtcNow },
                    new OxygenSaturation { ID = 5, PatientID = 5, MeasurementID = 29, OxygenSaturationValue = 94, Date = DateTime.UtcNow },
                    new OxygenSaturation { ID = 6, PatientID = 6, MeasurementID = 30, OxygenSaturationValue = 93, Date = DateTime.UtcNow }
                );
            }

            if (!context.Measurements.Any())
            {
                context.Measurements.AddRange(
                    new Measurement { ID = 1, PatientID = 1},
                    new Measurement { ID = 2, PatientID = 2},
                    new Measurement { ID = 3, PatientID = 3},
                    new Measurement { ID = 4, PatientID = 4},
                    new Measurement { ID = 5, PatientID = 5},
                    new Measurement { ID = 6, PatientID = 6},
                    new Measurement { ID = 7, PatientID = 1},
                    new Measurement { ID = 8, PatientID = 2},
                    new Measurement { ID = 9, PatientID = 3},
                    new Measurement { ID = 10, PatientID = 4},
                    new Measurement { ID = 11, PatientID = 5},
                    new Measurement { ID = 12, PatientID = 6},
                    new Measurement { ID = 13, PatientID = 1},
                    new Measurement { ID = 14, PatientID = 2},
                    new Measurement { ID = 15, PatientID = 3},
                    new Measurement { ID = 16, PatientID = 4},
                    new Measurement { ID = 17, PatientID = 5},
                    new Measurement { ID = 18, PatientID = 6},
                    new Measurement { ID = 19, PatientID = 1},
                    new Measurement { ID = 20, PatientID = 2},
                    new Measurement { ID = 21, PatientID = 3},
                    new Measurement { ID = 22, PatientID = 4},
                    new Measurement { ID = 23, PatientID = 5},
                    new Measurement { ID = 24, PatientID = 6},
                    new Measurement { ID = 25, PatientID = 1},
                    new Measurement { ID = 26, PatientID = 2},
                    new Measurement { ID = 27, PatientID = 3},
                    new Measurement { ID = 28, PatientID = 4},
                    new Measurement { ID = 29, PatientID = 5},
                    new Measurement { ID = 30, PatientID = 6}

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