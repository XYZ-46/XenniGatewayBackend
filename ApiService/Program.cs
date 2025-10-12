using ApiService.ActionFilter;
using ApiService.Middleware;
using Application;
using Auth;
using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;
using System.Text.Json;
using System.Text.Json.Serialization;
using Telemetry;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<ValidationFilter>();
    opt.Filters.Add<DuplicateKeyValidationFilter>();
}).AddJsonOptions(opt =>
{
    var options = opt.JsonSerializerOptions;

    //============* read Json
    // Skip comments in JSON
    options.ReadCommentHandling = JsonCommentHandling.Skip;

    // Accept property names case-insensitively
    options.PropertyNameCaseInsensitive = true;

    // Accept trailing commas (like your example)
    options.AllowTrailingCommas = true;

    // Allow numbers to be read from strings
    options.NumberHandling = JsonNumberHandling.AllowReadingFromString;

    //============* write Json
    // Relax string escaping (allow special chars like single quote '12')
    options.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;

    // Optional: write indented JSON
    options.WriteIndented = true;

    // Ignore null values when serializing
    options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

    // Leave property names as-is (no camelCase conversion)
    options.PropertyNamingPolicy = null;

    // Support enums as strings
    options.Converters.Add(new JsonStringEnumConverter());

}).ConfigureApiBehaviorOptions(options =>
{
    // disable automatic 400 on model validation failure
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info.Version = "9.9";
        document.Info.Title = "Demo .NET 10 API";
        document.Info.Description = "This API demonstrates OpenAPI customization in a .NET 9 project.";
        document.Info.TermsOfService = new Uri("codewithmukesh.com/terms");
        document.Info.Contact = new OpenApiContact
        {
            Name = "Mukesh Murugan",
            Email = "mukesh@codewithmukesh.com",
            Url = new Uri("codewithmukesh.com")
        };
        document.Info.License = new OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("opensource.org/licenses/MIT")
        };

        document.Servers.Add(new OpenApiServer
        {
            Url = "https://api.codewithmukesh.com/v1",
            Description = "Production Server"
        });

        return Task.CompletedTask;
    });
});

builder.Services.AddDIInfrastructure(builder.Configuration["ConnectionDB:XenniDB"]!);
builder.Services.AddDomainDI();
builder.Services.AddApplicationDI();
builder.Services.AddDITelemetryServices();
builder.Services.AddAuthDI(builder.Configuration);

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
    app.MapScalarApiReference("/docs");
}

app.UseHttpsRedirection();
app.MapHealthChecks("_health");

app.UseAuthentication();
app.UseAuthorization(); 

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

await app.RunAsync();
