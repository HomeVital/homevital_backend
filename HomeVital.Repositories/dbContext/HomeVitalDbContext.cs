
using Microsoft.EntityFrameworkCore;
using HomeVital.Models.Entities;

namespace HomeVital.Repositories.dbContext
{
    public class HomeVitalDbContext : DbContext
    {
        public HomeVitalDbContext(DbContextOptions<HomeVitalDbContext> options) : base(options) {}
        // TODO: Add DbSet properties for your entities
        public DbSet<HealthcareWorker> HealthcareWorkers { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Bloodsugar> Bloodsugars { get; set; }
        public DbSet<OxygenSaturation> OxygenSaturations { get; set; }
        public DbSet<BloodPressure> BloodPressures { get; set; }

        public DbSet<User> Users { get; set;}

        // public DbSet<Measurement> Measurements { get; set; }
        public DbSet<BodyWeight> BodyWeights { get; set; }
        public DbSet<BodyTemperature> BodyTemperatures { get; set; }
        public DbSet<BloodPressureRange> BloodPressureRanges { get; set; }
        public DbSet<BodyTemperatureRange> BodyTemperatureRanges { get; set; }
        public DbSet<BodyWeightRange> BodyWeightRanges { get; set; }
        public DbSet<BloodSugarRange> BloodSugarRanges { get; set; }
        public DbSet<OxygenSaturationRange> OxygenSaturationRanges { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<PatientPlan> PatientPlans { get; set; }

        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasIndex(u => u.Kennitala).IsUnique(); // Unique index on Kennitala

            modelBuilder.Entity<HealthcareWorker>()
                .HasMany(h => h.Teams)
                .WithMany(t => t.HealthcareWorkers)
                .UsingEntity(j => j.ToTable("HealthcareWorkerTeams")); // Many-to-many relationship

            modelBuilder.Entity<Bloodsugar>()
                .HasOne(b => b.Patient) // Navigation property in Bloodsugar table
                .WithMany(p => p.Bloodsugars) // Navigation property in Bloodsugar table
                .HasForeignKey(b => b.PatientID) // Foreign key in Bloodsugar table
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BloodPressure>()
                .HasOne(b => b.Patient)
                .WithMany(p => p.BloodPressures)
                .HasForeignKey(b => b.PatientID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OxygenSaturation>()
                .HasOne(o => o.Patient)
                .WithMany(p => p.OxygenSaturations)
                .HasForeignKey(o => o.PatientID)
                .OnDelete(DeleteBehavior.Cascade);  

            modelBuilder.Entity<BodyWeight>()
                .HasOne(b => b.Patient)
                .WithMany(p => p.BodyWeights)
                .HasForeignKey(b => b.PatientID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BodyTemperature>()
                .HasOne(b => b.Patient)
                .WithMany(p => p.BodyTemperatures)
                .HasForeignKey(b => b.PatientID)
                .OnDelete(DeleteBehavior.Cascade); // Prevent deletion of patient if it has measurements

            modelBuilder.Entity<Patient>()
                .HasOne(p => p.Team)
                .WithMany(t => t.Patients)
                .HasForeignKey(p => p.TeamID)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deletion of team if it has patients
            modelBuilder.Entity<Team>()
                .HasMany(t => t.Patients)
                .WithOne(p => p.Team)
                .HasForeignKey(p => p.TeamID);
            
        }
        

    }
}