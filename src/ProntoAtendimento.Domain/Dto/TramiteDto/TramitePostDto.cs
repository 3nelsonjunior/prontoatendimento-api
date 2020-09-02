using System;
using System.Collections.Generic;
using System.Text;

namespace ProntoAtendimento.Domain.Dto.TramiteDto
{
    public class TramitePostDto : BaseDto
    {
        public string Descricao { get; set; }
        public DateTime DataHora { get; set; }
        public bool Solucao { get; set; }
        public Guid OcorrenciaId { get; set; }
        public Guid UsuarioId { get; set; }

        public TramitePostDto()
        {

        }
    }
}
