using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProntoAtendimento.Domain.Entity;

namespace ProntoAtendimento.Repository.Mapping
{
    public class TramiteMap : IEntityTypeConfiguration<Tramite>
    {
        public void Configure(EntityTypeBuilder<Tramite> builder)
        {
            builder.ToTable("Tramite");
            
            builder.HasKey(tra => tra.Id);
            builder.Property(tra => tra.IncTramite).HasColumnName("Inc_Tramite").ValueGeneratedOnAdd();
            builder.Property(tra => tra.Descricao).HasColumnName("Descricao").HasMaxLength(6000).IsRequired();
            builder.Property(tra => tra.DataHora).HasColumnName("Data_Hora").IsRequired();
            builder.Property(tra => tra.Solucao).HasColumnName("Solucao").IsRequired();
            builder.Property(tra => tra.OcorrenciaId).HasColumnName("Ocorrencia_Id").IsRequired();
            builder.Property(tra => tra.UsuarioId).HasColumnName("Usuario_Id").IsRequired();

            builder.HasAlternateKey(tra => tra.IncTramite).HasName("Inc_Tramite");
            builder.HasOne(tra => tra.Ocorrencia).WithMany(oco => oco.Tramites).HasForeignKey(tra => tra.Id);
            builder.HasOne(tra => tra.Usuario).WithMany(usu => usu.Tramites).HasForeignKey(tra => tra.Id);
        }


    }
}
