using Interfaces.IRepositoryCrud;
using Microsoft.Extensions.DependencyInjection;

namespace Repository
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDIRepository(this IServiceCollection services)
        {
            services.AddScoped<ITenantRepoCrud, TenantRepository>();


            return services;
        }
    }
}
