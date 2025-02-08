/*using System.Reflection;
using Microsoft.EntityFrameworkCore;
using HomeVital.Utilities.Mapper;
using HomeVital.Repositories.dbContext;

var builder = WebApplication.CreateBuilder(args);

// Automatically bind to the port assigned by Azure
builder.WebHost.ConfigureKestrel((context, options) =>
{
    var port = Environment.GetEnvironmentVariable("PORT") ?? "5000"; // Default to 5000 if PORT is not set
    options.ListenAnyIP(int.Parse(port)); // Bind to the available IP and port
});

// Add services to the container.
builder.Services.AddAutoMapper(typeof(HomeVitalProfile));

// Add Database Context
builder.Services.AddDbContext<HomeVitalDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("HomeVitalConnectionString"), options =>
        options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ✅ Enable Swagger in all environments
app.UseSwagger();
app.UseSwaggerUI();

// ✅ Ensure correct middleware order
app.UseRouting();
app.UseAuthorization();
app.MapControllers();  // Ensure API controllers are mapped

if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.Run();
*/

// test config - need to change

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