using Microsoft.Extensions.DependencyInjection;
using ProntoAtendimento.Domain.Notification;
using ProntoAtendimento.Repository.Implementation;
using ProntoAtendimento.Repository.Interface;
using ProntoAtendimento.Service.Implementation;
using ProntoAtendimento.Service.Interface;

namespace ProntoAtendimento.API.Shared.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            // serviços
            services.AddScoped<IAtivoService, AtivoService>();
            services.AddScoped<IOcorrenciaService, OcorrenciaService>();
            services.AddScoped<ISetorService, SetorService>();
            services.AddScoped<ITramiteService, TramiteService>();
            services.AddScoped<ITurnoService, TurnoService>();
            services.AddScoped<IUsuarioService, UsuarioService>();

            // repository
            services.AddScoped<IAtivoRepository, AtivoRepository>();
            services.AddScoped<IOcorrenciaAtivoRepository, OcorrenciaAtivoRepository>();
            services.AddScoped<IOcorrenciaRepository, OcorrenciaRepository>();
            services.AddScoped<ISetorRepository, SetorRepository>();
            services.AddScoped<ITramiteRepository, TramiteRepository>();
            services.AddScoped<ITurnoOcorrenciaRepository, TurnoOcorrenciaRepository>();
            services.AddScoped<ITurnoRepository, TurnoRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            // outros
            services.AddScoped<INotificador, Notificador>();
            
            return services;
        }

    }
}
