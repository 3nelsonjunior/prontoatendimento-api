using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProntoAtendimento.Domain.Entity;

namespace ProntoAtendimento.Repository.Mapping
{
    public class TurnoOcorrenciaMap : IEntityTypeConfiguration<TurnoOcorrencia>
    {
        public void Configure(EntityTypeBuilder<TurnoOcorrencia> builder)
        {
            builder.ToTable("Turno_Ocorrencia");
            
            builder.HasKey(to => new { to.TurnoId, to.OcorrenciaId });
            builder.Property(to => to.TurnoId).HasColumnName("Turno_Id").IsRequired();
            builder.Property(to => to.OcorrenciaId).HasColumnName("Ocorrencia_Id").IsRequired();
            builder.Property(to => to.StatusTurnoOcorrencia).HasColumnName("Status_Turno_Ocorrencia").HasMaxLength(1).IsRequired();

            builder.HasOne(to => to.Turno).WithMany(tur => tur.TurnoOcorrencias).HasForeignKey(to => to.TurnoId);
            builder.HasOne(to => to.Ocorrencia).WithMany(oco => oco.TurnoOcorrencias).HasForeignKey(to => to.OcorrenciaId);
        }


    }
}
