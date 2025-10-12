using Infrastructure.IRepositories;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDIInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<XenniDB>(options => options.UseSqlServer(connectionString));

            services.AddScoped<ITenantRepo, TenantRepository>();
            services.AddScoped<IUserLoginRepo, UserLoginRepo>();
            services.AddScoped<IUserProfileRepo, UserProfileRepo>();

            return services;
        }
    }
}
