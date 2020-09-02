using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProntoAtendimento.API.Shared.Configuration;
using ProntoAtendimento.Repository.Context;
using Swashbuckle.AspNetCore.Swagger;

namespace ProntoAtendimento.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // configure services
        public void ConfigureServices(IServiceCollection services)
        {

            // mapeando banco
            services.AddDbContext<EntityContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("MySQLConnection")));

            // Configuração Swagger
            services.AddSwaggerConfiguration(Configuration);

            // configurações Identity
            services.AddIndentityConfiguration(Configuration);

            // configuraçãoes da Api
            services.WebApiConfig();

            // injeção de dependencias
            services.ResolveDependencies();
        }

        
        // configure
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            // ativando middlewares para o uso do Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api - Sistema Pronto Atendimento");
            });

            // Identity
            app.UseAuthentication();

            app.UseMvcConfiguration();

        }
    }
}
