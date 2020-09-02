using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProntoAtendimento.Domain.Entity;
using ProntoAtendimento.Domain.Enums;
using System;

namespace ProntoAtendimento.Repository.Mapping
{
    public class AtivoMap : IEntityTypeConfiguration<Ativo>
    {
        public void Configure(EntityTypeBuilder<Ativo> builder)
        {
            builder.ToTable("Ativo");
            
            builder.HasKey(ati => ati.Id);
            builder.Property(ati => ati.IncAtivo).HasColumnName("Inc_Ativo").ValueGeneratedOnAdd();
            builder.Property(ati => ati.Nome).HasColumnName("Nome").HasMaxLength(480).IsRequired();
            builder.Property(ati => ati.Descricao).HasColumnName("Descricao").HasMaxLength(6000).IsRequired();
            builder.Property(ati => ati.DataHoraCadastro).HasColumnName("Data_Hora_Cadastro").IsRequired();
            builder.Property(ati => ati.DataHoraUltimaAtualizacao).HasColumnName("Data_Hora_Ultima_Atualizacao").IsRequired();
            builder.Property(ati => ati.StatusAtivo).HasColumnName("Status_Ativo").IsRequired();
            builder.Property(ati => ati.CriticidadeAtivo).HasColumnName("Criticidade").HasMaxLength(1).IsRequired();
            builder.Property(ati => ati.SetorId).HasColumnName("Setor_Id").IsRequired();

            builder.HasAlternateKey(ati => ati.IncAtivo).HasName("Inc_Ativo");
            builder.HasOne(ati => ati.Setor).WithMany(set => set.Ativos).HasForeignKey(ati => ati.Id);

            
            builder.HasData(new Ativo
                {
                    Id = Guid.Parse("6c2b2734-ea6b-4c00-afc1-75f06de09eee"),
                    IncAtivo = 1000,
                    Nome = "PADRAO",
                    Descricao = "PADRAO",
                    DataHoraCadastro = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                    DataHoraUltimaAtualizacao = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                    StatusAtivo = StatusAtivoEnum.ATIVO,
                    CriticidadeAtivo = CriticidadeAtivoEnum.NAO_SE_APLICA,
                    SetorId = Guid.Parse("dd84e876-d3d7-418b-85e0-5aee403a2ceb")
                }
            );

        }


    }
}
