using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProntoAtendimento.Domain.Entity;
using ProntoAtendimento.Domain.Enums;
using System;

namespace ProntoAtendimento.Repository.Mapping
{
    public class TurnoMap : IEntityTypeConfiguration<Turno>
    {
        public void Configure(EntityTypeBuilder<Turno> builder)
        {
            builder.ToTable("Turno");
            
            builder.HasKey(tur => tur.Id);
            builder.Property(tur => tur.IncTurno).HasColumnName("Inc_Turno").ValueGeneratedOnAdd();
            builder.Property(tur => tur.DataHoraInicio).HasColumnName("Data_Hora_Inicio").HasMaxLength(120).IsRequired();
            builder.Property(tur => tur.DataHoraFim).HasColumnName("Data_Hora_Fim").IsRequired();
            builder.Property(tur => tur.StatusTurno).HasColumnName("Status_Turno").HasMaxLength(1).IsRequired();
            builder.Property(tur => tur.UsuarioId).HasColumnName("Usuario_Id").IsRequired();

            builder.HasAlternateKey(tur => tur.IncTurno).HasName("Inc_Turno");
            builder.HasOne(tur => tur.Usuario).WithMany(usu => usu.Turnos).HasForeignKey(tur => tur.Id);

            builder.HasData(new Turno
            {
                Id = Guid.Parse("3079122f-3efa-4596-91e6-09215bd04ad8"),
                IncTurno = 1000,
                DataHoraInicio = DateTime.Parse("2020-05-29 18:00:00"),
                DataHoraFim = DateTime.Parse("2020-05-30 00:00:00"),
                StatusTurno = StatusTurnoEnum.FECHADO,
                UsuarioId = Guid.Parse("db8ca864-3473-4fa1-bfee-55440dd56ebf"),
            });
        }
    }
}
