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
                    // new Team { Name = "Teymi 1" },
                    // new Team { Name = "Teymi 2" },
                    // new Team { Name = "Teymi 3" },
                    new Team { Name = "Sárateymi" },
                    // new Team { Name = "Sárateymi 2" },
                    new Team { Name = "Lungnateymi" },
                    // new Team { Name = "Lungnateymi 2" },
                    new Team { Name = "Insulinteymi" },
                    // new Team { Name = "Insulinteymi 2" },
                    new Team { Name = "Krabbameinsteymi" },
                    new Team { Name = "Parkinsonsteymi" }
                );
                context.SaveChanges();
            }
           
       
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    // 20 Patients
                    new User { Kennitala = "1111111111", PatientID = 1 },
                    new User { Kennitala = "2222222222", PatientID = 2 },
                    new User { Kennitala = "3333333333", PatientID = 3 },
                    new User { Kennitala = "4444444444", PatientID = 4 },
                    new User { Kennitala = "5555555555", PatientID = 5 },
                    new User { Kennitala = "6666666666", PatientID = 6 },
                    new User { Kennitala = "7777777777", PatientID = 7 },
                    new User { Kennitala = "8888888888", PatientID = 8 },
                    new User { Kennitala = "9999999999", PatientID = 9 },
                    new User { Kennitala = "1010101010", PatientID = 10 },
                    new User { Kennitala = "1111111112", PatientID = 11 },
                    new User { Kennitala = "1212121212", PatientID = 12 },
                    new User { Kennitala = "1313131313", PatientID = 13 },
                    new User { Kennitala = "1414141414", PatientID = 14 },
                    new User { Kennitala = "1515151515", PatientID = 15 },
                    new User { Kennitala = "1616161616", PatientID = 16 },
                    new User { Kennitala = "1717171717", PatientID = 17 },
                    new User { Kennitala = "1818181818", PatientID = 18 },
                    new User { Kennitala = "1919191919", PatientID = 19 },
                    new User { Kennitala = "2020202020", PatientID = 20 },
                    new User { Kennitala = "2121212121", PatientID = 21 },
                    // 20 Workers 0987654321
                    new User { Kennitala = "1234567890", HealthcareWorkerID = 1 },
                    new User { Kennitala = "0987654321", HealthcareWorkerID = 2 },
                    new User { Kennitala = "1234123412", HealthcareWorkerID = 3 },
                    new User { Kennitala = "1234561234", HealthcareWorkerID = 4 },
                    new User { Kennitala = "6543214321", HealthcareWorkerID = 5 },
                    new User { Kennitala = "4321432143", HealthcareWorkerID = 6 },
                    new User { Kennitala = "9090909090", HealthcareWorkerID = 7 },
                    new User { Kennitala = "1010101011", HealthcareWorkerID = 8 },
                    new User { Kennitala = "1111111113", HealthcareWorkerID = 9 },
                    new User { Kennitala = "1212121213", HealthcareWorkerID = 10 },
                    new User { Kennitala = "1313131314", HealthcareWorkerID = 11 },
                    new User { Kennitala = "1414141415", HealthcareWorkerID = 12 },
                    new User { Kennitala = "1515151516", HealthcareWorkerID = 13 },
                    new User { Kennitala = "1616161617", HealthcareWorkerID = 14 },
                    new User { Kennitala = "1717171718", HealthcareWorkerID = 15 },
                    new User { Kennitala = "1818181819", HealthcareWorkerID = 16 },
                    new User { Kennitala = "1919191920", HealthcareWorkerID = 17 },
                    new User { Kennitala = "2020202021", HealthcareWorkerID = 18 },
                    new User { Kennitala = "2121212122", HealthcareWorkerID = 19 },
                    new User { Kennitala = "2222222223", HealthcareWorkerID = 20 }
                );
            }

            if (!context.Patients.Any())
            {
                context.Patients.AddRange(
                    // 20 Patients 
                    new Patient { Name = "Guðrún Guðmundsdóttir", Phone = "8834575", Address = "Geislagata 9, 600 Akureyri", TeamID = 3 },
                    new Patient { Name = "Kolbrún Eggertsdóttir", Phone = "8957970", Address = "Eyrarlandsvegur 1, 600 Akureyri,", TeamID = 2 },
                    new Patient { Name = "Aron Ingi Jónsson", Phone = "8554013", Address = "Strandgata 12, 600 Akureyri", TeamID = 3 },
                    new Patient { Name = "Sindri Þór Guðmundsson", Phone = "8037056", Address = "Eyrarlandsvegur 10, 600 Akureyri", TeamID = 4 },
                    new Patient { Name = "Sturla Emil Sturluson", Phone = "8533691", Address = "Brekkugata 17, 600 Akureyri", TeamID = 5 },
                    new Patient { Name = "Jón Jónsson", Phone = "8255942", Address = "Þingvallastræti 23, 600 Akureyri", TeamID = 1 },
                    new Patient { Name = "Sigurður Sigurðsson", Phone = "8404784", Address = "Eyrarlandsvegur 26, 600 Akureyri", TeamID = 2 },
                    new Patient { Name = "Þórir Gunnar Valgeirsson", Phone = "8926335", Address = "Eyrarlandsvegur 1, 600 Akureyri", TeamID = 5 },
                    new Patient { Name = "Sigríður Eggertsdóttir", Phone = "8363273", Address = "Glerárgata 22, 600 Akureyri", TeamID = 2 },
                    new Patient { Name = "Einar Einarsson", Phone = "8328049", Address = "Skipagata 9, 600 Akureyri", TeamID = 2 },
                    new Patient { Name = "Anna Björnsdóttir", Phone = "8060289", Address = "Strandgata 23, 600 Akureyri", TeamID = 3 },
                    new Patient { Name = "Björk Björnsdóttir", Phone = "8590348", Address = "Þingvallastræti 1, 600 Akureyri", TeamID = 3 },
                    new Patient { Name = "Kristján Kristjánsson", Phone = "8991327", Address = "Aðalstræti 58, 600 Akureyri", TeamID = 4 },
                    new Patient { Name = "Sólveig Sólveigardóttir", Phone = "8038508", Address = "Sólborg, Norðurslóð 2, 600 Akureyri", TeamID = 2 },
                    new Patient { Name = "Hrafn Hrafnsson", Phone = "8382062", Address = "Skólastígur 1, 600 Akureyri", TeamID = 3 },
                    new Patient { Name = "Ragnheiður Ragnarsdóttir", Phone = "8319557", Address = "Glerárgata 34, 600 Akureyri", TeamID = 1 },
                    new Patient { Name = "Gunnar Gunnarsson", Phone = "8766134", Address = "Glerárgata 32, 600 Akureyri", TeamID = 2 },
                    new Patient { Name = "Elín Elínardóttir", Phone = "8257583", Address = "Aðalstræti 60, 600 Akureyri", TeamID = 3 },
                    new Patient { Name = "Sigurður Sigurðsson", Phone = "8108364", Address = "Strandgata 1, 600 Akureyri", TeamID = 1 },
                    new Patient { Name = "Jakub Ingvar Pitak", Phone = "8834930", Address = "Skólastígur 3, 600 Akureyri", TeamID = 2 },
                    new Patient { Name = "Patient For TESTS", Phone = "8554013", Address = "Eyrarlandsvegur 12, 600 Akureyri", TeamID = 3 }
                );
                context.SaveChanges();
            }
            if (!context.HealthcareWorkers.Any())
            {
                 // Retrieve teams from the database
                var Sárateymi = context.Teams.FirstOrDefault(t => t.ID == 1);
                var Lungnateymi = context.Teams.FirstOrDefault(t => t.ID == 2);
                var Insulinteymi = context.Teams.FirstOrDefault(t => t.ID == 3);
                var Krabbameinsteymi = context.Teams.FirstOrDefault(t => t.ID == 4);
                var Parkinsonsteymi = context.Teams.FirstOrDefault(t => t.ID == 5);

                if (Sárateymi == null || Lungnateymi == null || Insulinteymi == null || Krabbameinsteymi == null || Parkinsonsteymi == null)
                {
                    throw new Exception("Teams must be initialized before adding HealthcareWorkers.");
                }
                context.HealthcareWorkers.AddRange(
                    new HealthcareWorker { Name = "Sigurmundur Davíð", Phone = "1234567", Status = "Active", Teams = new List<Team> {Lungnateymi, Insulinteymi} },
                    new HealthcareWorker { Name = "Freyr Björgvin", Phone = "1234567", Status = "Active",Teams = new List<Team> {Lungnateymi, Krabbameinsteymi} },
                    new HealthcareWorker { Name = "Friðleif Eva", Phone = "1234567", Status = "Active",Teams = new List<Team> {Sárateymi, Parkinsonsteymi}},
                    new HealthcareWorker { Name = "Indiana Sigrún", Phone = "1234567", Status = "Active",Teams = new List<Team> {Parkinsonsteymi, Krabbameinsteymi} },
                    new HealthcareWorker { Name = "Sveinveig Guðrún", Phone = "1234567", Status = "Active",Teams = new List<Team> {Sárateymi, Insulinteymi} },
                    new HealthcareWorker { Name = "Ingvi Ólafur", Phone = "1234567", Status = "Active",Teams = new List<Team> {Lungnateymi, Krabbameinsteymi}},
                    new HealthcareWorker { Name = "Sigurður Bjarki", Phone = "1234567", Status = "Active",Teams = new List<Team> {Parkinsonsteymi, Insulinteymi} },
                    new HealthcareWorker { Name = "Sólveig Björk", Phone = "1234567", Status = "Active",Teams = new List<Team> {Lungnateymi, Krabbameinsteymi} },
                    new HealthcareWorker { Name = "Stefán Bjarnarson", Phone = "1234567", Status = "Active",Teams = new List<Team> {Sárateymi, Parkinsonsteymi} },
                    new HealthcareWorker { Name = "Katla Ragnarsdóttir", Phone = "1234567", Status = "Active",Teams = new List<Team> {Lungnateymi, Krabbameinsteymi} },
                    new HealthcareWorker { Name = "Felix Gunnarsson", Phone = "1234567", Status = "Active",Teams = new List<Team> {Parkinsonsteymi, Insulinteymi} },
                    new HealthcareWorker { Name = "Erla Elínardóttir", Phone = "1234567", Status = "Active",Teams = new List<Team> {Lungnateymi, Krabbameinsteymi} },
                    new HealthcareWorker { Name = "Sonja Ketilsdóttir", Phone = "1234567", Status = "Active",Teams = new List<Team> {Sárateymi, Insulinteymi} },
                    new HealthcareWorker { Name = "Hallgerður Guðmundsdóttir", Phone = "1234567", Status = "Active",Teams = new List<Team> {Lungnateymi, Krabbameinsteymi} },
                    new HealthcareWorker { Name = "Karl Óskarsson'", Phone = "1234567", Status = "Active",Teams = new List<Team> {Sárateymi, Parkinsonsteymi} },
                    new HealthcareWorker { Name = "Friðgeir Lárusson", Phone = "1234567", Status = "Active",Teams = new List<Team> {Lungnateymi, Krabbameinsteymi} },
                    new HealthcareWorker { Name = "Unnar Sturluson", Phone = "1234567", Status = "Active",Teams = new List<Team> {Sárateymi, Insulinteymi} },
                    new HealthcareWorker { Name = "Brynjar Már Halldórsson", Phone = "1234567", Status = "Active",Teams = new List<Team> {Parkinsonsteymi, Krabbameinsteymi} },
                    new HealthcareWorker { Name = "Jón Hafsteinn Einarsson", Phone = "1234567", Status = "Active",Teams = new List<Team> {Sárateymi, Insulinteymi} },
                    new HealthcareWorker { Name = "Kári Gautason", Phone = "1234567", Status = "Active",Teams = new List<Team> {Lungnateymi, Parkinsonsteymi} }
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
                    new BloodPressureRange { PatientID = 6 },
                    new BloodPressureRange { PatientID = 7 },
                    new BloodPressureRange { PatientID = 8 },
                    new BloodPressureRange { PatientID = 9 },
                    new BloodPressureRange { PatientID = 10 },
                    new BloodPressureRange { PatientID = 11 },
                    new BloodPressureRange { PatientID = 12 },
                    new BloodPressureRange { PatientID = 13 },
                    new BloodPressureRange { PatientID = 14 },
                    new BloodPressureRange { PatientID = 15 },
                    new BloodPressureRange { PatientID = 16 },
                    new BloodPressureRange { PatientID = 17 },
                    new BloodPressureRange { PatientID = 18 },
                    new BloodPressureRange { PatientID = 19 },
                    new BloodPressureRange { PatientID = 20 },
                    new BloodPressureRange { PatientID = 21 }
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
                    new BloodSugarRange { PatientID = 6 },
                    new BloodSugarRange { PatientID = 7 },
                    new BloodSugarRange { PatientID = 8 },
                    new BloodSugarRange { PatientID = 9 },
                    new BloodSugarRange { PatientID = 10 },
                    new BloodSugarRange { PatientID = 11 },
                    new BloodSugarRange { PatientID = 12 },
                    new BloodSugarRange { PatientID = 13 },
                    new BloodSugarRange { PatientID = 14 },
                    new BloodSugarRange { PatientID = 15 },
                    new BloodSugarRange { PatientID = 16 },
                    new BloodSugarRange { PatientID = 17 },
                    new BloodSugarRange { PatientID = 18 },
                    new BloodSugarRange { PatientID = 19 },
                    new BloodSugarRange { PatientID = 20 },
                    new BloodSugarRange { PatientID = 21 }
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
                    new BodyTemperatureRange { PatientID = 6 },
                    new BodyTemperatureRange { PatientID = 7 },
                    new BodyTemperatureRange { PatientID = 8 },
                    new BodyTemperatureRange { PatientID = 9 },
                    new BodyTemperatureRange { PatientID = 10 },
                    new BodyTemperatureRange { PatientID = 11 },
                    new BodyTemperatureRange { PatientID = 12 },
                    new BodyTemperatureRange { PatientID = 13 },
                    new BodyTemperatureRange { PatientID = 14 },
                    new BodyTemperatureRange { PatientID = 15 },
                    new BodyTemperatureRange { PatientID = 16 },
                    new BodyTemperatureRange { PatientID = 17 },
                    new BodyTemperatureRange { PatientID = 18 },
                    new BodyTemperatureRange { PatientID = 19 },
                    new BodyTemperatureRange { PatientID = 20 },
                    new BodyTemperatureRange { PatientID = 21 }
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
                    new BodyWeightRange { PatientID = 6 },
                    new BodyWeightRange { PatientID = 7 },
                    new BodyWeightRange { PatientID = 8 },
                    new BodyWeightRange { PatientID = 9 },
                    new BodyWeightRange { PatientID = 10 },
                    new BodyWeightRange { PatientID = 11 },
                    new BodyWeightRange { PatientID = 12 },
                    new BodyWeightRange { PatientID = 13 },
                    new BodyWeightRange { PatientID = 14 },
                    new BodyWeightRange { PatientID = 15 },
                    new BodyWeightRange { PatientID = 16 },
                    new BodyWeightRange { PatientID = 17 },
                    new BodyWeightRange { PatientID = 18 },
                    new BodyWeightRange { PatientID = 19 },
                    new BodyWeightRange { PatientID = 20 },
                    new BodyWeightRange { PatientID = 21 }
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
                    new OxygenSaturationRange { PatientID = 6 },
                    new OxygenSaturationRange { PatientID = 7 },
                    new OxygenSaturationRange { PatientID = 8 },
                    new OxygenSaturationRange { PatientID = 9 },
                    new OxygenSaturationRange { PatientID = 10 },
                    new OxygenSaturationRange { PatientID = 11 },
                    new OxygenSaturationRange { PatientID = 12 },
                    new OxygenSaturationRange { PatientID = 13 },
                    new OxygenSaturationRange { PatientID = 14 },
                    new OxygenSaturationRange { PatientID = 15 },
                    new OxygenSaturationRange { PatientID = 16 },
                    new OxygenSaturationRange { PatientID = 17 },
                    new OxygenSaturationRange { PatientID = 18 },
                    new OxygenSaturationRange { PatientID = 19 },
                    new OxygenSaturationRange { PatientID = 20 },
                    new OxygenSaturationRange { PatientID = 21 }
                );
            }
            if (!context.Bloodsugars.Any())
            {
                context.Bloodsugars.AddRange(
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 3.5f, Date = DateTime.UtcNow.AddDays(-63), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 3.1f, Date = DateTime.UtcNow.AddDays(-62), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 4.0f, Date = DateTime.UtcNow.AddDays(-61), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 3.8f, Date = DateTime.UtcNow.AddDays(-60), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 3.3f, Date = DateTime.UtcNow.AddDays(-59), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 4.0f, Date = DateTime.UtcNow.AddDays(-58), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 2.9f, Date = DateTime.UtcNow.AddDays(-57), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 4.0f, Date = DateTime.UtcNow.AddDays(-56), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 3.7f, Date = DateTime.UtcNow.AddDays(-55), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 3.9f, Date = DateTime.UtcNow.AddDays(-54), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 3.5f, Date = DateTime.UtcNow.AddDays(-53), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 3.6f, Date = DateTime.UtcNow.AddDays(-52), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 4.1f, Date = DateTime.UtcNow.AddDays(-51), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 3.8f, Date = DateTime.UtcNow.AddDays(-50), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 3.6f, Date = DateTime.UtcNow.AddDays(-49), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 3.1f, Date = DateTime.UtcNow.AddDays(-48), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 2.6f, Date = DateTime.UtcNow.AddDays(-47), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 4.6f, Date = DateTime.UtcNow.AddDays(-46), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 2.2f, Date = DateTime.UtcNow.AddDays(-45), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 2.5f, Date = DateTime.UtcNow.AddDays(-44), Status = "Raised" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 2.6f, Date = DateTime.UtcNow.AddDays(-43), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 3.0f, Date = DateTime.UtcNow.AddDays(-42), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 3.5f, Date = DateTime.UtcNow.AddDays(-41), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 2.7f, Date = DateTime.UtcNow.AddDays(-40), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 3.2f, Date = DateTime.UtcNow.AddDays(-39), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 2.8f, Date = DateTime.UtcNow.AddDays(-38), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 2.7f, Date = DateTime.UtcNow.AddDays(-37), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 3.0f, Date = DateTime.UtcNow.AddDays(-36), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 3.1f, Date = DateTime.UtcNow.AddDays(-35), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 2.8f, Date = DateTime.UtcNow.AddDays(-34), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 5.6f, Date = DateTime.UtcNow.AddDays(-33), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 2.9f, Date = DateTime.UtcNow.AddDays(-32), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 4.1f, Date = DateTime.UtcNow.AddDays(-31), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 4.2f, Date = DateTime.UtcNow.AddDays(-30), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 3.9f, Date = DateTime.UtcNow.AddDays(-29), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 2.6f, Date = DateTime.UtcNow.AddDays(-28), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 2.9f, Date = DateTime.UtcNow.AddDays(-27), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 3.0f, Date = DateTime.UtcNow.AddDays(-26), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 3.7f, Date = DateTime.UtcNow.AddDays(-25), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 3.6f, Date = DateTime.UtcNow.AddDays(-24), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 3.1f, Date = DateTime.UtcNow.AddDays(-23), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 3.3f, Date = DateTime.UtcNow.AddDays(-22), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 3.2f, Date = DateTime.UtcNow.AddDays(-21), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 2.9f, Date = DateTime.UtcNow.AddDays(-20), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 4.1f, Date = DateTime.UtcNow.AddDays(-19), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 3.6f, Date = DateTime.UtcNow.AddDays(-18), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 2.1f, Date = DateTime.UtcNow.AddDays(-17), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 4.5f, Date = DateTime.UtcNow.AddDays(-16), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 3.5f, Date = DateTime.UtcNow.AddDays(-15), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 6.5f, Date = DateTime.UtcNow.AddDays(-14), Status = "Raised" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 7.5f, Date = DateTime.UtcNow.AddDays(-13), Status = "High" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 5.5f, Date = DateTime.UtcNow.AddDays(-12), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 6.5f, Date = DateTime.UtcNow.AddDays(-11), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 8.5f, Date = DateTime.UtcNow.AddDays(-10), Status = "High" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 5.5f, Date = DateTime.UtcNow.AddDays(-9), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 6.5f, Date = DateTime.UtcNow.AddDays(-8), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 4.5f, Date = DateTime.UtcNow.AddDays(-7), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 5.5f, Date = DateTime.UtcNow.AddDays(-6), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 6.1f, Date = DateTime.UtcNow.AddDays(-5), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 6.8f, Date = DateTime.UtcNow.AddDays(-4), Status = "High" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 5.5f, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 6.5f, Date = DateTime.UtcNow.AddDays(-2), Status = "Normal" },
                    new Bloodsugar { PatientID = 1, BloodsugarLevel = 4.5f, Date = DateTime.UtcNow.AddDays(-1), Status = "Normal" },
                    
                    
                    new Bloodsugar { PatientID = 3, BloodsugarLevel = 5.5f, Date = DateTime.UtcNow.AddDays(-33), Status = "Normal" },
                    // new Bloodsugar { PatientID = 3, BloodsugarLevel = 6.5f, Date = DateTime.UtcNow.AddDays(-21), Status = "Raised" },
                    // new Bloodsugar { PatientID = 3, BloodsugarLevel = 8.5f, Date = DateTime.UtcNow.AddDays(-463), Status = "High" },
                    new Bloodsugar { PatientID = 3, BloodsugarLevel = 5.5f, Date = DateTime.UtcNow.AddDays(-15), Status = "Normal" },
                    // new Bloodsugar { PatientID = 3, BloodsugarLevel = 6.5f, Date = DateTime.UtcNow.AddDays(-14), Status = "Raised" },
                    // new Bloodsugar { PatientID = 3, BloodsugarLevel = 7.5f, Date = DateTime.UtcNow.AddDays(-13), Status = "High" },
                    new Bloodsugar { PatientID = 3, BloodsugarLevel = 5.5f, Date = DateTime.UtcNow.AddDays(-12), Status = "Normal" },
                    new Bloodsugar { PatientID = 3, BloodsugarLevel = 6.5f, Date = DateTime.UtcNow.AddDays(-11), Status = "Normal" },
                    // new Bloodsugar { PatientID = 3, BloodsugarLevel = 8.5f, Date = DateTime.UtcNow.AddDays(-10), Status = "High" },
                    new Bloodsugar { PatientID = 3, BloodsugarLevel = 5.5f, Date = DateTime.UtcNow.AddDays(-9), Status = "Normal" },
                    new Bloodsugar { PatientID = 3, BloodsugarLevel = 6.5f, Date = DateTime.UtcNow.AddDays(-8), Status = "Normal" },
                    new Bloodsugar { PatientID = 3, BloodsugarLevel = 4.5f, Date = DateTime.UtcNow.AddDays(-7), Status = "Normal" },
                    new Bloodsugar { PatientID = 3, BloodsugarLevel = 5.5f, Date = DateTime.UtcNow.AddDays(-6), Status = "Normal" },
                    new Bloodsugar { PatientID = 3, BloodsugarLevel = 6.5f, Date = DateTime.UtcNow.AddDays(-5), Status = "Normal" },
                    // new Bloodsugar { PatientID = 3, BloodsugarLevel = 8.5f, Date = DateTime.UtcNow.AddDays(-4), Status = "High" },
                    new Bloodsugar { PatientID = 3, BloodsugarLevel = 5.5f, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal" },
                    // new Bloodsugar { PatientID = 3, BloodsugarLevel = 10.5f, Date = DateTime.UtcNow.AddDays(-2), Status = "High" },
                    // new Bloodsugar { PatientID = 3, BloodsugarLevel = 9.5f, Date = DateTime.UtcNow.AddDays(-1), Status = "High" },
                    
                    new Bloodsugar { PatientID = 4, BloodsugarLevel = 5.5f, Date = DateTime.UtcNow.AddDays(-436), Status = "Normal" },
                    new Bloodsugar { PatientID = 4, BloodsugarLevel = 6.5f, Date = DateTime.UtcNow.AddDays(-287), Status = "Raised" },
                    new Bloodsugar { PatientID = 4, BloodsugarLevel = 8.5f, Date = DateTime.UtcNow.AddDays(-166), Status = "High" },
                    
                    new Bloodsugar { PatientID = 5, BloodsugarLevel = 5.5f, Date = DateTime.UtcNow.AddDays(-366), Status = "Normal" },
                    new Bloodsugar { PatientID = 5, BloodsugarLevel = 6.5f, Date = DateTime.UtcNow.AddDays(-246), Status = "Raised" },
                    new Bloodsugar { PatientID = 5, BloodsugarLevel = 8.5f, Date = DateTime.UtcNow.AddDays(-64), Status = "High" },
                    
                    new Bloodsugar { PatientID = 6, BloodsugarLevel = 5.5f, Date = DateTime.UtcNow.AddDays(-346), Status = "Normal" },
                    new Bloodsugar { PatientID = 6, BloodsugarLevel = 6.5f, Date = DateTime.UtcNow.AddDays(-2253), Status = "Raised" },
                    new Bloodsugar { PatientID = 6, BloodsugarLevel = 10.5f, Date = DateTime.UtcNow.AddDays(-5), Status = "High" },

                    new Bloodsugar { PatientID = 7, BloodsugarLevel = 5.5f, Date = DateTime.UtcNow.AddDays(-66), Status = "Normal" },
                    new Bloodsugar { PatientID = 7, BloodsugarLevel = 6.5f, Date = DateTime.UtcNow.AddDays(-55), Status = "Raised" },
                    new Bloodsugar { PatientID = 7, BloodsugarLevel = 9.5f, Date = DateTime.UtcNow.AddDays(-50), Status = "High" },

                    new Bloodsugar { PatientID = 8, BloodsugarLevel = 5.5f, Date = DateTime.UtcNow.AddDays(-55), Status = "Normal" },
                    new Bloodsugar { PatientID = 8, BloodsugarLevel = 6.5f, Date = DateTime.UtcNow.AddDays(-13), Status = "Raised" },
                    new Bloodsugar { PatientID = 8, BloodsugarLevel = 11.5f, Date = DateTime.UtcNow.AddDays(-5), Status = "High" },
                    
                    new Bloodsugar { PatientID = 9, BloodsugarLevel = 5.5f, Date = DateTime.UtcNow.AddDays(-12), Status = "Normal" },
                    new Bloodsugar { PatientID = 9, BloodsugarLevel = 7.5f, Date = DateTime.UtcNow.AddDays(-33), Status = "Raised" },
                    new Bloodsugar { PatientID = 9, BloodsugarLevel = 10.5f, Date = DateTime.UtcNow.AddDays(-5), Status = "High" },

                    new Bloodsugar { PatientID = 10, BloodsugarLevel = 5.5f, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal" },
                    new Bloodsugar { PatientID = 10, BloodsugarLevel = 7.1f, Date = DateTime.UtcNow.AddDays(-2), Status = "Raised" },
                    new Bloodsugar { PatientID = 10, BloodsugarLevel = 8.9f, Date = DateTime.UtcNow.AddDays(-1), Status = "High" },

                    new Bloodsugar { PatientID = 11, BloodsugarLevel = 5.5f, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal" },
                    new Bloodsugar { PatientID = 11, BloodsugarLevel = 6.5f, Date = DateTime.UtcNow.AddDays(-5), Status = "Raised" },
                    new Bloodsugar { PatientID = 11, BloodsugarLevel = 9.5f, Date = DateTime.UtcNow.AddDays(-6), Status = "High" },

                    new Bloodsugar { PatientID = 12, BloodsugarLevel = 5.5f, Date = DateTime.UtcNow.AddDays(-1), Status = "Normal" },
                    new Bloodsugar { PatientID = 12, BloodsugarLevel = 6.5f, Date = DateTime.UtcNow.AddDays(-7), Status = "Raised" },
                    new Bloodsugar { PatientID = 12, BloodsugarLevel = 9.5f, Date = DateTime.UtcNow.AddDays(-5), Status = "High" },

                    new Bloodsugar { PatientID = 13, BloodsugarLevel = 5.5f, Date = DateTime.UtcNow.AddDays(-2), Status = "Normal" },
                    new Bloodsugar { PatientID = 13, BloodsugarLevel = 6.5f, Date = DateTime.UtcNow.AddDays(-1), Status = "Raised" },
                    new Bloodsugar { PatientID = 13, BloodsugarLevel = 11.5f, Date = DateTime.UtcNow.AddDays(-3), Status = "High" },

                    new Bloodsugar { PatientID = 14, BloodsugarLevel = 5.5f, Date = DateTime.UtcNow.AddDays(-2), Status = "Normal" },
                    new Bloodsugar { PatientID = 14, BloodsugarLevel = 6.5f, Date = DateTime.UtcNow.AddDays(-5), Status = "Raised" },
                    new Bloodsugar { PatientID = 14, BloodsugarLevel = 10.5f, Date = DateTime.UtcNow.AddDays(-1), Status = "High" },

                    new Bloodsugar { PatientID = 15, BloodsugarLevel = 5.5f, Date = DateTime.UtcNow.AddDays(-13), Status = "Normal" },
                    new Bloodsugar { PatientID = 15, BloodsugarLevel = 6.5f, Date = DateTime.UtcNow.AddDays(-52), Status = "Raised" },
                    new Bloodsugar { PatientID = 15, BloodsugarLevel = 11.5f, Date = DateTime.UtcNow.AddDays(-11), Status = "High" },

                    new Bloodsugar { PatientID = 16, BloodsugarLevel = 5.5f, Date = DateTime.UtcNow.AddDays(-6), Status = "Normal" },
                    new Bloodsugar { PatientID = 16, BloodsugarLevel = 6.5f, Date = DateTime.UtcNow.AddDays(-23), Status = "Raised" },
                    new Bloodsugar { PatientID = 16, BloodsugarLevel = 9.5f, Date = DateTime.UtcNow.AddDays(-8), Status = "High" },

                    new Bloodsugar { PatientID = 17, BloodsugarLevel = 5.5f, Date = DateTime.UtcNow.AddDays(-2), Status = "Normal" },
                    new Bloodsugar { PatientID = 17, BloodsugarLevel = 6.5f, Date = DateTime.UtcNow.AddDays(-1), Status = "Raised" },
                    new Bloodsugar { PatientID = 17, BloodsugarLevel = 8.5f, Date = DateTime.UtcNow.AddDays(-6), Status = "High" },

                    new Bloodsugar { PatientID = 18, BloodsugarLevel = 5.5f, Date = DateTime.UtcNow.AddDays(-7), Status = "Normal" },
                    new Bloodsugar { PatientID = 18, BloodsugarLevel = 6.5f, Date = DateTime.UtcNow.AddDays(-1), Status = "Raised" },
                    new Bloodsugar { PatientID = 18, BloodsugarLevel = 8.5f, Date = DateTime.UtcNow.AddDays(-3), Status = "High" },

                    new Bloodsugar { PatientID = 19, BloodsugarLevel = 5.5f, Date = DateTime.UtcNow.AddDays(-7), Status = "Normal" },
                    new Bloodsugar { PatientID = 19, BloodsugarLevel = 7.0f, Date = DateTime.UtcNow.AddDays(-6), Status = "Raised" },
                    new Bloodsugar { PatientID = 19, BloodsugarLevel = 9.5f, Date = DateTime.UtcNow.AddDays(-3), Status = "High" },

                    new Bloodsugar { PatientID = 20, BloodsugarLevel = 5.5f, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal" },
                    new Bloodsugar { PatientID = 20, BloodsugarLevel = 6.5f, Date = DateTime.UtcNow.AddDays(-2), Status = "Raised" },
                    new Bloodsugar { PatientID = 20, BloodsugarLevel = 8.5f, Date = DateTime.UtcNow.AddDays(-6), Status = "High" }
                );
            }

            if (!context.BloodPressures.Any())
            {
                context.BloodPressures.AddRange(
                   
                    
                    new BloodPressure { PatientID = 3, Systolic = 120, Diastolic = 80, Date = DateTime.UtcNow.AddDays(-28), MeasuredHand = "Right", BodyPosition = "Laying", Pulse = 60, Status = "Normal"},
                    // new BloodPressure { PatientID = 3, Systolic = 130, Diastolic = 90, Date = DateTime.UtcNow.AddDays(-26), MeasuredHand = "Left", BodyPosition = "Sitting", Pulse = 75, Status = "Raised"},
                    // new BloodPressure { PatientID = 3, Systolic = 150, Diastolic = 100, Date = DateTime.UtcNow.AddDays(-15), MeasuredHand = "Left", BodyPosition = "Sitting", Pulse = 74, Status = "High"},
                    new BloodPressure { PatientID = 3, Systolic = 120, Diastolic = 80, Date = DateTime.UtcNow.AddDays(-15), MeasuredHand = "Left", BodyPosition = "Sitting", Pulse = 88, Status = "Normal"},
                    // new BloodPressure { PatientID = 3, Systolic = 130, Diastolic = 90, Date = DateTime.UtcNow.AddDays(-14), MeasuredHand = "Left", BodyPosition = "Sitting", Pulse = 72, Status = "Raised"},
                    // new BloodPressure { PatientID = 3, Systolic = 150, Diastolic = 100, Date = DateTime.UtcNow.AddDays(-13), MeasuredHand = "Left", BodyPosition = "Sitting", Pulse = 77, Status = "High"},
                    new BloodPressure { PatientID = 3, Systolic = 120, Diastolic = 80, Date = DateTime.UtcNow.AddDays(-12), MeasuredHand = "Left", BodyPosition = "Sitting", Pulse = 66, Status = "Normal"},
                    new BloodPressure { PatientID = 3, Systolic = 130, Diastolic = 90, Date = DateTime.UtcNow.AddDays(-11), MeasuredHand = "Left", BodyPosition = "Sitting", Pulse = 99, Status = "Normal"},
                    // new BloodPressure { PatientID = 3, Systolic = 150, Diastolic = 100, Date = DateTime.UtcNow.AddDays(-10), MeasuredHand = "Left", BodyPosition = "Sitting", Pulse = 100, Status = "High"},
                    // new BloodPressure { PatientID = 3, Systolic = 130, Diastolic = 80, Date = DateTime.UtcNow.AddDays(-9), MeasuredHand = "Left", BodyPosition = "Sitting", Pulse = 85, Status = "Raised"},
                    new BloodPressure { PatientID = 3, Systolic = 110, Diastolic = 77, Date = DateTime.UtcNow.AddDays(-8), MeasuredHand = "Left", BodyPosition = "Sitting", Pulse = 68, Status = "Normal"},
                    new BloodPressure { PatientID = 3, Systolic = 100, Diastolic = 77, Date = DateTime.UtcNow.AddDays(-7), MeasuredHand = "Left", BodyPosition = "Sitting", Pulse = 80, Status = "Normal"},
                    new BloodPressure { PatientID = 3, Systolic = 120, Diastolic = 80, Date = DateTime.UtcNow.AddDays(-6), MeasuredHand = "Left", BodyPosition = "Sitting", Pulse = 67, Status = "Normal"},
                    // new BloodPressure { PatientID = 3, Systolic = 130, Diastolic = 90, Date = DateTime.UtcNow.AddDays(-5), MeasuredHand = "Left", BodyPosition = "Sitting", Pulse = 74, Status = "Raised"},
                    // new BloodPressure { PatientID = 3, Systolic = 150, Diastolic = 100, Date = DateTime.UtcNow.AddDays(-4), MeasuredHand = "Left", BodyPosition = "Sitting", Pulse = 76, Status = "High"},
                    new BloodPressure { PatientID = 3, Systolic = 110, Diastolic = 80, Date = DateTime.UtcNow.AddDays(-3), MeasuredHand = "Left", BodyPosition = "Sitting", Pulse = 77, Status = "Normal"},
                    new BloodPressure { PatientID = 3, Systolic = 110, Diastolic = 70, Date = DateTime.UtcNow.AddDays(-2), MeasuredHand = "Left", BodyPosition = "Sitting", Pulse = 68, Status = "Normal"},
                    new BloodPressure { PatientID = 3, Systolic = 100, Diastolic = 70, Date = DateTime.UtcNow.AddDays(-1), MeasuredHand = "Left", BodyPosition = "Sitting", Pulse = 82, Status = "Normal"},
                    
                    new BloodPressure { PatientID = 4, Systolic = 120, Diastolic = 80, Date = DateTime.UtcNow.AddDays(-4), MeasuredHand = "Right", BodyPosition = "Laying", Pulse = 70, Status = "Normal"},
                    new BloodPressure { PatientID = 4, Systolic = 130, Diastolic = 90, Date = DateTime.UtcNow.AddDays(-55), MeasuredHand = "Left", BodyPosition = "Laying", Pulse = 70, Status = "Raised"},
                    new BloodPressure { PatientID = 4, Systolic = 150, Diastolic = 100, Date = DateTime.UtcNow.AddDays(-321), MeasuredHand = "Left", BodyPosition = "Sitting", Pulse = 70, Status = "High"},
                    
                    new BloodPressure { PatientID = 5, Systolic = 120, Diastolic = 80, Date = DateTime.UtcNow.AddDays(-3), MeasuredHand = "Right", BodyPosition = "Sitting", Pulse = 70, Status = "Normal"},
                    new BloodPressure { PatientID = 5, Systolic = 130, Diastolic = 90, Date = DateTime.UtcNow.AddDays(-332), MeasuredHand = "Right", BodyPosition = "Sitting", Pulse = 70, Status = "Raised"},
                    new BloodPressure { PatientID = 5, Systolic = 150, Diastolic = 100, Date = DateTime.UtcNow.AddDays(-232), MeasuredHand = "Left", BodyPosition = "Laying", Pulse = 70, Status = "High"},
                    
                    new BloodPressure { PatientID = 6, Systolic = 120, Diastolic = 80, Date = DateTime.UtcNow.AddDays(-55), MeasuredHand = "Right", BodyPosition = "Sitting", Pulse = 70, Status = "Normal"},
                    new BloodPressure { PatientID = 6, Systolic = 130, Diastolic = 90, Date = DateTime.UtcNow.AddDays(-23), MeasuredHand = "Right", BodyPosition = "Laying", Pulse = 70, Status = "Raised"},
                    new BloodPressure { PatientID = 6, Systolic = 150, Diastolic = 100, Date = DateTime.UtcNow.AddDays(-5), MeasuredHand = "Right", BodyPosition = "Sitting", Pulse = 70, Status = "High"},

                    new BloodPressure { PatientID = 7, Systolic = 120, Diastolic = 80, Date = DateTime.UtcNow.AddDays(-2), MeasuredHand = "Left", BodyPosition = "Sitting", Pulse = 70, Status = "Normal"},
                    new BloodPressure { PatientID = 7, Systolic = 120, Diastolic = 80, Date = DateTime.UtcNow.AddDays(-1), MeasuredHand = "Left", BodyPosition = "Sitting", Pulse = 70, Status = "Normal"},
                    new BloodPressure { PatientID = 7, Systolic = 120, Diastolic = 80, Date = DateTime.UtcNow.AddDays(-5), MeasuredHand = "Left", BodyPosition = "Sitting", Pulse = 70, Status = "Normal"},

                    new BloodPressure { PatientID = 8, Systolic = 120, Diastolic = 80, Date = DateTime.UtcNow.AddDays(-3), MeasuredHand = "Right", BodyPosition = "Sitting", Pulse = 70, Status = "Normal"},
                    new BloodPressure { PatientID = 8, Systolic = 130, Diastolic = 90, Date = DateTime.UtcNow.AddDays(-4), MeasuredHand = "Left", BodyPosition = "Laying", Pulse = 70, Status = "Raised"},
                    new BloodPressure { PatientID = 8, Systolic = 150, Diastolic = 100, Date = DateTime.UtcNow.AddDays(-5), MeasuredHand = "Left", BodyPosition = "Sitting", Pulse = 70, Status = "High"},

                    new BloodPressure { PatientID = 9, Systolic = 120, Diastolic = 80, Date = DateTime.UtcNow.AddDays(-3), MeasuredHand = "Left", BodyPosition = "Sitting", Pulse = 70, Status = "Normal"},
                    new BloodPressure { PatientID = 9, Systolic = 130, Diastolic = 90, Date = DateTime.UtcNow.AddDays(-4), MeasuredHand = "Left", BodyPosition = "Laying", Pulse = 70, Status = "Raised"},
                    new BloodPressure { PatientID = 9, Systolic = 150, Diastolic = 100, Date = DateTime.UtcNow.AddDays(-5), MeasuredHand = "Left", BodyPosition = "Sitting", Pulse = 70, Status = "High"},

                    new BloodPressure { PatientID = 10, Systolic = 120, Diastolic = 80, Date = DateTime.UtcNow.AddDays(-3), MeasuredHand = "Left", BodyPosition = "Sitting", Pulse = 70, Status = "Normal"},
                    new BloodPressure { PatientID = 10, Systolic = 130, Diastolic = 90, Date = DateTime.UtcNow.AddDays(-4), MeasuredHand = "Right", BodyPosition = "Laying", Pulse = 70, Status = "Raised"},
                    new BloodPressure { PatientID = 10, Systolic = 150, Diastolic = 100, Date = DateTime.UtcNow.AddDays(-5), MeasuredHand = "Left", BodyPosition = "Sitting", Pulse = 70, Status = "High"},
                    
                    new BloodPressure { PatientID = 11, Systolic = 120, Diastolic = 80, Date = DateTime.UtcNow.AddDays(-3), MeasuredHand = "Right", BodyPosition = "Sitting", Pulse = 70, Status = "Normal"},
                    new BloodPressure { PatientID = 11, Systolic = 130, Diastolic = 90, Date = DateTime.UtcNow.AddDays(-4), MeasuredHand = "Left", BodyPosition = "Laying", Pulse = 70, Status = "Raised"},
                    new BloodPressure { PatientID = 11, Systolic = 150, Diastolic = 100, Date = DateTime.UtcNow.AddDays(-5), MeasuredHand = "Right", BodyPosition = "Sitting", Pulse = 70, Status = "High"},
                    
                    new BloodPressure { PatientID = 12, Systolic = 120, Diastolic = 80, Date = DateTime.UtcNow.AddDays(-3), MeasuredHand = "Left", BodyPosition = "Sitting", Pulse = 70, Status = "Normal"},
                    new BloodPressure { PatientID = 12, Systolic = 130, Diastolic = 90, Date = DateTime.UtcNow.AddDays(-4), MeasuredHand = "Right", BodyPosition = "Laying", Pulse = 70, Status = "Raised"},
                    new BloodPressure { PatientID = 12, Systolic = 150, Diastolic = 100, Date = DateTime.UtcNow.AddDays(-5), MeasuredHand = "Left", BodyPosition = "Sitting", Pulse = 70, Status = "High"},

                    new BloodPressure { PatientID = 13, Systolic = 120, Diastolic = 80, Date = DateTime.UtcNow.AddDays(-3), MeasuredHand = "Right", BodyPosition = "Sitting", Pulse = 70, Status = "Normal"},
                    new BloodPressure { PatientID = 13, Systolic = 130, Diastolic = 90, Date = DateTime.UtcNow.AddDays(-4), MeasuredHand = "Right", BodyPosition = "Laying", Pulse = 70, Status = "Raised"},
                    new BloodPressure { PatientID = 13, Systolic = 150, Diastolic = 100, Date = DateTime.UtcNow.AddDays(-5), MeasuredHand = "Left", BodyPosition = "Sitting", Pulse = 70, Status = "High"},

                    new BloodPressure { PatientID = 14, Systolic = 120, Diastolic = 80, Date = DateTime.UtcNow.AddDays(-3), MeasuredHand = "Right", BodyPosition = "Sitting", Pulse = 70, Status = "Normal"},
                    new BloodPressure { PatientID = 14, Systolic = 130, Diastolic = 90, Date = DateTime.UtcNow.AddDays(-4), MeasuredHand = "Right", BodyPosition = "Laying", Pulse = 70, Status = "Raised"},
                    new BloodPressure { PatientID = 14, Systolic = 150, Diastolic = 100, Date = DateTime.UtcNow.AddDays(-5), MeasuredHand = "Right", BodyPosition = "Sitting", Pulse = 70, Status = "High"},

                    new BloodPressure { PatientID = 15, Systolic = 120, Diastolic = 80, Date = DateTime.UtcNow.AddDays(-3), MeasuredHand = "Right", BodyPosition = "Sitting", Pulse = 70, Status = "Normal"},
                    new BloodPressure { PatientID = 15, Systolic = 130, Diastolic = 90, Date = DateTime.UtcNow.AddDays(-4), MeasuredHand = "Right", BodyPosition = "Laying", Pulse = 70, Status = "Raised"},
                    new BloodPressure { PatientID = 15, Systolic = 150, Diastolic = 100, Date = DateTime.UtcNow.AddDays(-5), MeasuredHand = "Right", BodyPosition = "Sitting", Pulse = 70, Status = "High"},

                    new BloodPressure { PatientID = 16, Systolic = 120, Diastolic = 80, Date = DateTime.UtcNow.AddDays(-3), MeasuredHand = "Left", BodyPosition = "Sitting", Pulse = 70, Status = "Normal"},
                    new BloodPressure { PatientID = 16, Systolic = 130, Diastolic = 90, Date = DateTime.UtcNow.AddDays(-4), MeasuredHand = "Left", BodyPosition = "Laying", Pulse = 70, Status = "Raised"},
                    new BloodPressure { PatientID = 16, Systolic = 150, Diastolic = 100, Date = DateTime.UtcNow.AddDays(-5), MeasuredHand = "Left", BodyPosition = "Sitting", Pulse = 70, Status = "High"},

                    new BloodPressure { PatientID = 17, Systolic = 120, Diastolic = 80, Date = DateTime.UtcNow.AddDays(-3), MeasuredHand = "Right", BodyPosition = "Sitting", Pulse = 70, Status = "Normal"},
                    new BloodPressure { PatientID = 17, Systolic = 130, Diastolic = 90, Date = DateTime.UtcNow.AddDays(-4), MeasuredHand = "Right", BodyPosition = "Laying", Pulse = 70, Status = "Raised"},
                    new BloodPressure { PatientID = 17, Systolic = 150, Diastolic = 100, Date = DateTime.UtcNow.AddDays(-5), MeasuredHand = "Left", BodyPosition = "Sitting", Pulse = 70, Status = "High"},

                    new BloodPressure { PatientID = 18, Systolic = 120, Diastolic = 80, Date = DateTime.UtcNow.AddDays(-3), MeasuredHand = "Left", BodyPosition = "Sitting", Pulse = 70, Status = "Normal"},
                    new BloodPressure { PatientID = 18, Systolic = 130, Diastolic = 90, Date = DateTime.UtcNow.AddDays(-4), MeasuredHand = "Left", BodyPosition = "Laying", Pulse = 70, Status = "Raised"},
                    new BloodPressure { PatientID = 18, Systolic = 150, Diastolic = 100, Date = DateTime.UtcNow.AddDays(-5), MeasuredHand = "Right", BodyPosition = "Sitting", Pulse = 70, Status = "High"},

                    new BloodPressure { PatientID = 19, Systolic = 120, Diastolic = 80, Date = DateTime.UtcNow.AddDays(-3), MeasuredHand = "Right", BodyPosition = "Sitting", Pulse = 70, Status = "Normal"},
                    new BloodPressure { PatientID = 19, Systolic = 130, Diastolic = 90, Date = DateTime.UtcNow.AddDays(-4), MeasuredHand = "Right", BodyPosition = "Laying", Pulse = 70, Status = "Raised"},
                    new BloodPressure { PatientID = 19, Systolic = 150, Diastolic = 100, Date = DateTime.UtcNow.AddDays(-5), MeasuredHand = "Left", BodyPosition = "Sitting", Pulse = 70, Status = "High"},

                    new BloodPressure { PatientID = 20, Systolic = 120, Diastolic = 80, Date = DateTime.UtcNow.AddDays(-3), MeasuredHand = "Right", BodyPosition = "Sitting", Pulse = 70, Status = "Normal"},
                    new BloodPressure { PatientID = 20, Systolic = 130, Diastolic = 90, Date = DateTime.UtcNow.AddDays(-4), MeasuredHand = "Left", BodyPosition = "Laying", Pulse = 70, Status = "Raised"},
                    new BloodPressure { PatientID = 20, Systolic = 150, Diastolic = 100, Date = DateTime.UtcNow.AddDays(-5), MeasuredHand = "Right", BodyPosition = "Sitting", Pulse = 70, Status = "High"}
                );
            }

            if (!context.BodyWeights.Any())
            {
                context.BodyWeights.AddRange(
             
                
                    new BodyWeight { PatientID = 3, Weight = 70.1f, Date = DateTime.UtcNow.AddDays(-3475), Status = "Normal" },
                    new BodyWeight { PatientID = 3, Weight = 70.7f, Date = DateTime.UtcNow.AddDays(-277), Status = "Normal" },
                    new BodyWeight { PatientID = 3, Weight = 70.3f, Date = DateTime.UtcNow.AddDays(-775), Status = "Normal" },
                    new BodyWeight { PatientID = 3, Weight = 70.5f, Date = DateTime.UtcNow.AddDays(-15), Status = "Normal" },
                    new BodyWeight { PatientID = 3, Weight = 70.8f, Date = DateTime.UtcNow.AddDays(-14), Status = "Normal" },
                    new BodyWeight { PatientID = 3, Weight = 71.2f, Date = DateTime.UtcNow.AddDays(-13), Status = "Normal" },
                    new BodyWeight { PatientID = 3, Weight = 70.5f, Date = DateTime.UtcNow.AddDays(-12), Status = "Normal" },
                    new BodyWeight { PatientID = 3, Weight = 70.8f, Date = DateTime.UtcNow.AddDays(-11), Status = "Normal" },
                    new BodyWeight { PatientID = 3, Weight = 71.2f, Date = DateTime.UtcNow.AddDays(-10), Status = "Normal" },
                    new BodyWeight { PatientID = 3, Weight = 70.5f, Date = DateTime.UtcNow.AddDays(-9), Status = "Normal" },
                    new BodyWeight { PatientID = 3, Weight = 70.8f, Date = DateTime.UtcNow.AddDays(-8), Status = "Normal" },
                    new BodyWeight { PatientID = 3, Weight = 71.2f, Date = DateTime.UtcNow.AddDays(-7), Status = "Normal" },
                    new BodyWeight { PatientID = 3, Weight = 70.5f, Date = DateTime.UtcNow.AddDays(-6), Status = "Normal" },
                    new BodyWeight { PatientID = 3, Weight = 70.8f, Date = DateTime.UtcNow.AddDays(-5), Status = "Normal" },
                    new BodyWeight { PatientID = 3, Weight = 71.2f, Date = DateTime.UtcNow.AddDays(-4), Status = "Normal" },
                    new BodyWeight { PatientID = 3, Weight = 70.5f, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal" },
                    new BodyWeight { PatientID = 3, Weight = 70.8f, Date = DateTime.UtcNow.AddDays(-2), Status = "Normal" },
                    // new BodyWeight { PatientID = 3, Weight = 86.2f, Date = DateTime.UtcNow.AddDays(-1), Status = "High" },
                
                    new BodyWeight { PatientID = 3, Weight = 70.5f, Date = DateTime.UtcNow.AddDays(-578), Status = "Normal" },
                    new BodyWeight { PatientID = 3, Weight = 70.5f, Date = DateTime.UtcNow.AddDays(-76), Status = "Normal" },
                    new BodyWeight { PatientID = 3, Weight = 70.5f, Date = DateTime.UtcNow.AddDays(-65), Status = "Normal" },
                    new BodyWeight { PatientID = 3, Weight = 70.5f, Date = DateTime.UtcNow.AddDays(-15), Status = "Normal" },
                    new BodyWeight { PatientID = 3, Weight = 70.8f, Date = DateTime.UtcNow.AddDays(-14), Status = "Normal" },
                    new BodyWeight { PatientID = 3, Weight = 71.2f, Date = DateTime.UtcNow.AddDays(-13), Status = "Normal" },
                    new BodyWeight { PatientID = 3, Weight = 70.5f, Date = DateTime.UtcNow.AddDays(-12), Status = "Normal" },
                    new BodyWeight { PatientID = 3, Weight = 70.8f, Date = DateTime.UtcNow.AddDays(-11), Status = "Normal" },
                    new BodyWeight { PatientID = 3, Weight = 71.2f, Date = DateTime.UtcNow.AddDays(-10), Status = "Normal" },
                    new BodyWeight { PatientID = 3, Weight = 70.5f, Date = DateTime.UtcNow.AddDays(-9), Status = "Normal" },
                    new BodyWeight { PatientID = 3, Weight = 70.8f, Date = DateTime.UtcNow.AddDays(-8), Status = "Normal" },
                    new BodyWeight { PatientID = 3, Weight = 71.2f, Date = DateTime.UtcNow.AddDays(-7), Status = "Normal" },
                    new BodyWeight { PatientID = 3, Weight = 70.5f, Date = DateTime.UtcNow.AddDays(-6), Status = "Normal" },
                    new BodyWeight { PatientID = 3, Weight = 70.8f, Date = DateTime.UtcNow.AddDays(-5), Status = "Normal" },
                    new BodyWeight { PatientID = 3, Weight = 71.2f, Date = DateTime.UtcNow.AddDays(-4), Status = "Normal" },
                    new BodyWeight { PatientID = 3, Weight = 70.5f, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal" },
                    new BodyWeight { PatientID = 3, Weight = 70.8f, Date = DateTime.UtcNow.AddDays(-2), Status = "Normal" },
                    // new BodyWeight { PatientID = 3, Weight = 86.2f, Date = DateTime.UtcNow.AddDays(-1), Status = "High" },
                    
                    new BodyWeight { PatientID = 4, Weight = 70.5f, Date = DateTime.UtcNow.AddDays(-457), Status = "Normal" },
                    new BodyWeight { PatientID = 4, Weight = 70.2f, Date = DateTime.UtcNow.AddDays(-85), Status = "High" },
                    new BodyWeight { PatientID = 4, Weight = 70.7f, Date = DateTime.UtcNow.AddDays(-7), Status = "High" },
                    
                    new BodyWeight { PatientID = 5, Weight = 70.5f, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal" },
                    new BodyWeight { PatientID = 5, Weight = 80.5f, Date = DateTime.UtcNow.AddDays(-66), Status = "High" },
                    new BodyWeight { PatientID = 5, Weight = 90.5f, Date = DateTime.UtcNow.AddDays(-46), Status = "High" },
                    
                    new BodyWeight { PatientID = 6, Weight = 70.5f, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal" },
                    new BodyWeight { PatientID = 6, Weight = 80.5f, Date = DateTime.UtcNow.AddDays(-44), Status = "High" },
                    new BodyWeight { PatientID = 6, Weight = 90.5f, Date = DateTime.UtcNow.AddDays(-634), Status = "High" },

                    new BodyWeight { PatientID = 7, Weight = 73.5f, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal" },
                    new BodyWeight { PatientID = 7, Weight = 73.1f, Date = DateTime.UtcNow.AddDays(-4), Status = "Normal" },
                    new BodyWeight { PatientID = 7, Weight = 73.4f, Date = DateTime.UtcNow.AddDays(-5), Status = "Normal" },

                    new BodyWeight { PatientID = 8, Weight = 65.5f, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal" },
                    new BodyWeight { PatientID = 8, Weight = 65.1f, Date = DateTime.UtcNow.AddDays(-4), Status = "Normal" },
                    new BodyWeight { PatientID = 8, Weight = 65.4f, Date = DateTime.UtcNow.AddDays(-5), Status = "Normal" },

                    new BodyWeight { PatientID = 9, Weight = 67.5f, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal" },
                    new BodyWeight { PatientID = 9, Weight = 67.1f, Date = DateTime.UtcNow.AddDays(-4), Status = "Normal" },
                    new BodyWeight { PatientID = 9, Weight = 67.4f, Date = DateTime.UtcNow.AddDays(-5), Status = "Normal" },

                    new BodyWeight { PatientID = 10, Weight = 87.5f, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal" },
                    new BodyWeight { PatientID = 10, Weight = 87.1f, Date = DateTime.UtcNow.AddDays(-4), Status = "Normal" },
                    new BodyWeight { PatientID = 10, Weight = 87.4f, Date = DateTime.UtcNow.AddDays(-5), Status = "Normal" },

                    new BodyWeight { PatientID = 11, Weight = 77.5f, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal" },
                    new BodyWeight { PatientID = 11, Weight = 77.1f, Date = DateTime.UtcNow.AddDays(-4), Status = "Normal" },
                    new BodyWeight { PatientID = 11, Weight = 77.4f, Date = DateTime.UtcNow.AddDays(-5), Status = "Normal" },

                    new BodyWeight { PatientID = 12, Weight = 81.5f, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal" },
                    new BodyWeight { PatientID = 12, Weight = 81.1f, Date = DateTime.UtcNow.AddDays(-4), Status = "Normal" },
                    new BodyWeight { PatientID = 12, Weight = 81.4f, Date = DateTime.UtcNow.AddDays(-5), Status = "Normal" },

                    new BodyWeight { PatientID = 13, Weight = 76.5f, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal" },
                    new BodyWeight { PatientID = 13, Weight = 76.1f, Date = DateTime.UtcNow.AddDays(-4), Status = "Normal" },
                    new BodyWeight { PatientID = 13, Weight = 76.4f, Date = DateTime.UtcNow.AddDays(-5), Status = "Normal" },

                    new BodyWeight { PatientID = 14, Weight = 72.5f, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal" },
                    new BodyWeight { PatientID = 14, Weight = 72.1f, Date = DateTime.UtcNow.AddDays(-4), Status = "Normal" },
                    new BodyWeight { PatientID = 14, Weight = 72.4f, Date = DateTime.UtcNow.AddDays(-5), Status = "Normal" },

                    new BodyWeight { PatientID = 15, Weight = 75.5f, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal" },
                    new BodyWeight { PatientID = 15, Weight = 75.1f, Date = DateTime.UtcNow.AddDays(-4), Status = "Normal" },
                    new BodyWeight { PatientID = 15, Weight = 75.4f, Date = DateTime.UtcNow.AddDays(-5), Status = "Normal" },

                    new BodyWeight { PatientID = 16, Weight = 92.5f, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal" },
                    new BodyWeight { PatientID = 16, Weight = 92.1f, Date = DateTime.UtcNow.AddDays(-4), Status = "Normal" },
                    new BodyWeight { PatientID = 16, Weight = 92.4f, Date = DateTime.UtcNow.AddDays(-5), Status = "Normal" },

                    new BodyWeight { PatientID = 17, Weight = 70.5f, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal" },
                    new BodyWeight { PatientID = 17, Weight = 70.6f, Date = DateTime.UtcNow.AddDays(-4), Status = "Normal" },
                    new BodyWeight { PatientID = 17, Weight = 70.3f, Date = DateTime.UtcNow.AddDays(-5), Status = "Normal" },

                    new BodyWeight { PatientID = 18, Weight = 70.5f, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal" },
                    new BodyWeight { PatientID = 18, Weight = 70.6f, Date = DateTime.UtcNow.AddDays(-4), Status = "Normal" },
                    new BodyWeight { PatientID = 18, Weight = 70.9f, Date = DateTime.UtcNow.AddDays(-5), Status = "Normal" },

                    new BodyWeight { PatientID = 19, Weight = 80.1f, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal" },
                    new BodyWeight { PatientID = 19, Weight = 80.9f, Date = DateTime.UtcNow.AddDays(-4), Status = "Normal" },
                    new BodyWeight { PatientID = 19, Weight = 80.8f, Date = DateTime.UtcNow.AddDays(-5), Status = "Normal" },

                    new BodyWeight { PatientID = 20, Weight = 90.5f, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal" },
                    new BodyWeight { PatientID = 20, Weight = 90.1f, Date = DateTime.UtcNow.AddDays(-4), Status = "Normal" },
                    new BodyWeight { PatientID = 20, Weight = 90.4f, Date = DateTime.UtcNow.AddDays(-5), Status = "Normal" }

                );
            }

            if (!context.BodyTemperatures.Any())
            {
                context.BodyTemperatures.AddRange(
             
                
                    new BodyTemperature { PatientID = 1, Temperature = 36.1f, Date = DateTime.UtcNow.AddDays(-60), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 36.9f, Date = DateTime.UtcNow.AddDays(-59), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 36.4f, Date = DateTime.UtcNow.AddDays(-58), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 37.2f, Date = DateTime.UtcNow.AddDays(-57), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 36.0f, Date = DateTime.UtcNow.AddDays(-56), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 36.4f, Date = DateTime.UtcNow.AddDays(-55), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 36.8f, Date = DateTime.UtcNow.AddDays(-54), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 36.7f, Date = DateTime.UtcNow.AddDays(-53), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 36.9f, Date = DateTime.UtcNow.AddDays(-52), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-51), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 36.2f, Date = DateTime.UtcNow.AddDays(-50), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 36.3f, Date = DateTime.UtcNow.AddDays(-49), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 37.1f, Date = DateTime.UtcNow.AddDays(-48), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 35.7f, Date = DateTime.UtcNow.AddDays(-47), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 35.5f, Date = DateTime.UtcNow.AddDays(-46), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 36.3f, Date = DateTime.UtcNow.AddDays(-45), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 36.2f, Date = DateTime.UtcNow.AddDays(-44), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 36.9f, Date = DateTime.UtcNow.AddDays(-43), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 38.8f, Date = DateTime.UtcNow.AddDays(-42), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 36.8f, Date = DateTime.UtcNow.AddDays(-41), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 36.0f, Date = DateTime.UtcNow.AddDays(-40), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 36.6f, Date = DateTime.UtcNow.AddDays(-39), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 36.3f, Date = DateTime.UtcNow.AddDays(-38), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 37.3f, Date = DateTime.UtcNow.AddDays(-37), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 37.2f, Date = DateTime.UtcNow.AddDays(-36), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 36.3f, Date = DateTime.UtcNow.AddDays(-35), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 35.9f, Date = DateTime.UtcNow.AddDays(-34), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-33), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 36.9f, Date = DateTime.UtcNow.AddDays(-32), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 36.8f, Date = DateTime.UtcNow.AddDays(-31), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 37.7f, Date = DateTime.UtcNow.AddDays(-30), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 37.3f, Date = DateTime.UtcNow.AddDays(-29), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 36.8f, Date = DateTime.UtcNow.AddDays(-28), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 36.2f, Date = DateTime.UtcNow.AddDays(-27), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 37.2f, Date = DateTime.UtcNow.AddDays(-26), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 38.2f, Date = DateTime.UtcNow.AddDays(-25), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 38.1f, Date = DateTime.UtcNow.AddDays(-24), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-23), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-22), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-21), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-20), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-19), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-18), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-17), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-16), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-15), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 36.0f, Date = DateTime.UtcNow.AddDays(-14), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-13), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 36.2f, Date = DateTime.UtcNow.AddDays(-12), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 36.3f, Date = DateTime.UtcNow.AddDays(-11), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 36.8f, Date = DateTime.UtcNow.AddDays(-10), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-9), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 37.0f, Date = DateTime.UtcNow.AddDays(-8), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 36.9f, Date = DateTime.UtcNow.AddDays(-7), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 35.7f, Date = DateTime.UtcNow.AddDays(-6), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-5), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 38.4f, Date = DateTime.UtcNow.AddDays(-4), Status = "Normal" },
                    new BodyTemperature { PatientID = 1, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-3), Status = "High" },
                    new BodyTemperature { PatientID = 1, Temperature = 39.0f, Date = DateTime.UtcNow.AddDays(-2), Status = "High" },
                    new BodyTemperature { PatientID = 1, Temperature = 38.5f, Date = DateTime.UtcNow.AddDays(-1), Status = "Raised" },
                    
                    new BodyTemperature { PatientID = 3, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-15), Status = "Normal" },
                    new BodyTemperature { PatientID = 3, Temperature = 36.0f, Date = DateTime.UtcNow.AddDays(-14), Status = "Normal" },
                    new BodyTemperature { PatientID = 3, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-13), Status = "Normal" },
                    new BodyTemperature { PatientID = 3, Temperature = 36.0f, Date = DateTime.UtcNow.AddDays(-12), Status = "Normal" },
                    new BodyTemperature { PatientID = 3, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-11), Status = "Normal" },
                    new BodyTemperature { PatientID = 3, Temperature = 36.0f, Date = DateTime.UtcNow.AddDays(-10), Status = "Normal" },
                    new BodyTemperature { PatientID = 3, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-9), Status = "Normal" },
                    new BodyTemperature { PatientID = 3, Temperature = 36.0f, Date = DateTime.UtcNow.AddDays(-8), Status = "Normal" },
                    new BodyTemperature { PatientID = 3, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-7), Status = "Normal" },
                    new BodyTemperature { PatientID = 3, Temperature = 36.0f, Date = DateTime.UtcNow.AddDays(-6), Status = "Normal" },
                    new BodyTemperature { PatientID = 3, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-5), Status = "Normal" },
                    new BodyTemperature { PatientID = 3, Temperature = 36.0f, Date = DateTime.UtcNow.AddDays(-4), Status = "Normal" },
                    new BodyTemperature { PatientID = 3, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal" },
                    // new BodyTemperature { PatientID = 3, Temperature = 39.0f, Date = DateTime.UtcNow.AddDays(-2), Status = "High" },
                    // new BodyTemperature { PatientID = 3, Temperature = 38.5f, Date = DateTime.UtcNow.AddDays(-1), Status = "Raised" },
                    
                    new BodyTemperature { PatientID = 4, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-34), Status = "Normal" },
                    new BodyTemperature { PatientID = 4, Temperature = 37.5f, Date = DateTime.UtcNow.AddDays(-26), Status = "Raised" },
                    new BodyTemperature { PatientID = 4, Temperature = 38.5f, Date = DateTime.UtcNow.AddDays(-642), Status = "High" },
                    
                    new BodyTemperature { PatientID = 5, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-124), Status = "Normal" },
                    new BodyTemperature { PatientID = 5, Temperature = 37.5f, Date = DateTime.UtcNow.AddDays(-442), Status = "Raised" },
                    new BodyTemperature { PatientID = 5, Temperature = 38.5f, Date = DateTime.UtcNow.AddDays(-12), Status = "High" },
                    
                    new BodyTemperature { PatientID = 6, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-232), Status = "Normal" },
                    new BodyTemperature { PatientID = 6, Temperature = 37.5f, Date = DateTime.UtcNow.AddDays(-22), Status = "Raised" },
                    new BodyTemperature { PatientID = 6, Temperature = 38.5f, Date = DateTime.UtcNow.AddDays(-156), Status = "High" }, 

                    new BodyTemperature { PatientID = 7, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal" },
                    new BodyTemperature { PatientID = 7, Temperature = 37.5f, Date = DateTime.UtcNow.AddDays(-2), Status = "Raised" },
                    new BodyTemperature { PatientID = 7, Temperature = 38.5f, Date = DateTime.UtcNow.AddDays(-1), Status = "High" },

                    new BodyTemperature { PatientID = 8, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal" },
                    new BodyTemperature { PatientID = 8, Temperature = 37.5f, Date = DateTime.UtcNow.AddDays(-2), Status = "Raised" },
                    new BodyTemperature { PatientID = 8, Temperature = 38.5f, Date = DateTime.UtcNow.AddDays(-1), Status = "High" },

                    new BodyTemperature { PatientID = 9, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal" },
                    new BodyTemperature { PatientID = 9, Temperature = 37.5f, Date = DateTime.UtcNow.AddDays(-2), Status = "Raised" },
                    new BodyTemperature { PatientID = 9, Temperature = 38.5f, Date = DateTime.UtcNow.AddDays(-1), Status = "High" },

                    new BodyTemperature { PatientID = 10, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal" },
                    new BodyTemperature { PatientID = 10, Temperature = 37.5f, Date = DateTime.UtcNow.AddDays(-2), Status = "Raised" },
                    new BodyTemperature { PatientID = 10, Temperature = 38.5f, Date = DateTime.UtcNow.AddDays(-1), Status = "High" },

                    new BodyTemperature { PatientID = 11, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal" },
                    new BodyTemperature { PatientID = 11, Temperature = 37.5f, Date = DateTime.UtcNow.AddDays(-2), Status = "Raised" },
                    new BodyTemperature { PatientID = 11, Temperature = 38.5f, Date = DateTime.UtcNow.AddDays(-1), Status = "High" },

                    new BodyTemperature { PatientID = 12, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal" },
                    new BodyTemperature { PatientID = 12, Temperature = 37.5f, Date = DateTime.UtcNow.AddDays(-2), Status = "Raised" },
                    new BodyTemperature { PatientID = 12, Temperature = 38.5f, Date = DateTime.UtcNow.AddDays(-1), Status = "High" },

                    new BodyTemperature { PatientID = 13, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal" },
                    new BodyTemperature { PatientID = 13, Temperature = 37.5f, Date = DateTime.UtcNow.AddDays(-2), Status = "Raised" },
                    new BodyTemperature { PatientID = 13, Temperature = 38.5f, Date = DateTime.UtcNow.AddDays(-1), Status = "High" },

                    new BodyTemperature { PatientID = 14, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal" },
                    new BodyTemperature { PatientID = 14, Temperature = 37.5f, Date = DateTime.UtcNow.AddDays(-2), Status = "Raised" },
                    new BodyTemperature { PatientID = 14, Temperature = 38.5f, Date = DateTime.UtcNow.AddDays(-1), Status = "High" },

                    new BodyTemperature { PatientID = 15, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal" },
                    new BodyTemperature { PatientID = 15, Temperature = 37.5f, Date = DateTime.UtcNow.AddDays(-2), Status = "Raised" },
                    new BodyTemperature { PatientID = 15, Temperature = 38.5f, Date = DateTime.UtcNow.AddDays(-1), Status = "High" },

                    new BodyTemperature { PatientID = 16, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal" },
                    new BodyTemperature { PatientID = 16, Temperature = 37.5f, Date = DateTime.UtcNow.AddDays(-2), Status = "Raised" },
                    new BodyTemperature { PatientID = 16, Temperature = 38.5f, Date = DateTime.UtcNow.AddDays(-1), Status = "High" },

                    new BodyTemperature { PatientID = 17, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal" },
                    new BodyTemperature { PatientID = 17, Temperature = 37.5f, Date = DateTime.UtcNow.AddDays(-2), Status = "Raised" }, 
                    new BodyTemperature { PatientID = 17, Temperature = 38.5f, Date = DateTime.UtcNow.AddDays(-1), Status = "High" },

                    new BodyTemperature { PatientID = 18, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal" },
                    new BodyTemperature { PatientID = 18, Temperature = 37.5f, Date = DateTime.UtcNow.AddDays(-2), Status = "Raised" },
                    new BodyTemperature { PatientID = 18, Temperature = 38.5f, Date = DateTime.UtcNow.AddDays(-1), Status = "High" },

                    new BodyTemperature { PatientID = 19, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal" },
                    new BodyTemperature { PatientID = 19, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-2), Status = "Normal" },
                    new BodyTemperature { PatientID = 19, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-1), Status = "Normal" },

                    new BodyTemperature { PatientID = 20, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal" },
                    new BodyTemperature { PatientID = 20, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-2), Status = "Normal" },
                    new BodyTemperature { PatientID = 20, Temperature = 36.5f, Date = DateTime.UtcNow.AddDays(-1), Status = "Normal" }

                );
            }

            if (!context.OxygenSaturations.Any())
            {
                context.OxygenSaturations.AddRange(
                    new OxygenSaturation { PatientID = 2, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-18), Status = "Normal"},
                    new OxygenSaturation { PatientID = 2, OxygenSaturationValue = 96, Date = DateTime.UtcNow.AddDays(-17), Status = "Normal"},
                    new OxygenSaturation { PatientID = 2, OxygenSaturationValue = 97, Date = DateTime.UtcNow.AddDays(-16), Status = "Normal"},
                    new OxygenSaturation { PatientID = 2, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-15), Status = "Normal"},
                    new OxygenSaturation { PatientID = 2, OxygenSaturationValue = 97, Date = DateTime.UtcNow.AddDays(-14), Status = "Normal"},
                    new OxygenSaturation { PatientID = 2, OxygenSaturationValue = 88, Date = DateTime.UtcNow.AddDays(-13), Status = "High"},
                    new OxygenSaturation { PatientID = 2, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-12), Status = "Normal"},
                    new OxygenSaturation { PatientID = 2, OxygenSaturationValue = 93, Date = DateTime.UtcNow.AddDays(-11), Status = "Raised"},
                    new OxygenSaturation { PatientID = 2, OxygenSaturationValue = 90, Date = DateTime.UtcNow.AddDays(-10), Status = "High"},
                    new OxygenSaturation { PatientID = 2, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-9), Status = "Normal"},
                    new OxygenSaturation { PatientID = 2, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-8), Status = "Normal"},
                    new OxygenSaturation { PatientID = 2, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-7), Status = "Normal"},
                    new OxygenSaturation { PatientID = 2, OxygenSaturationValue = 97, Date = DateTime.UtcNow.AddDays(-6), Status = "Normal"},
                    new OxygenSaturation { PatientID = 2, OxygenSaturationValue = 99, Date = DateTime.UtcNow.AddDays(-5), Status = "Normal"},
                    new OxygenSaturation { PatientID = 2, OxygenSaturationValue = 97, Date = DateTime.UtcNow.AddDays(-4), Status = "Normal"},
                    new OxygenSaturation { PatientID = 2, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal"},
                    new OxygenSaturation { PatientID = 2, OxygenSaturationValue = 97, Date = DateTime.UtcNow.AddDays(-2), Status = "Normal"},
                    new OxygenSaturation { PatientID = 2, OxygenSaturationValue = 97, Date = DateTime.UtcNow.AddDays(-1), Status = "Normal"},
                    
                    // new OxygenSaturation { PatientID = 3, OxygenSaturationValue = 95, Date = DateTime.UtcNow.AddDays(-46), Status = "Raised"},
                    // new OxygenSaturation { PatientID = 3, OxygenSaturationValue = 92, Date = DateTime.UtcNow.AddDays(-23), Status = "High"},
                    new OxygenSaturation { PatientID = 3, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-4), Status = "Normal"},
                    new OxygenSaturation { PatientID = 3, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-65), Status = "Normal"},
                    new OxygenSaturation { PatientID = 3, OxygenSaturationValue = 96, Date = DateTime.UtcNow.AddDays(-155), Status = "Normal"},
                    new OxygenSaturation { PatientID = 3, OxygenSaturationValue = 97, Date = DateTime.UtcNow.AddDays(-53), Status = "Normal"},
                    new OxygenSaturation { PatientID = 3, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-15), Status = "Normal"},
                    new OxygenSaturation { PatientID = 3, OxygenSaturationValue = 97, Date = DateTime.UtcNow.AddDays(-14), Status = "Normal"},
                    // new OxygenSaturation { PatientID = 3, OxygenSaturationValue = 88, Date = DateTime.UtcNow.AddDays(-13), Status = "High"},
                    new OxygenSaturation { PatientID = 3, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-12), Status = "Normal"},
                    // new OxygenSaturation { PatientID = 3, OxygenSaturationValue = 94, Date = DateTime.UtcNow.AddDays(-11), Status = "Raised"},
                    // new OxygenSaturation { PatientID = 3, OxygenSaturationValue = 90, Date = DateTime.UtcNow.AddDays(-10), Status = "High"},
                    new OxygenSaturation { PatientID = 3, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-9), Status = "Normal"},
                    new OxygenSaturation { PatientID = 3, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-8), Status = "Normal"},
                    new OxygenSaturation { PatientID = 3, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-7), Status = "Normal"},
                    new OxygenSaturation { PatientID = 3, OxygenSaturationValue = 97, Date = DateTime.UtcNow.AddDays(-6), Status = "Normal"},
                    new OxygenSaturation { PatientID = 3, OxygenSaturationValue = 99, Date = DateTime.UtcNow.AddDays(-5), Status = "Normal"},
                    new OxygenSaturation { PatientID = 3, OxygenSaturationValue = 97, Date = DateTime.UtcNow.AddDays(-4), Status = "Normal"},
                    new OxygenSaturation { PatientID = 3, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal"},
                    new OxygenSaturation { PatientID = 3, OxygenSaturationValue = 97, Date = DateTime.UtcNow.AddDays(-2), Status = "Normal"},
                    new OxygenSaturation { PatientID = 3, OxygenSaturationValue = 97, Date = DateTime.UtcNow.AddDays(-1), Status = "Normal"},
                    
                    new OxygenSaturation { PatientID = 3, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-12), Status = "Normal"},
                    // new OxygenSaturation { PatientID = 3, OxygenSaturationValue = 95, Date = DateTime.UtcNow.AddDays(-17), Status = "Raised"},
                    // new OxygenSaturation { PatientID = 3, OxygenSaturationValue = 92, Date = DateTime.UtcNow.AddDays(-12), Status = "High"},
                    new OxygenSaturation { PatientID = 3, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-65), Status = "Normal"},
                    new OxygenSaturation { PatientID = 3, OxygenSaturationValue = 96, Date = DateTime.UtcNow.AddDays(-155), Status = "Normal"},
                    new OxygenSaturation { PatientID = 3, OxygenSaturationValue = 97, Date = DateTime.UtcNow.AddDays(-53), Status = "Normal"},
                    new OxygenSaturation { PatientID = 3, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-15), Status = "Normal"},
                    new OxygenSaturation { PatientID = 3, OxygenSaturationValue = 97, Date = DateTime.UtcNow.AddDays(-14), Status = "Normal"},
                    // new OxygenSaturation { PatientID = 3, OxygenSaturationValue = 88, Date = DateTime.UtcNow.AddDays(-13), Status = "High"},
                    new OxygenSaturation { PatientID = 3, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-12), Status = "Normal"},
                    // new OxygenSaturation { PatientID = 3, OxygenSaturationValue = 94, Date = DateTime.UtcNow.AddDays(-11), Status = "Raised"},
                    // new OxygenSaturation { PatientID = 3, OxygenSaturationValue = 90, Date = DateTime.UtcNow.AddDays(-10), Status = "High"},
                    new OxygenSaturation { PatientID = 3, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-9), Status = "Normal"},
                    new OxygenSaturation { PatientID = 3, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-8), Status = "Normal"},
                    new OxygenSaturation { PatientID = 3, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-7), Status = "Normal"},
                    new OxygenSaturation { PatientID = 3, OxygenSaturationValue = 97, Date = DateTime.UtcNow.AddDays(-6), Status = "Normal"},
                    new OxygenSaturation { PatientID = 3, OxygenSaturationValue = 99, Date = DateTime.UtcNow.AddDays(-5), Status = "Normal"},
                    new OxygenSaturation { PatientID = 3, OxygenSaturationValue = 97, Date = DateTime.UtcNow.AddDays(-4), Status = "Normal"},
                    new OxygenSaturation { PatientID = 3, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal"},
                    new OxygenSaturation { PatientID = 3, OxygenSaturationValue = 97, Date = DateTime.UtcNow.AddDays(-2), Status = "Normal"},
                    new OxygenSaturation { PatientID = 3, OxygenSaturationValue = 97, Date = DateTime.UtcNow.AddDays(-1), Status = "Normal"},
                    
                    new OxygenSaturation { PatientID = 4, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-355), Status = "Normal"},
                    new OxygenSaturation { PatientID = 4, OxygenSaturationValue = 95, Date = DateTime.UtcNow.AddDays(-3), Status = "Raised"},
                    new OxygenSaturation { PatientID = 4, OxygenSaturationValue = 92, Date = DateTime.UtcNow.AddDays(-13), Status = "High"},
                    
                    new OxygenSaturation { PatientID = 5, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-35), Status = "Normal"},
                    new OxygenSaturation { PatientID = 5, OxygenSaturationValue = 95, Date = DateTime.UtcNow.AddDays(-22), Status = "Raised"},
                    new OxygenSaturation { PatientID = 5, OxygenSaturationValue = 92, Date = DateTime.UtcNow.AddDays(-133), Status = "High"},
                    
                    new OxygenSaturation { PatientID = 6, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-333), Status = "Normal"},
                    new OxygenSaturation { PatientID = 6, OxygenSaturationValue = 95, Date = DateTime.UtcNow.AddDays(-2325), Status = "Raised"},
                    new OxygenSaturation { PatientID = 6, OxygenSaturationValue = 92, Date = DateTime.UtcNow.AddDays(-1), Status = "High"},

                    new OxygenSaturation { PatientID = 7, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal"},
                    new OxygenSaturation { PatientID = 7, OxygenSaturationValue = 95, Date = DateTime.UtcNow.AddDays(-2), Status = "Raised"},
                    new OxygenSaturation { PatientID = 7, OxygenSaturationValue = 92, Date = DateTime.UtcNow.AddDays(-1), Status = "High"},

                    new OxygenSaturation { PatientID = 8, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal"},
                    new OxygenSaturation { PatientID = 8, OxygenSaturationValue = 95, Date = DateTime.UtcNow.AddDays(-2), Status = "Raised"},
                    new OxygenSaturation { PatientID = 8, OxygenSaturationValue = 92, Date = DateTime.UtcNow.AddDays(-1), Status = "High"},

                    new OxygenSaturation { PatientID = 9, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal"},
                    new OxygenSaturation { PatientID = 9, OxygenSaturationValue = 97, Date = DateTime.UtcNow.AddDays(-2), Status = "Normal"},
                    new OxygenSaturation { PatientID = 9, OxygenSaturationValue = 97, Date = DateTime.UtcNow.AddDays(-1), Status = "Normal"},

                    new OxygenSaturation { PatientID = 10, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal"},
                    new OxygenSaturation { PatientID = 10, OxygenSaturationValue = 97, Date = DateTime.UtcNow.AddDays(-2), Status = "Normal"},
                    new OxygenSaturation { PatientID = 10, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-1), Status = "Normal"},

                    new OxygenSaturation { PatientID = 11, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal"},
                    new OxygenSaturation { PatientID = 11, OxygenSaturationValue = 95, Date = DateTime.UtcNow.AddDays(-2), Status = "Normal"},
                    new OxygenSaturation { PatientID = 11, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-1), Status = "Normal"},

                    new OxygenSaturation { PatientID = 12, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal"},
                    new OxygenSaturation { PatientID = 12, OxygenSaturationValue = 95, Date = DateTime.UtcNow.AddDays(-2), Status = "Normal"},
                    new OxygenSaturation { PatientID = 12, OxygenSaturationValue = 90, Date = DateTime.UtcNow.AddDays(-1), Status = "High"},

                    new OxygenSaturation { PatientID = 13, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal"},
                    new OxygenSaturation { PatientID = 13, OxygenSaturationValue = 95, Date = DateTime.UtcNow.AddDays(-2), Status = "Normal"},
                    new OxygenSaturation { PatientID = 13, OxygenSaturationValue = 90, Date = DateTime.UtcNow.AddDays(-1), Status = "High"},

                    new OxygenSaturation { PatientID = 14, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal"},
                    new OxygenSaturation { PatientID = 14, OxygenSaturationValue = 95, Date = DateTime.UtcNow.AddDays(-2), Status = "Normal"},
                    new OxygenSaturation { PatientID = 14, OxygenSaturationValue = 92, Date = DateTime.UtcNow.AddDays(-1), Status = "High"},

                    new OxygenSaturation { PatientID = 15, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal"},
                    new OxygenSaturation { PatientID = 15, OxygenSaturationValue = 95, Date = DateTime.UtcNow.AddDays(-2), Status = "Normal"},
                    new OxygenSaturation { PatientID = 15, OxygenSaturationValue = 92, Date = DateTime.UtcNow.AddDays(-1), Status = "High"},

                    new OxygenSaturation { PatientID = 16, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal"},
                    new OxygenSaturation { PatientID = 16, OxygenSaturationValue = 95, Date = DateTime.UtcNow.AddDays(-2), Status = "Normal"},
                    new OxygenSaturation { PatientID = 16, OxygenSaturationValue = 92, Date = DateTime.UtcNow.AddDays(-1), Status = "High"},

                    new OxygenSaturation { PatientID = 17, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal"},
                    new OxygenSaturation { PatientID = 17, OxygenSaturationValue = 95, Date = DateTime.UtcNow.AddDays(-2), Status = "Normal"},
                    new OxygenSaturation { PatientID = 17, OxygenSaturationValue = 92, Date = DateTime.UtcNow.AddDays(-1), Status = "High"},

                    new OxygenSaturation { PatientID = 18, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal"},
                    new OxygenSaturation { PatientID = 18, OxygenSaturationValue = 95, Date = DateTime.UtcNow.AddDays(-2), Status = "Normal"},
                    new OxygenSaturation { PatientID = 18, OxygenSaturationValue = 92, Date = DateTime.UtcNow.AddDays(-1), Status = "High"},

                    new OxygenSaturation { PatientID = 19, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal"},
                    new OxygenSaturation { PatientID = 19, OxygenSaturationValue = 95, Date = DateTime.UtcNow.AddDays(-2), Status = "Normal"},
                    new OxygenSaturation { PatientID = 19, OxygenSaturationValue = 92, Date = DateTime.UtcNow.AddDays(-1), Status = "High"},

                    new OxygenSaturation { PatientID = 20, OxygenSaturationValue = 98, Date = DateTime.UtcNow.AddDays(-3), Status = "Normal"},
                    new OxygenSaturation { PatientID = 20, OxygenSaturationValue = 95, Date = DateTime.UtcNow.AddDays(-2), Status = "Normal"},
                    new OxygenSaturation { PatientID = 20, OxygenSaturationValue = 92, Date = DateTime.UtcNow.AddDays(-1), Status = "Raised"}

                );
            }

            // create the plans
            if (!context.PatientPlans.Any())
            {
                context.PatientPlans.AddRange(
                    // new PatientPlan { PatientID = 1, TeamID = 1, Name = "Plan A", StartDate = DateTime.UtcNow.AddDays(-365), EndDate = DateTime.UtcNow.AddMinutes(5), Instructions = "Take medication daily", WeightMeasurementDays = new int[] { 1, 0, 0, 0, 0, 0, 0 }, BloodSugarMeasurementDays = new int[] { 1, 0, 0, 1, 0, 0, 0 }, BloodPressureMeasurementDays = new int[] { 1, 0, 0, 0, 0, 0, 0 }, OxygenSaturationMeasurementDays = new int[] { 1, 0, 0, 0, 0, 0, 0 }, BodyTemperatureMeasurementDays = new int[] { 1, 0, 0, 0, 0, 0, 0 } },
                    new PatientPlan { PatientID = 2, TeamID = 2, Name = "Plan B", StartDate = DateTime.UtcNow.AddDays(-365), EndDate = DateTime.UtcNow.AddDays(365), Instructions = "Take medication daily", WeightMeasurementDays = new int[] { 0, 0, 0, 0, 0, 0, 0 }, BloodSugarMeasurementDays = new int[] { 0, 0, 0, 0, 0, 0, 0 }, BloodPressureMeasurementDays = new int[] { 0, 0, 0, 0, 0, 0, 0 }, OxygenSaturationMeasurementDays = new int[] { 1, 1, 1, 1, 1, 1, 1 }, BodyTemperatureMeasurementDays = new int[] { 0, 0, 0, 0, 0, 0, 0 } },
                    new PatientPlan { PatientID = 3, TeamID = 3, Name = "Plan C", StartDate = DateTime.UtcNow.AddDays(-365), EndDate = DateTime.UtcNow.AddDays(30), Instructions = "Take medication daily", WeightMeasurementDays = new int[] { 1, 0, 0, 0, 0, 0, 1 }, BloodSugarMeasurementDays = new int[] { 1, 0, 1, 0, 0, 0, 0 }, BloodPressureMeasurementDays = new int[] { 1, 1, 0, 0, 0, 0, 0 }, OxygenSaturationMeasurementDays = new int[] { 1, 0, 0, 0, 0, 0, 1 }, BodyTemperatureMeasurementDays = new int[] { 1, 0, 0, 0, 0, 0, 0 } },
                    new PatientPlan { PatientID = 4, TeamID = 1, Name = "Plan D", StartDate = DateTime.UtcNow.AddDays(-365), EndDate = DateTime.UtcNow.AddDays(30), Instructions = "Take medication daily", WeightMeasurementDays = new int[] { 1, 0, 0, 0, 0, 0, 0 }, BloodSugarMeasurementDays = new int[] { 1, 0, 0, 0, 0, 0, 0 }, BloodPressureMeasurementDays = new int[] { 1, 0, 0, 0, 0, 0, 0 }, OxygenSaturationMeasurementDays = new int[] { 1, 0, 0, 0, 1, 0, 1 }, BodyTemperatureMeasurementDays = new int[] { 0, 0, 0, 0, 0, 0, 0 } },
                    new PatientPlan { PatientID = 5, TeamID = 2, Name = "Plan E", StartDate = DateTime.UtcNow.AddDays(-365), EndDate = DateTime.UtcNow.AddDays(30), Instructions = "Take medication daily", WeightMeasurementDays = new int[] { 1, 0, 0, 0, 1, 0, 0 }, BloodSugarMeasurementDays = new int[] { 1, 0, 0, 0, 0, 1, 0 }, BloodPressureMeasurementDays = new int[] { 0, 0, 0, 0, 0, 0, 0 }, OxygenSaturationMeasurementDays = new int[] { 1, 0, 0, 0, 0, 1, 0 }, BodyTemperatureMeasurementDays = new int[] { 0, 0, 0, 0, 0, 0, 0 } },
                    new PatientPlan { PatientID = 6, TeamID = 3, Name = "Plan F", StartDate = DateTime.UtcNow.AddDays(-365), EndDate = DateTime.UtcNow.AddDays(30), Instructions = "Take medication daily", WeightMeasurementDays = new int[] { 0, 0, 0, 0, 0, 0, 0 }, BloodSugarMeasurementDays = new int[] { 1, 0, 0, 1, 0, 0, 1 }, BloodPressureMeasurementDays = new int[] { 1, 0, 0, 0, 1, 0, 0 }, OxygenSaturationMeasurementDays = new int[] { 1, 0, 0, 0, 0, 0, 0 }, BodyTemperatureMeasurementDays = new int[] { 0, 0, 0, 0, 0, 0, 0 } }
                );
            }
            




            context.SaveChanges();
        }
    }
}