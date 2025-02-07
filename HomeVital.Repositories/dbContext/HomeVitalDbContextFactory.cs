using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Npgsql;

using Microsoft.Extensions.DependencyInjection;
using System.IO;


namespace HomeVital.Repositories.dbContext
{
    public class HomeVitalDbContextFactory : IDesignTimeDbContextFactory<HomeVitalDbContext>
    {
        public HomeVitalDbContext CreateDbContext(string[] args)
        {
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../HomeVital.API");
            // Load configuration from appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true) // For local development
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetConnectionString("Default");

            var optionsBuilder = new DbContextOptionsBuilder<HomeVitalDbContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new HomeVitalDbContext(optionsBuilder.Options);
        }
    }
}