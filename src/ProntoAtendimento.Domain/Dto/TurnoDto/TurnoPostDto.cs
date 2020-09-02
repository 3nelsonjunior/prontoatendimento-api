using System;

namespace ProntoAtendimento.Domain.Dto.TurnoDto
{
    public class TurnoPostDto : BaseDto
    {
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
        public Guid UsuarioId { get; set; }

        public TurnoPostDto()
        {
        }

    }
}
