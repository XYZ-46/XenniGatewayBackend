using Infrastructure;
using Telemetry;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDIInfrastructureServices(builder.Configuration["ConnectionDB:XenniDB"]!);
builder.Services.AddDITelemetryServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapHealthChecks("_health");

app.UseAuthorization();

app.MapControllers();

app.Run();
