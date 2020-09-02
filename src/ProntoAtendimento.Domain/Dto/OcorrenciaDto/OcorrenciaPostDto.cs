using ProntoAtendimento.Domain.Dto.AtivoDto;
using System;
using System.Collections.Generic;

namespace ProntoAtendimento.Domain.Dto.OcorrenciaDto
{
    public class OcorrenciaPostDto : BaseDto
    {
        public string Titulo { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime? DataHoraFim { get; set; }
        public string ChamadoTI { get; set; }
        public string ChamadoFornecedor { get; set; }
        public string OcorrenciaCCM { get; set; }
        public bool Acionamento { get; set; }
        public bool Impacto { get; set; }
        public string DescricaoImpacto { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid TurnoId { get; set; }
        public ICollection<AtivoOcorrenciaPostDto> Ativos { get; set; }

        public OcorrenciaPostDto()
        {

        }
    }
}
