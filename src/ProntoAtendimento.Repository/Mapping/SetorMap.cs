using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProntoAtendimento.Domain.Entity;
using System;

namespace ProntoAtendimento.Repository.Mapping
{
    public class SetorMap : IEntityTypeConfiguration<Setor>
    {
        public void Configure(EntityTypeBuilder<Setor> builder)
        {

            builder.ToTable("Setor");
            
            builder.HasKey(set => set.Id);
            builder.Property(set => set.IncSetor).HasColumnName("Inc_Setor").ValueGeneratedOnAdd();
            builder.Property(set => set.Nome).HasColumnName("Nome").HasMaxLength(480).IsRequired();
            builder.Property(set => set.Coordenacao).HasColumnName("Coordenacao").HasMaxLength(480).IsRequired();

            builder.HasAlternateKey(set => set.IncSetor).HasName("Inc_Setor");
            builder.HasMany(set => set.Ativos).WithOne(ati => ati.Setor).HasForeignKey(ati => ati.SetorId);

            builder.HasData(new Setor
                {
                    Id = Guid.Parse("dd84e876-d3d7-418b-85e0-5aee403a2ceb"),
                    IncSetor = 1000,
                    Nome = "PADRAO",
                    Coordenacao = "PADRAO"
                }
            );
        }
    }
}
