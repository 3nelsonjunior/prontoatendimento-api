using ProntoAtendimento.Domain.Entity;
using ProntoAtendimento.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProntoAtendimento.Domain.Dto.TurnoDto
{
    public class TurnoResultDto
    {
        public string Id { get; set; }
        public string IncTurno { get; set; }
        public string DataHoraInicio { get; set; }
        public string DataHoraFim { get; set; }
        public string StatusTurno { get; set; }
        public string UsuarioId { get; set; }
        public string DescricaoUsuario { get; set; }

        public TurnoResultDto()
        {
        }

    }
}
