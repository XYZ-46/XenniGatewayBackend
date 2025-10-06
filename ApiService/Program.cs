using ApiService;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Repository;
using Services;
using Telemetry;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    //options.Filters.Add<AppExceptionFilter>();
});

builder.Services.AddOpenApi();

builder.Services.AddDIInfrastructure(builder.Configuration["ConnectionDB:XenniDB"]!);
builder.Services.AddDIRepository();
builder.Services.AddDIService();
builder.Services.AddDIApi();

builder.Services.AddDITelemetryServices();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
});

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
