using Microsoft.Extensions.DependencyInjection;

namespace Telemetry
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDITelemetryServices(this IServiceCollection services)
        {

            services.AddHealthChecks().AddCheck<DatabaseHealthCheck>("XenniDB");


            return services;
        }
    }
}
