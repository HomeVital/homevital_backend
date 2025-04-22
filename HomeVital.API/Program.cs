using Microsoft.EntityFrameworkCore;
using HomeVital.Utilities.Mapper;
using HomeVital.Repositories.dbContext;
using System.Reflection;
using HomeVital.Services.Interfaces;
using HomeVital.Services.Implementations;
using HomeVital.Repositories.Interfaces;
using HomeVital.Repositories.Implementations;

using Microsoft.AspNetCore.Authentication;
using HomeVital.Services;
using HomeVital.Repositories;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;


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
builder.Services.AddTransient<IBodyWeightService, BodyWeightService>();
builder.Services.AddTransient<IBodyWeightRepository, BodyWeightRepository>();
builder.Services.AddTransient<IBodyTemperatureService, BodyTemperatureService>();
builder.Services.AddTransient<IBodyTemperatureRepository, BodyTemperatureRepository>();
builder.Services.AddTransient<IOxygenSaturationService, OxygenSaturationService>();
builder.Services.AddTransient<IOxygenSaturationRepository, OxygenSaturationRepository>();
builder.Services.AddTransient<IVitalRangeService, VitalRangeService>();
builder.Services.AddTransient<IVitalRangeRepository, VitalRangeRepository>();
builder.Services.AddTransient<ITeamService, TeamService>();
builder.Services.AddTransient<ITeamRepository, TeamRepository>();

builder.Services.AddSingleton<ITokenService, TokenService>();

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


// Add services to the container.
var jwtSettings = builder.Configuration.GetSection("Jwt");


// var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);
var keyString = jwtSettings["Key"];
byte[] key;
if (keyString != null)
{
    key = Encoding.ASCII.GetBytes(keyString);
    // Continue processing with 'key'
}
else
{
    // Handle the null case, e.g., log an error or throw an exception
    throw new ArgumentNullException("jwtSettings[\"Key\"]", "The JWT key setting cannot be null.");
}

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Initialize the database with seed data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<HomeVitalDbContext>();
    DatabaseInitializer.Initialize(context);
}

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