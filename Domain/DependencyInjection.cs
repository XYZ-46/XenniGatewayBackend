﻿using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomainDI(this IServiceCollection services)
        {
            services.AddScoped<ITenantRepo, TenantRepository>();


            return services;
        }
    }
}
