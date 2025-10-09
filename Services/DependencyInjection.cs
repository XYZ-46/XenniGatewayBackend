using Microsoft.Extensions.DependencyInjection;
using Services.Interfaces;

namespace Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDIService(this IServiceCollection services)
        {
            services.AddScoped<ITenantService, TenantService>();

            return services;
        }
    }
}
