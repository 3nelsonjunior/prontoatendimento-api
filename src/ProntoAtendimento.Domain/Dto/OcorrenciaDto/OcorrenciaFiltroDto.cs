using ProntoAtendimento.Domain.Enums;
using System;

namespace ProntoAtendimento.Domain.Dto.OcorrenciaDto
{
    public class OcorrenciaFiltroDto
    {
        public string Titulo { get; set; }
        public string DataHoraInicio { get; set; }
        public string DataHoraFim { get; set; }
        public string DataHoraUltimaAtualizacao { get; set; }
        public string ChamadoTI { get; set; }
        public string ChamadoFornecedor { get; set; }
        public string OcorrenciaCCM { get; set; }
        public bool Acionamento { get; set; }
        public bool Impacto { get; set; }
        public string StatusAtualOcorrencia { get; set; }
        public string UsuarioId { get; set; }

        public OcorrenciaFiltroDto()
        {

        }
    }
}
