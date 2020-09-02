using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProntoAtendimento.Domain.Entity;
using System;

namespace ProntoAtendimento.Repository.Mapping
{
    public class UsuarioPerfilMap : IEntityTypeConfiguration<UsuarioPerfil>
    {
        public void Configure(EntityTypeBuilder<UsuarioPerfil> builder)
        {
            builder.ToTable("UsuarioPerfil");

            builder.HasKey(ur => new { ur.UserId, ur.RoleId });

            builder.HasOne(ur => ur.Perfil).WithMany(per => per.ListaUsuarioPerfil).HasForeignKey(ur => ur.RoleId).IsRequired();
            builder.HasOne(ur => ur.Usuario).WithMany(per => per.ListaUsuarioPerfil).HasForeignKey(ur => ur.UserId).IsRequired();

            builder.HasData(new UsuarioPerfil
            {
                RoleId = Guid.Parse("c32b2335-2166-4342-a7cb-ad37c47b9b02"),
                UserId = Guid.Parse("db8ca864-3473-4fa1-bfee-55440dd56ebf")
            }); 

        }


    }
}
