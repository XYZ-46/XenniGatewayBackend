using Microsoft.Extensions.DependencyInjection;
using Repository.Interfaces;

namespace Repository
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDIRepository(this IServiceCollection services)
        {
            services.AddScoped<ITenantRepo, TenantRepository>();


            return services;
        }
    }
}
