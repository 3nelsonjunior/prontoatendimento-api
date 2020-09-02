using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProntoAtendimento.Domain.Entity;
using System;

namespace ProntoAtendimento.Repository.Mapping
{
    public class PerfilMap : IEntityTypeConfiguration<Perfil>
    {
        public void Configure(EntityTypeBuilder<Perfil> builder)
        {
            builder.ToTable("Perfil");

            builder.HasData(
                new Perfil
                {
                    Id = Guid.Parse("c32b2335-2166-4342-a7cb-ad37c47b9b02"),
                    Name = "ADMIN",
                    NormalizedName = "ADMIN",
                },
                new Perfil
                {
                    Id = Guid.Parse("a1ba9685-8860-46d6-b6e5-a72ce3965970"),
                    Name = "ADMIN_TI",
                    NormalizedName = "ADMIN_TI",
                },
                new Perfil
                {
                    Id = Guid.Parse("8205cf8b-fb97-449d-8810-69cbfa1dacf7"),
                    Name = "CONSULTA",
                    NormalizedName = "CONSULTA",
                },
                new Perfil
                {
                    Id = Guid.Parse("d289114a-f651-43a7-956b-feebbd6d85cc"),
                    Name = "DEV",
                    NormalizedName = "DEV",
                },
                new Perfil
                {
                    Id = Guid.Parse("fedf42c6-7c71-4b96-83eb-05ceaa7a6bbc"),
                    Name = "PA",
                    NormalizedName = "PA",
                },
                new Perfil
                {
                    Id = Guid.Parse("0cc9ce69-e164-4435-9e96-b59877366973"),
                    Name = "TI",
                    NormalizedName = "TI",
                }
            );
        }
    }
}
