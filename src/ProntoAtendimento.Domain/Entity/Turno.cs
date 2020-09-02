using ProntoAtendimento.Domain.Dto.TurnoDto;
using ProntoAtendimento.Domain.Enums;
using System;
using System.Collections.Generic;

namespace ProntoAtendimento.Domain.Entity
{
    public class Turno : BaseEntity
    {
        public int? IncTurno { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
        public StatusTurnoEnum StatusTurno { get; set; }
        public Guid UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<TurnoOcorrencia> TurnoOcorrencias { get; set; }

        public Turno()
        {
        }

        public Turno(TurnoPostDto turnoPostDto)
        {
            IncTurno = null;
            DataHoraInicio = turnoPostDto.DataHoraInicio;
            DataHoraFim = turnoPostDto.DataHoraFim;
            UsuarioId = turnoPostDto.UsuarioId;
            StatusTurno = StatusTurnoEnum.ABERTO;
        }

        public Turno(TurnoPutDto turnoPutDto, TurnoResultDto turnoResultDto)
        {
            Id = Guid.Parse(turnoResultDto.Id);
            IncTurno = int.Parse(turnoResultDto.IncTurno);
            DataHoraInicio = turnoPutDto.DataHoraInicio;
            DataHoraFim = turnoPutDto.DataHoraFim;
            UsuarioId = turnoPutDto.UsuarioId;
        }

       

    }
}
