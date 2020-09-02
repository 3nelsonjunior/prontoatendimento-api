using ProntoAtendimento.Domain.Enums;
using System;

namespace ProntoAtendimento.Domain.Entity
{
    public class OcorrenciaAtivo
    {
        public Guid OcorrenciaId { get; set; }
        public Guid AtivoId { get; set; }
        public bool Principal { get; set; }
        public virtual Ocorrencia Ocorrencia { get; set; }
        public virtual Ativo Ativo { get; set; }

        public OcorrenciaAtivo()
        {

        }

        public OcorrenciaAtivo(Guid ocorrenciaId, Guid ativoId, bool principal)
        {
            OcorrenciaId = ocorrenciaId;
            AtivoId = ativoId;
            Principal = principal;
        }
    }
}
