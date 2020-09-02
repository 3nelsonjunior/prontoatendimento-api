using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace ProntoAtendimento.API.Shared.Configuration
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Api - Sistema Pronto Atendimento",
                        Version = "v1",
                        Description = "Api - Controle de Ocorrências do Pronto Atendimento",
                        Contact = new Contact
                        {
                            Name = "Nelson Florencio Júnior",
                            Url = "https://github.com/3nelsonjunior"
                        }
                    }
                );

            });

            return services;
        }
    }
}
