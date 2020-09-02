using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProntoAtendimento.Domain.Entity;

namespace ProntoAtendimento.Repository.Mapping
{
    public class OcorrenciaMap : IEntityTypeConfiguration<Ocorrencia>
    {
        public void Configure(EntityTypeBuilder<Ocorrencia> builder)
        {
            builder.ToTable("Ocorrencia");
            
            builder.HasKey(oco => oco.Id);
            builder.Property(oco => oco.IncOcorrencia).HasColumnName("Inc_Ocorrencia").ValueGeneratedOnAdd();
            builder.Property(oco => oco.Titulo).HasColumnName("Titulo").HasMaxLength(300).IsRequired();
            builder.Property(oco => oco.DataHoraInicio).HasColumnName("Data_Hora_Inicio").HasMaxLength(120).IsRequired();
            builder.Property(oco => oco.DataHoraFim).HasColumnName("Data_Hora_Fim");
            builder.Property(oco => oco.DataHoraUltimaAtualizacao).HasColumnName("Data_Hora_Ultima_Atualizacao").IsRequired();
            builder.Property(oco => oco.ChamadoTI).HasColumnName("Chamado_TI").HasMaxLength(30);
            builder.Property(oco => oco.ChamadoFornecedor).HasColumnName("Chamado_Fornecedor").HasMaxLength(60);
            builder.Property(oco => oco.OcorrenciaCCM).HasColumnName("Ocorrencia_CCM").HasMaxLength(30);
            builder.Property(oco => oco.Acionamento).HasColumnName("Acionamento").IsRequired();
            builder.Property(oco => oco.Impacto).HasColumnName("Impacto").IsRequired();
            builder.Property(oco => oco.DescricaoImpacto).HasColumnName("Descricao_Impacto").HasMaxLength(3600);
            builder.Property(oco => oco.StatusAtualOcorrencia).HasColumnName("Status_Atual_Ocorrencia").HasMaxLength(1).IsRequired();
            builder.Property(oco => oco.UsuarioId).HasColumnName("Usuario_Id").IsRequired();

            builder.HasAlternateKey(oco => oco.IncOcorrencia).HasName("Inc_Ocorrencia");
            builder.HasOne(oco => oco.Usuario).WithMany(usu => usu.Ocorrencias).HasForeignKey(oco => oco.Id);
            builder.HasMany(oco => oco.Tramites).WithOne(tra => tra.Ocorrencia).HasForeignKey(tra => tra.OcorrenciaId);


        }
    }
}
