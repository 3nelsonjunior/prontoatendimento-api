using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProntoAtendimento.Domain.Entity;
using ProntoAtendimento.Repository.Mapping;
using System;

namespace ProntoAtendimento.Repository.Context
{
    public class EntityContext : IdentityDbContext<Usuario,
                                                   Perfil,
                                                   Guid,
                                                   IdentityUserClaim<Guid>,
                                                   UsuarioPerfil,
                                                   IdentityUserLogin<Guid>,
                                                   IdentityRoleClaim<Guid>,
                                                   IdentityUserToken<Guid>
                                                    >
    {
        public EntityContext(DbContextOptions<EntityContext> options) : base(options)
        { }
        

        public DbSet<Ativo> Ativos { get; set; }
        public DbSet<Ocorrencia> Ocorrencias { get; set; }
        public DbSet<OcorrenciaAtivo> OcorrenciaAtivos { get; set; }
        public DbSet<Setor> Setores { get; set; }
        public DbSet<Tramite> Tramites { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<TurnoOcorrencia> TurnoOcorrencias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AtivoMap());
            modelBuilder.ApplyConfiguration(new OcorrenciaMap());
            modelBuilder.ApplyConfiguration(new OcorrenciaAtivoMap());
            modelBuilder.ApplyConfiguration(new SetorMap());
            modelBuilder.ApplyConfiguration(new TramiteMap());
            modelBuilder.ApplyConfiguration(new TurnoMap());
            modelBuilder.ApplyConfiguration(new TurnoOcorrenciaMap());

            modelBuilder.ApplyConfiguration(new PerfilMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new UsuarioPerfilMap());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
