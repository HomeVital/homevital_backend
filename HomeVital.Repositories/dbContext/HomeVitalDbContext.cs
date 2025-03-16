
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
        public DbSet<BloodPressure> BloodPressures { get; set; }

        public DbSet<User> Users { get; set;}

        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<BodyWeight> BodyWeights { get; set; }
        public DbSet<BodyTemperature> BodyTemperatures { get; set; }

        public DbSet<OxygenSaturation> OxygenSaturations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasIndex(u => u.Kennitala).IsUnique();

            modelBuilder.Entity<Measurement>()
                .HasKey(m => m.ID);

            modelBuilder.Entity<Measurement>()
                .Property(m => m.ID)
                .ValueGeneratedOnAdd();
        }

    }
}