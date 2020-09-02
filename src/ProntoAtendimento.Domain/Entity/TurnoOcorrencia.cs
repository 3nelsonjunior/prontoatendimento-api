using ProntoAtendimento.Domain.Enums;
using System;

namespace ProntoAtendimento.Domain.Entity
{
    public class TurnoOcorrencia
    {
        public Guid TurnoId { get; set; }
        public Guid OcorrenciaId { get; set; }
        public StatusOcorrenciaEnum StatusTurnoOcorrencia { get; set; }
        public virtual Turno Turno { get; set; }
        public virtual Ocorrencia Ocorrencia { get; set; }

        public TurnoOcorrencia()
        {

        }
        public TurnoOcorrencia(Guid turnoId, Guid ocorrenciaId, StatusOcorrenciaEnum statusOcorrencia)
        {
            TurnoId = turnoId;
            OcorrenciaId = ocorrenciaId;
            StatusTurnoOcorrencia = statusOcorrencia;
        }
    }
}
