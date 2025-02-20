using HomeVital.Repositories.dbContext;
using HomeVital.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeVital.Repositories
{
    public static class DatabaseInitializer
    {
        public static void Initialize(HomeVitalDbContext context)
        {
            context.Database.Migrate();
            // Clear existing data (optional)
            context.Users.RemoveRange(context.Users);
            context.Patients.RemoveRange(context.Patients);
            context.Bloodsugars.RemoveRange(context.Bloodsugars);
            context.BloodPressures.RemoveRange(context.BloodPressures);
            context.HealthcareWorkers.RemoveRange(context.HealthcareWorkers);
            context.BodyWeights.RemoveRange(context.BodyWeights);
            context.SaveChanges();

            // // Reset identity columns
            // context.Database.ExecuteSqlRaw("ALTER SEQUENCE \"Users_Id_seq\" RESTART WITH 1");
            // context.Database.ExecuteSqlRaw("ALTER SEQUENCE \"Patients_Id_seq\" RESTART WITH 1");
            // context.Database.ExecuteSqlRaw("ALTER SEQUENCE \"Bloodsugars_Id_seq\" RESTART WITH 1");
            // context.Database.ExecuteSqlRaw("ALTER SEQUENCE \"BloodPressures_Id_seq\" RESTART WITH 1");
            // context.Database.ExecuteSqlRaw("ALTER SEQUENCE \"HealthcareWorkers_Id_seq\" RESTART WITH 1");
            // context.Database.ExecuteSqlRaw("ALTER SEQUENCE \"BodyWeights_Id_seq\" RESTART WITH 1");

            // Ensure sequences exist and reset identity columns
            context.Database.ExecuteSqlRaw(@"
                DO $$
                BEGIN
                    IF NOT EXISTS (SELECT 1 FROM pg_class WHERE relname = 'Users_Id_seq') THEN
                        CREATE SEQUENCE ""Users_Id_seq"";
                    END IF;
                    ALTER SEQUENCE ""Users_Id_seq"" RESTART WITH 1;

                    IF NOT EXISTS (SELECT 1 FROM pg_class WHERE relname = 'Patients_Id_seq') THEN
                        CREATE SEQUENCE ""Patients_Id_seq"";
                    END IF;
                    ALTER SEQUENCE ""Patients_Id_seq"" RESTART WITH 1;

                    IF NOT EXISTS (SELECT 1 FROM pg_class WHERE relname = 'Bloodsugars_Id_seq') THEN
                        CREATE SEQUENCE ""Bloodsugars_Id_seq"";
                    END IF;
                    ALTER SEQUENCE ""Bloodsugars_Id_seq"" RESTART WITH 1;

                    IF NOT EXISTS (SELECT 1 FROM pg_class WHERE relname = 'BloodPressures_Id_seq') THEN
                        CREATE SEQUENCE ""BloodPressures_Id_seq"";
                    END IF;
                    ALTER SEQUENCE ""BloodPressures_Id_seq"" RESTART WITH 1;

                    IF NOT EXISTS (SELECT 1 FROM pg_class WHERE relname = 'HealthcareWorkers_Id_seq') THEN
                        CREATE SEQUENCE ""HealthcareWorkers_Id_seq"";
                    END IF;
                    ALTER SEQUENCE ""HealthcareWorkers_Id_seq"" RESTART WITH 1;

                    IF NOT EXISTS (SELECT 1 FROM pg_class WHERE relname = 'BodyWeights_Id_seq') THEN
                        CREATE SEQUENCE ""BodyWeights_Id_seq"";
                    END IF;
                    ALTER SEQUENCE ""BodyWeights_Id_seq"" RESTART WITH 1;
                END
                $$;
            ");
            

            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User { UserName = "user1", Kennitala = "1234567890" },
                    new User { UserName = "user2", Kennitala = "0987654321" },
                    new User {UserName = "Jakub", Kennitala = "1111111111"} ,
                    new User {UserName = "þorir", Kennitala = "2222222222"},
                    new User {UserName = "sindri", Kennitala = "3333333333"},
                    new User {UserName = "aron", Kennitala = "4444444444"}
                );
            }

            if (!context.Patients.Any())
            {
                context.Patients.AddRange(
                    new Patient { Name = "John Doe", Phone = "123456789", Status = "Active", Address = "123 Main St", TeamID = 1 },
                    new Patient { Name = "Jane Smith", Phone = "987654321", Status = "Inactive", Address = "456 Elm St", TeamID = 2 },
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
                    new BloodPressure { PatientID = 6, Systolic = 170, Diastolic = 105, Date = DateTime.UtcNow, MeasureHand = "Right", BodyPosition = "Laying", Pulse = 95, Status = "Hypertension" }
                );
            }
            // healthcareworker
            if (!context.HealthcareWorkers.Any())
            {
                context.HealthcareWorkers.AddRange(
                    new HealthcareWorker { Name = "John Doe", Phone = "123456789", Status = "Active", TeamID = 1 },
                    new HealthcareWorker { Name = "Jane Smith", Phone = "987654321", Status = "Inactive", TeamID = 2 }
                );
            }
            // bodyweight
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

            context.SaveChanges();
        }
    }
}