using System;

namespace ProntoAtendimento.Domain.Dto.TurnoDto
{
    public class TurnoTabelaViewDto : BaseDto
    {
        public string IncTurno { get; set; }
        public string DataHoraInicio { get; set; }
        public string DataHoraFim { get; set; }
        public string StatusTurno { get; set; }
        public string DescricaoStatusTurno { get; set; }
        public Guid UsuarioId { get; set; }
        public string DescricaoUsuario { get; set; }

        public TurnoTabelaViewDto()
        {

        }
    }
}