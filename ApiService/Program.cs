using ApiService;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Repository;
using Services;
using System.Text.Json;
using System.Text.Json.Serialization;
using Telemetry;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(opt =>
{
    //options.Filters.Add<AppExceptionFilter>();
}).AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    opt.JsonSerializerOptions.AllowTrailingCommas = true;
    opt.JsonSerializerOptions.WriteIndented = true;
    opt.JsonSerializerOptions.PropertyNamingPolicy = null;
    opt.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddOpenApi();

builder.Services.AddDIInfrastructure(builder.Configuration["ConnectionDB:XenniDB"]!);
builder.Services.AddDIRepository();
builder.Services.AddDIService();
builder.Services.AddDIApi();

builder.Services.AddDITelemetryServices();

builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new ApiVersion(1, 0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = new UrlSegmentApiVersionReader();
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
