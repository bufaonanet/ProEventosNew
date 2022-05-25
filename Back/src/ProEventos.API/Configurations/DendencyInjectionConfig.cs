using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProEventos.Application;
using ProEventos.Persistence;
using ProEventos.Persistence.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.API.Configurations
{
    public static class DendencyInjectionConfig
    {
        public static IServiceCollection AddDendencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IEventoService, EventoService>();
            services.AddScoped<ILoteService, LoteService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IPalestranteService, PalestranteService>();
            services.AddScoped<IRedeSocialService, RedeSocialService>();

            services.AddScoped<IGeralPersist, GeralPersist>();
            services.AddScoped<IEventoPersist, EventoPersist>();
            services.AddScoped<ILotePersist, LotePersist>();
            services.AddScoped<IUserPersist, UserPersist>();
            services.AddScoped<IPalestrantePersist, PalestrantePersist>();
            services.AddScoped<IRedeSocialPersist, RedeSocialPersist>();

            return services;
        }
    }
}
