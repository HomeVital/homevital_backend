

using Microsoft.EntityFrameworkCore;
using HomeVital.Utilities.Mapper;
using HomeVital.Repositories.dbContext;
using System.Reflection;
using HomeVital.Services.Interfaces;
using HomeVital.Services.Implementations;
using HomeVital.Repositories.Interfaces;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;
using HomeVital.Repositories.Implementations;

using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HomeVital.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(HomeVitalProfile));

// Add Transient for all service and repository interfaces

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IBloodsugarService, BloodsugarService>();
builder.Services.AddTransient<IBloodsugarRepository, BloodsugarRepository>();
builder.Services.AddTransient<IBloodPressureService, BloodPressureService>();
builder.Services.AddTransient<IBloodPressureRepository, BloodPressureRepository>();
builder.Services.AddTransient<IPatientService, PatientService>();
builder.Services.AddTransient<IPatientRepository, PatientRepository>();
builder.Services.AddTransient<IHealthcareWorkerService, HealthcareWorkerService>();
builder.Services.AddTransient<IHealthcareWorkerRepository, HealthcareWorkerRepository>();
builder.Services.AddTransient<IMeasurementService, MeasurementsService>();
builder.Services.AddTransient<IMeasurementsRepository, MeasurementsRepository>();
builder.Services.AddTransient<MeasurementsService>();


var environment = Environment.GetEnvironmentVariable("AZURE_ENV") ?? "LocalDevelopment";

var connectionString = builder.Configuration.GetConnectionString(
    environment == "AzureDevelopment" ? "HomeVitalConnectionString" : "Default"
);

builder.Services.AddDbContext<HomeVitalDbContext>(options =>
    options.UseNpgsql(connectionString, options =>
        options.MigrationsAssembly("HomeVital.Repositories"))
);

builder.Services.AddDbContext<HomeVitalDbContext>(options =>
{
    options.UseNpgsql(connectionString, options =>
    options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));
    
});


builder.Services.AddControllers();
// Register TimeProvider
builder.Services.AddSingleton<TimeProvider>(TimeProvider.System);
builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// public void ConfigureServices(IServiceCollection services)
// {
//     services.AddScoped<IUserService, UserService>();
//     // Other service registrations
// }

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI(x =>
{

    x.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API V1");

#if DEBUG
    x.RoutePrefix = "swagger"; // For localhost
#else
    x.RoutePrefix = string.Empty; //  For azure
#endif
}
);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { } // Add this line to make the Program class accessible in tests