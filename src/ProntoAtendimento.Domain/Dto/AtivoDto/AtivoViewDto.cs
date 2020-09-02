using ProntoAtendimento.Domain.Enums;
using System;

namespace ProntoAtendimento.Domain.Dto.AtivoDto
{
    public class AtivoViewDto : BaseDto
    {
        public int IncAtivo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataHoraCadastro { get; set; }
        public DateTime DataHoraUltimaAtualizacao { get; set; }
        public string DescricaoCriticidadeAtivo { get; set; }
        public string DescricaoStatusAtivo { get; set; }
        public CriticidadeAtivoEnum CriticidadeAtivo { get; set; }
        public StatusAtivoEnum StatusAtivo { get; set; }
        public Guid SetorId { get; set; }
        public string DescricaoSetor { get; set; }
    }
}
