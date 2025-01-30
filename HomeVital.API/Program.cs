using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;


using HomeVital.Utilities.Mapper;
using HomeVital.Repositories.dbContext;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// ef við viljum nota automapper
builder.Services.AddAutoMapper(typeof(HomeVitalProfile));

// TODO: Add builder.Services.AddTransient<IUserService, UserService>(); fyrir service og repository

// Add context
builder.Services.AddDbContext<HomeVitalDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("HomeVitalConnectionString"), options =>
    options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();