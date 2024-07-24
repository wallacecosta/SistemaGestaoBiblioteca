using Microsoft.EntityFrameworkCore;
using SistemaGestaoBiblioteca.Application;
using SistemaGestaoBiblioteca.Infrastructure;
using SistemaGestaoBiblioteca.Infrastructure.Configuration;
using SistemaGestaoBiblioteca.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);
var appsettings = "appsettings.Production.json";

if (builder.Environment.IsDevelopment())
    appsettings = "appsettings.Development.json";

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile(appsettings, optional: true, reloadOnChange: true)
    .Build();

var origins = configuration.GetSection("Origins").Value ?? "http://localhost;http://localhost:4200";

builder.Services.Configure<ConnectionStringOptions>(options => configuration.GetSection("AppConnection").Bind(options));

// Add services to the container.

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
