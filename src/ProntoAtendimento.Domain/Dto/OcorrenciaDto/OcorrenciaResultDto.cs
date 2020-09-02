using System;

namespace ProntoAtendimento.Domain.Dto.OcorrenciaDto
{
    public class OcorrenciaResultDto
    {
        public string Id { get; set; }
        public string IncOcorrecia { get; set; }
        public string Titulo { get; set; }
        public string DataHoraInicio { get; set; }
        public string DataHoraFim { get; set; }
        public string DataHoraUltimaAtualizacao { get; set; }
        public string ChamadoTI { get; set; }
        public string ChamadoFornecedor { get; set; }
        public string OcorrenciaCCM { get; set; }
        public string Acionamento { get; set; }
        public string Impacto { get; set; }
        public string DescricaoImpacto { get; set; }
        public string StatusAtualOcorrencia { get; set; }
        public string UsuarioId { get; set; }
        public string DescricaoUsuario { get; set; }

        public OcorrenciaResultDto()
        {
        }
    }
}
