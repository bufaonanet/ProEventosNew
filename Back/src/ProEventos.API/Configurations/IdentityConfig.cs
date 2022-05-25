using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ProEventos.Domain.Identity;
using ProEventos.Persistence.Contextos;

namespace ProEventos.API.Configurations
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services)
        {
            services.AddIdentityCore<User>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 4;
            })
           .AddRoles<Role>()
           .AddRoleManager<RoleManager<Role>>()
           .AddSignInManager<SignInManager<User>>()
           .AddRoleValidator<RoleValidator<Role>>()
           .AddEntityFrameworkStores<ProEventosContext>()
           .AddDefaultTokenProviders();

            return services;
        }
    }
}
