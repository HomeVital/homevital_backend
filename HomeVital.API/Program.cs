using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;


using HomeVital.Utilities.Mapper;
using HomeVital.Repositories.dbContext;
using HomeVital.Services.Interfaces;
using HomeVital.Services;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// ef við viljum nota automapper
builder.Services.AddAutoMapper(typeof(HomeVitalProfile));

// TODO: Add builder.Services.AddTransient<IUserService, UserService>(); fyrir service og repository

// Add context
builder.Services.AddDbContext<HomeVitalDbContext>(options =>
{
    // options.UseNpgsql(builder.Configuration.GetConnectionString("HomeVitalConnectionString"), options =>
    // options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));
    options.UseNpgsql(builder.Configuration.GetConnectionString("localConnection"), options =>
    options.MigrationsAssembly("HomeVital.Repositories"));
    
});
// Register services
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IHealthcareWorkerService, HealthcareWorkerService>();


builder.Services.AddControllers();
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.UseSwagger();
    // app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program { } // Add this line to make the Program class accessible in tests