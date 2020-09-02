using ProntoAtendimento.Domain.Enums;
using System;


namespace ProntoAtendimento.Domain.Dto.AtivoDto
{
    public class AtivoPutDto : BaseDto
    {
        public int IncAtivo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public StatusAtivoEnum StatusAtivo { get; set; }
        public CriticidadeAtivoEnum CriticidadeAtivo { get; set; }
        public Guid SetorId { get; set; }
    }
}
