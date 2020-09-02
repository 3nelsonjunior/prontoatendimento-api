using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ProntoAtendimento.API.Shared.JWT;
using ProntoAtendimento.Domain.Entity;
using ProntoAtendimento.Repository.Context;
using System;
using System.Text;

namespace ProntoAtendimento.API.Shared.Configuration
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIndentityConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            // novo contexto para Identity
            services.AddDbContext<EntityContext>(options =>
                options.UseMySql(configuration.GetConnectionString("MySQLConnection")));

            // configuração Indenty
            services
                .AddDefaultIdentity<Usuario>(options => {
                    options.Lockout.AllowedForNewUsers = true;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    options.Lockout.MaxFailedAccessAttempts = 5;
                 })
                .AddRoles<Perfil>()
                .AddEntityFrameworkStores<EntityContext>()
                .AddRoleValidator<RoleValidator<Perfil>>()
                .AddRoleManager<RoleManager<Perfil>>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<TraducaoMensagensIdentityConfig>()
                ;

            // configuração JWT
            var appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.PalavraChave);

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;    // estiver trabalhando com https usar true
                options.SaveToken = true;   // guardar token após apresentação do mesmo
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,    // validar issuer(quem está emitindo) se é o mesmo de quem está emitindo o token
                    IssuerSigningKey = new SymmetricSecurityKey(key),   // criptografando chave
                    ValidateIssuer = true,  // Validar apenas o Issuer conforme nome
                    ValidateAudience = true,    // onde token é valido(qual audiencia)
                    ValidAudience = appSettings.ValidoEm,   // qual audiencia
                    ValidIssuer = appSettings.Emissor   // valido para
                };
            });

            return services;
        }
    }
}
