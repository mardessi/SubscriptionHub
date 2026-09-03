using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SubscriptionHub.Application.Common.Interfaces;
using SubscriptionHub.Infrastructure.Persistence;
using SubscriptionHub.Infrastructure.Services;
using SubscriptionHub.Infrastructure.Settings;

namespace SubscriptionHub.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            // Register DbContext
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            // Register repositories, services, etc. here
            // e.g. services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IApplicationDbContext>(provider =>
                         provider.GetRequiredService<AppDbContext>());

            services.AddScoped<IPasswordHasher, PasswordHasher>();
            // Service JWT
            services.AddScoped<IJwtTokenService, JwtTokenService>();

            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            return services;
        }
    }
}
