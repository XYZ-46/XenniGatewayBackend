using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDIInfrastructureServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<XenniDB>(options => options.UseSqlServer(connectionString));


            return services;
        }
    }
}
