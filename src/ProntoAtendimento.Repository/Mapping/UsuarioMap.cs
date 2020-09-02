using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProntoAtendimento.Domain.Entity;
using ProntoAtendimento.Domain.Enums;
using System;

namespace ProntoAtendimento.Repository.Mapping
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");

            builder.HasMany(usu => usu.Ocorrencias).WithOne(oco => oco.Usuario).HasForeignKey(oco => oco.UsuarioId);
            builder.HasMany(usu => usu.Tramites).WithOne(tra => tra.Usuario).HasForeignKey(tra => tra.UsuarioId);
            builder.HasMany(usu => usu.Turnos).WithOne(tur => tur.Usuario).HasForeignKey(tur => tur.UsuarioId);

            var password = "Admin.0001";

            var usuario = new Usuario
            {
                Id = Guid.Parse("db8ca864-3473-4fa1-bfee-55440dd56ebf"),
                UserName = "11111111",
                NormalizedUserName = "11111111",
                NomeCompleto = "ADMIN_I",
                Email = "grupo.operacao@mrs.com.br",
                NormalizedEmail = "GRUPO.OPERACAO@MRS.COM.BR",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                StatusUsuario = StatusUsuarioEnum.ATIVO,
                DataHoraCadastro = DateTime.Now,
                LockoutEnabled = true,
            };

            usuario.PasswordHash = new PasswordHasher<Usuario>().HashPassword(usuario, password);

            builder.HasData(usuario);
        }
    }
}
