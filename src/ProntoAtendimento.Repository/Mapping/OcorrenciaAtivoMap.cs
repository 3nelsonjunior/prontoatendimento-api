using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProntoAtendimento.Domain.Entity;

namespace ProntoAtendimento.Repository.Mapping
{
    public class OcorrenciaAtivoMap : IEntityTypeConfiguration<OcorrenciaAtivo>
    {
        public void Configure(EntityTypeBuilder<OcorrenciaAtivo> builder)
        {
            builder.ToTable("Ocorrencia_Ativo");
            
            builder.HasKey(oa => new { oa.OcorrenciaId, oa.AtivoId });
            builder.Property(oa => oa.Principal).HasColumnName("Principal").IsRequired();
            builder.Property(to => to.OcorrenciaId).HasColumnName("Ocorrencia_Id").IsRequired();
            builder.Property(to => to.AtivoId).HasColumnName("Ativo_Id").IsRequired();

            builder.HasOne(oa => oa.Ocorrencia).WithMany(oco => oco.OcorrenciaAtivos).HasForeignKey(to => to.OcorrenciaId).IsRequired();
            builder.HasOne(oa => oa.Ativo).WithMany(ati => ati.OcorrenciaAtivos).HasForeignKey(oa => oa.AtivoId).IsRequired();

        }


    }
}
