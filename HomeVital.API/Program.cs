

using Microsoft.EntityFrameworkCore;
using HomeVital.Utilities.Mapper;
using HomeVital.Repositories.dbContext;
using System.Reflection;
using HomeVital.Services.Interfaces;
using HomeVital.Services.Implementations;
using HomeVital.Repositories.Interfaces;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;
using HomeVital.Repositories.Implementations;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(HomeVitalProfile));

// Add Transient for all service and repository interfaces
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddTransient<IUserRepository, UserRepository>();




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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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