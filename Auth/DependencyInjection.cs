using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Auth
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAuthDI(this IServiceCollection services, string connectionString)
        {
            services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                //var settings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "settings.Issuer",
                    ValidAudience = "settings.Audience",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("settings.SecretKey")),
                    ClockSkew = TimeSpan.Zero // Prevent time drift abuse
                };
            });

            return services;
        }
    }
}
