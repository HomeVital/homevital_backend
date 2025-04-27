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

            // // Reapply migrations to recreate the tables
            context.Database.Migrate();
            if (!context.Teams.Any())
            {
                context.Teams.AddRange(
                    new Team { ID = 1, Name = "Team A" },
                    new Team { ID = 2, Name = "Team B" },
                    new Team { ID = 3, Name = "Team C" }
                );
                context.SaveChanges();
            }
           
            // 1234567890
            // 1234561234
            // 6543214321
            // 0987654321
            // Repopulate the tables with dummy data
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User { Kennitala = "1111111111", PatientID = 1 },
                    new User { Kennitala = "2222222222", PatientID = 2 },
                    new User { Kennitala = "3333333333", PatientID = 3 },
                    new User { Kennitala = "4444444444", PatientID = 4 },
                    new User { Kennitala = "5555555555", PatientID = 5 },
                    new User { Kennitala = "6666666666", PatientID = 6 },
                    new User { Kennitala = "1234123412", HealthcareWorkerID = 1 },
                    new User { Kennitala = "4321432143", HealthcareWorkerID = 2 },
                    new User { Kennitala = "1234567890", HealthcareWorkerID = 3 },
                    new User { Kennitala = "1234561234", HealthcareWorkerID = 4 },
                    new User { Kennitala = "6543214321", HealthcareWorkerID = 5 },
                    new User { Kennitala = "0987654321", HealthcareWorkerID = 6 }
                );
            }

            if (!context.Patients.Any())
            {
                context.Patients.AddRange(
                    new Patient { Name = "Þórir Gunnar Valgeirsson", Phone = "1234567", Status = "Active", Address = "123 Main St", TeamID = 1 },
                    new Patient { Name = "Jakub Ingvar Pitak", Phone = "1234567", Status = "Inactive", Address = "456 Elm St", TeamID = 2 },
                    new Patient { Name = "Aron Ingi Jónsson", Phone = "1234567", Status = "Active", Address = "123 Main Reykjavik", TeamID = 3 },
                    new Patient { Name = "Sindri Þór Guðmundsson", Phone = "1234567", Status = "Active", Address = "123 Main Reykjavik", TeamID = 1 },
                    new Patient { Name = "Sturla Emil Sturluson", Phone = "1234567", Status = "Active", Address = "123 Main Reykjavik", TeamID = 2 },
                    new Patient { Name = "Jón Jónsson", Phone = "1234567", Status = "Active", Address = "123 Main Reykjavik", TeamID = 3 }
                );
                context.SaveChanges();
            }
            if (!context.HealthcareWorkers.Any())
            {
                 // Retrieve teams from the database
                var teamA = context.Teams.FirstOrDefault(t => t.ID == 1);
                var teamB = context.Teams.FirstOrDefault(t => t.ID == 2);
                var teamC = context.Teams.FirstOrDefault(t => t.ID == 3);

                if (teamA == null || teamB == null || teamC == null)
                {
                    throw new Exception("Teams must be initialized before adding HealthcareWorkers.");
                }
                context.HealthcareWorkers.AddRange(
                    // new HealthcareWorker { Name = "Sigurmundur Davíð", Phone = "1234567", Status = "Active", TeamIDs = new List<int> { 1, 3 } },
                    new HealthcareWorker { Name = "Sigurmundur Davíð", Phone = "1234567", Status = "Active", Teams = new List<Team> {teamA, teamC} },
                    // new HealthcareWorker { Name = "Freyr Björgvin", Phone = "1234567", Status = "Active", TeamIDs = new List<int> { 2, 3 } },
                    new HealthcareWorker { Name = "Freyr Björgvin", Phone = "1234567", Status = "Active",Teams = new List<Team> {teamB, teamC} },
                    // new HealthcareWorker { Name = "Friðleif Eva", Phone = "1234567", Status = "Active", TeamIDs = new List<int> { 1, 3 } },
                    new HealthcareWorker { Name = "Friðleif Eva", Phone = "1234567", Status = "Active",Teams = new List<Team> {teamA, teamC}},
                    // new HealthcareWorker { Name = "Indiana Sigrún", Phone = "1234567", Status = "Active", TeamIDs = new List<int> { 2, 3 } },
                    new HealthcareWorker { Name = "Indiana Sigrún", Phone = "1234567", Status = "Active",Teams = new List<Team> {teamB, teamC} },
                    // new HealthcareWorker { Name = "Sveinveig Guðrún", Phone = "1234567", Status = "Active", TeamIDs = new List<int> { 1, 3 } },
                    new HealthcareWorker { Name = "Sveinveig Guðrún", Phone = "1234567", Status = "Active",Teams = new List<Team> {teamA, teamC} },
                    // new HealthcareWorker { Name = "Ingvi Ólafur", Phone = "1234567", Status = "Active", TeamIDs = new List<int> { 2, 3 } },
                    new HealthcareWorker { Name = "Ingvi Ólafur", Phone = "1234567", Status = "Active",Teams = new List<Team> {teamB, teamC}}
                );
                context.SaveChanges();
            }
            


            // add the ranges
            if (!context.BloodPressureRanges.Any())
            {
                context.BloodPressureRanges.AddRange(
                    new BloodPressureRange { PatientID = 1 },
                    new BloodPressureRange { PatientID = 2 },
                    new BloodPressureRange { PatientID = 3 },
                    new BloodPressureRange { PatientID = 4 },
                    new BloodPressureRange { PatientID = 5 },
                    new BloodPressureRange { PatientID = 6 }
                );
            }
            if (!context.BloodSugarRanges.Any())
            {
                context.BloodSugarRanges.AddRange(
                    new BloodSugarRange { PatientID = 1 },
                    new BloodSugarRange { PatientID = 2 },
                    new BloodSugarRange { PatientID = 3 },
                    new BloodSugarRange { PatientID = 4 },
                    new BloodSugarRange { PatientID = 5 },
                    new BloodSugarRange { PatientID = 6 }
                );
            }
            if (!context.BodyTemperatureRanges.Any())
            {
                context.BodyTemperatureRanges.AddRange(
                    new BodyTemperatureRange { PatientID = 1 },
                    new BodyTemperatureRange { PatientID = 2 },
                    new BodyTemperatureRange { PatientID = 3 },
                    new BodyTemperatureRange { PatientID = 4 },
                    new BodyTemperatureRange { PatientID = 5 },
                    new BodyTemperatureRange { PatientID = 6 }
                );
            }
            if (!context.BodyWeightRanges.Any())
            {
                context.BodyWeightRanges.AddRange(
                    new BodyWeightRange { PatientID = 1 },
                    new BodyWeightRange { PatientID = 2 },
                    new BodyWeightRange { PatientID = 3 },
                    new BodyWeightRange { PatientID = 4 },
                    new BodyWeightRange { PatientID = 5 },
                    new BodyWeightRange { PatientID = 6 }
                );
            }
            if (!context.OxygenSaturationRanges.Any())
            {
                context.OxygenSaturationRanges.AddRange(
                    new OxygenSaturationRange { PatientID = 1 },
                    new OxygenSaturationRange { PatientID = 2 },
                    new OxygenSaturationRange { PatientID = 3 },
                    new OxygenSaturationRange { PatientID = 4 },
                    new OxygenSaturationRange { PatientID = 5 },
                    new OxygenSaturationRange { PatientID = 6 }
                );
            }
            if (!context.Bloodsugars.Any())
            {
                context.Bloodsugars.AddRange(
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 5.5f, Date = DateTime.UtcNow.AddDays(-365), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 6.5f, Date = DateTime.UtcNow.AddDays(-653), Status = "Raised" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 7.5f, Date = DateTime.UtcNow.AddDays(-146), Status = "High" },
                    new Bloodsugar { PatientID = 2, BloodsugarLevel = 5.5f, Date = DateTime.UtcNow.AddDays(-22), Status = "Normal" },
                    new Bloodsugar { PatientID = 2, BloodsugarLevel = 6.5f, Date = DateTime.UtcNow.AddDays(-26), Status = "Raised" },
                    new Bloodsugar { PatientID = 2, BloodsugarLevel = 7.5f, Date = DateTime.UtcNow.AddDays(-145), Status = "High" },
                    new Bloodsugar { PatientID = 3, BloodsugarLevel = 5.5f, Date = DateTime.UtcNow.AddDays(-33), Status = "Normal" },
                    new Bloodsugar { PatientID = 3, BloodsugarLevel = 6.5f, Date = DateTime.UtcNow.AddDays(-21), Status = "Raised" },
                    new Bloodsugar { PatientID = 3, BloodsugarLevel = 7.5f, Date = DateTime.UtcNow.AddDays(-463), Status = "High" },
                    new Bloodsugar { PatientID = 4, BloodsugarLevel = 5.5f, Date = DateTime.UtcNow.AddDays(-436), Status = "Normal" },
                    new Bloodsugar { PatientID = 4, BloodsugarLevel = 6.5f, Date = DateTime.UtcNow.AddDays(-287), Status = "Raised" },
                    new Bloodsugar { PatientID = 4, BloodsugarLevel = 7.5f, Date = DateTime.UtcNow.AddDays(-166), Status = "High" },
                    new Bloodsugar { PatientID = 5, BloodsugarLevel = 5.5f, Date = DateTime.UtcNow.AddDays(-366), Status = "Normal" },
                    new Bloodsugar { PatientID = 5, BloodsugarLevel = 6.5f, Date = DateTime.UtcNow.AddDays(-246), Status = "Raised" },
                    new Bloodsugar { PatientID = 5, BloodsugarLevel = 7.5f, Date = DateTime.UtcNow.AddDays(-64), Status = "High" },
                    new Bloodsugar { PatientID = 6, BloodsugarLevel = 5.5f, Date = DateTime.UtcNow.AddDays(-346), Status = "Normal" },
                    new Bloodsugar { PatientID = 6, BloodsugarLevel = 6.5f, Date = DateTime.UtcNow.AddDays(-2253), Status = "Raised" },
                    new Bloodsugar { PatientID = 6, BloodsugarLevel = 7.5f, Date = DateTime.UtcNow.AddDays(-5), Status = "High" }
                );
            }

            if (!context.BloodPressures.Any())
            {
                context.BloodPressures.AddRange(
                    new BloodPressure { PatientID = 1, Systolic = 120, Diastolic = 80, Date = DateTime.UtcNow.AddDays(-3), MeasureHand = "Left", BodyPosition = "Sitting", Pulse = 70, Status = "Normal"},
                    new BloodPressure { PatientID = 1, Systolic = 130, Diastolic = 90, Date = DateTime.UtcNow.AddDays(-5), MeasureHand = "Left", BodyPosition = "Laying", Pulse = 70, Status = "Raised"},
                    new BloodPressure { PatientID = 1, Systolic = 140, Diastolic = 100, Date = DateTime.UtcNow.AddDays(-83), MeasureHand = "Right", BodyPosition = "Laying", Pulse = 70, Status = "High"},
                    new BloodPressure { PatientID = 2, Systolic = 120, Diastolic = 80, Date = DateTime.UtcNow.AddDays(-77), MeasureHand = "Left", BodyPosition = "Sitting", Pulse = 70, Status = "Normal"},
                    new BloodPressure { PatientID = 2, Systolic = 130, Diastolic = 90, Date = DateTime.UtcNow.AddDays(-275), MeasureHand = "Right", BodyPosition = "Laying", Pulse = 70, Status = "Raised"},
                    new BloodPressure { PatientID = 2, Systolic = 140, Diastolic = 100, Date = DateTime.UtcNow.AddDays(-8), MeasureHand = "Left", BodyPosition = "Sitting", Pulse = 70, Status = "High"},
                    new BloodPressure { PatientID = 3, Systolic = 120, Diastolic = 80, Date = DateTime.UtcNow.AddDays(-378), MeasureHand = "Right", BodyPosition = "Laying", Pulse = 70, Status = "Normal"},
                    new BloodPressure { PatientID = 3, Systolic = 130, Diastolic = 90, Date = DateTime.UtcNow.AddDays(-26), MeasureHand = "Left", BodyPosition = "Sitting", Pulse = 70, Status = "Raised"},
                    new BloodPressure { PatientID = 3, Systolic = 140, Diastolic = 100, Date = DateTime.UtcNow.AddDays(-155), MeasureHand = "Left", BodyPosition = "Sitting", Pulse = 70, Status = "High"},
                    new BloodPressure { PatientID = 4, Systolic = 120, Diastolic = 80, Date = DateTime.UtcNow.AddDays(-4), MeasureHand = "Right", BodyPosition = "Laying", Pulse = 70, Status = "Normal"},
                    new BloodPressure { PatientID = 4, Systolic = 130, Diastolic = 90, Date = DateTime.UtcNow.AddDays(-55), MeasureHand = "Left", BodyPosition = "Laying", Pulse = 70, Status = "Raised"},
                    new BloodPressure { PatientID = 4, Systolic = 140, Diastolic = 100, Date = DateTime.UtcNow.AddDays(-321), MeasureHand = "Left", BodyPosition = "Sitting", Pulse = 70, Status = "High"},
                    new BloodPressure { PatientID = 5, Systolic = 120, Diastolic = 80, Date = DateTime.UtcNow.AddDays(-3), MeasureHand = "Right", BodyPosition = "Sitting", Pulse = 70, Status = "Normal"},
                    new BloodPressure { PatientID = 5, Systolic = 130, Diastolic = 90, Date = DateTime.UtcNow.AddDays(-332), MeasureHand = "Right", BodyPosition = "Sitting", Pulse = 70, Status = "Raised"},
                    new BloodPressure { PatientID = 5, Systolic = 140, Diastolic = 100, Date = DateTime.UtcNow.AddDays(-232), MeasureHand = "Left", BodyPosition = "Laying", Pulse = 70, Status = "High"},
                    new BloodPressure { PatientID = 6, Systolic = 120, Diastolic = 80, Date = DateTime.UtcNow.AddDays(-55), MeasureHand = "Right", BodyPosition = "Sitting", Pulse = 70, Status = "Normal"},
                    new BloodPressure { PatientID = 6, Systolic = 130, Diastolic = 90, Date = DateTime.UtcNow.AddDays(-23), MeasureHand = "Right", BodyPosition = "Laying", Pulse = 70, Status = "Raised"},
                    new BloodPressure { PatientID = 6, Systolic = 140, Diastolic = 100, Date = DateTime.UtcNow.AddDays(-5), MeasureHand = "Right", BodyPosition = "Sitting", Pulse = 70, Status = "High"}
                );
            }

            if (!context.BodyWeights.Any())
            {
                context.BodyWeights.AddRange(
                    new BodyWeight { PatientID = 1, Weight = 70.5f, Date = DateTime.UtcNow.AddDays(-475), Status = "Normal" },
                    new BodyWeight { PatientID = 1, Weight = 80.5f, Date = DateTime.UtcNow.AddDays(-7), Status = "Raised" },
                    new BodyWeight { PatientID = 1, Weight = 90.5f, Date = DateTime.UtcNow.AddDays(-754), Status = "High" },
                    new BodyWeight { PatientID = 2, Weight = 70.5f, Date = DateTime.UtcNow.AddDays(-3475), Status = "Normal" },
                    new BodyWeight { PatientID = 2, Weight = 80.5f, Date = DateTime.UtcNow.AddDays(-277), Status = "Raised" },
                    new BodyWeight { PatientID = 2, Weight = 90.5f, Date = DateTime.UtcNow.AddDays(-775), Status = "High" },
                    new BodyWeight { PatientID = 3, Weight = 70.5f, Date = DateTime.UtcNow.AddDays(-578), Status = "Normal" },
                    new BodyWeight { PatientID = 3, Weight = 80.5f, Date = DateTime.UtcNow.AddDays(-65), Status = "Raised" },
                    new BodyWeight { PatientID = 3, Weight = 90.5f, Date = DateTime.UtcNow.AddDays(-76), Status = "High" },
                    new BodyWeight { PatientID = 4, Weight = 70.5f, Date = DateTime.UtcNow.AddDays(-457), Status = "Normal" },
                    new BodyWeight { PatientID = 4, Weight = 80.5f, Date = DateTime.UtcNow.AddDays(-85), Status = "Raised" },
                    new BodyWeight { PatientID = 4, Weight = 90.5f, Date = DateTime.UtcNow.AddDays(-7), Status = "High" },
                    new BodyWeight { PatientID = 5, Weight = 70.5f, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal" },
                    new BodyWeight { PatientID = 5, Weight = 80.5f, Date = DateTime.UtcNow.AddDays(-66), Status = "Raised" },
                    new BodyWeight { PatientID = 5, Weight = 90.5f, Date = DateTime.UtcNow.AddDays(-46), Status = "High" },
                    new BodyWeight { PatientID = 6, Weight = 70.5f, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal" },
                    new BodyWeight { PatientID = 6, Weight = 80.5f, Date = DateTime.UtcNow.AddDays(-44), Status = "Raised" },
                    new BodyWeight { PatientID = 6, Weight = 90.5f, Date = DateTime.UtcNow.AddDays(-634), Status = "High" }
                );
            }

            if (!context.BodyTemperatures.Any())
            {
                context.BodyTemperatures.AddRange(
                    new BodyTemperature { PatientID = 1, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-753), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 37.5f, Date = DateTime.UtcNow.AddDays(-2), Status = "Raised" },
                    new BodyTemperature { PatientID = 1, Temperature = 38.5f, Date = DateTime.UtcNow.AddDays(-23), Status = "High" },
                    new BodyTemperature { PatientID = 2, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-5), Status = "Normal" },
                    new BodyTemperature { PatientID = 2, Temperature = 37.5f, Date = DateTime.UtcNow.AddDays(-244), Status = "Raised" },
                    new BodyTemperature { PatientID = 2, Temperature = 38.5f, Date = DateTime.UtcNow.AddDays(-144), Status = "High" },
                    new BodyTemperature { PatientID = 3, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-34), Status = "Normal" },
                    new BodyTemperature { PatientID = 3, Temperature = 37.5f, Date = DateTime.UtcNow.AddDays(-43), Status = "Raised" },
                    new BodyTemperature { PatientID = 3, Temperature = 38.5f, Date = DateTime.UtcNow.AddDays(-1), Status = "High" },
                    new BodyTemperature { PatientID = 4, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-34), Status = "Normal" },
                    new BodyTemperature { PatientID = 4, Temperature = 37.5f, Date = DateTime.UtcNow.AddDays(-26), Status = "Raised" },
                    new BodyTemperature { PatientID = 4, Temperature = 38.5f, Date = DateTime.UtcNow.AddDays(-642), Status = "High" },
                    new BodyTemperature { PatientID = 5, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-124), Status = "Normal" },
                    new BodyTemperature { PatientID = 5, Temperature = 37.5f, Date = DateTime.UtcNow.AddDays(-442), Status = "Raised" },
                    new BodyTemperature { PatientID = 5, Temperature = 38.5f, Date = DateTime.UtcNow.AddDays(-12), Status = "High" },
                    new BodyTemperature { PatientID = 6, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-232), Status = "Normal" },
                    new BodyTemperature { PatientID = 6, Temperature = 37.5f, Date = DateTime.UtcNow.AddDays(-22), Status = "Raised" },
                    new BodyTemperature { PatientID = 6, Temperature = 38.5f, Date = DateTime.UtcNow.AddDays(-156), Status = "High" }
                );
            }

            if (!context.OxygenSaturations.Any())
            {
                context.OxygenSaturations.AddRange(
                    new OxygenSaturation { PatientID = 1, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-6), Status = "Normal"},
                    new OxygenSaturation { PatientID = 1, OxygenSaturationValue = 95, Date = DateTime.UtcNow.AddDays(-1), Status = "Raised"},
                    new OxygenSaturation { PatientID = 1, OxygenSaturationValue = 92, Date = DateTime.UtcNow.AddDays(-53), Status = "High"},
                    new OxygenSaturation { PatientID = 2, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-4), Status = "Normal"},
                    new OxygenSaturation { PatientID = 2, OxygenSaturationValue = 95, Date = DateTime.UtcNow.AddDays(-46), Status = "Raised"},
                    new OxygenSaturation { PatientID = 2, OxygenSaturationValue = 92, Date = DateTime.UtcNow.AddDays(-23), Status = "High"},
                    new OxygenSaturation { PatientID = 3, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-12), Status = "Normal"},
                    new OxygenSaturation { PatientID = 3, OxygenSaturationValue = 95, Date = DateTime.UtcNow.AddDays(-17), Status = "Raised"},
                    new OxygenSaturation { PatientID = 3, OxygenSaturationValue = 92, Date = DateTime.UtcNow.AddDays(-12), Status = "High"},
                    new OxygenSaturation { PatientID = 4, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-355), Status = "Normal"},
                    new OxygenSaturation { PatientID = 4, OxygenSaturationValue = 95, Date = DateTime.UtcNow.AddDays(-3), Status = "Raised"},
                    new OxygenSaturation { PatientID = 4, OxygenSaturationValue = 92, Date = DateTime.UtcNow.AddDays(-13), Status = "High"},
                    new OxygenSaturation { PatientID = 5, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-35), Status = "Normal"},
                    new OxygenSaturation { PatientID = 5, OxygenSaturationValue = 95, Date = DateTime.UtcNow.AddDays(-22), Status = "Raised"},
                    new OxygenSaturation { PatientID = 5, OxygenSaturationValue = 92, Date = DateTime.UtcNow.AddDays(-133), Status = "High"},
                    new OxygenSaturation { PatientID = 6, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-333), Status = "Normal"},
                    new OxygenSaturation { PatientID = 6, OxygenSaturationValue = 95, Date = DateTime.UtcNow.AddDays(-2325), Status = "Raised"},
                    new OxygenSaturation { PatientID = 6, OxygenSaturationValue = 92, Date = DateTime.UtcNow.AddDays(-1), Status = "High"}
                );
            }

            




            context.SaveChanges();
        }
    }
}