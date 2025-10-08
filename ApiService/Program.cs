using ApiService;
using ApiService.Middleware;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;
using Repository;
using Scalar.AspNetCore;
using Services;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Telemetry;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(opt =>
{
    //options.Filters.Add<AppExceptionFilter>();
}).AddJsonOptions(opt =>
{
    // This prevents characters like ' from being escaped
    opt.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
    // Optional: ignore null values
    opt.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

    opt.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    opt.JsonSerializerOptions.AllowTrailingCommas = true;
    opt.JsonSerializerOptions.WriteIndented = true;
    opt.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString;
    opt.JsonSerializerOptions.PropertyNamingPolicy = null;
    opt.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info.Version = "9.9";
        document.Info.Title = "Demo .NET 10 API";
        document.Info.Description = "This API demonstrates OpenAPI customization in a .NET 9 project.";
        document.Info.TermsOfService = new Uri("https://codewithmukesh.com/terms");
        document.Info.Contact = new OpenApiContact
        {
            Name = "Mukesh Murugan",
            Email = "mukesh@codewithmukesh.com",
            Url = new Uri("https://codewithmukesh.com")
        };
        document.Info.License = new OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("https://opensource.org/licenses/MIT")
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
    app.MapScalarApiReference("/docs");
}

app.UseHttpsRedirection();
app.MapHealthChecks("_health");
app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();
app.Run();
