using System;

namespace ProntoAtendimento.Domain.Dto.TurnoDto
{
    public class TurnoPutDto : BaseDto
    {
        public int IncTurno { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
        public Guid UsuarioId { get; set; }

        public TurnoPutDto()
        {
        }
    }

}
