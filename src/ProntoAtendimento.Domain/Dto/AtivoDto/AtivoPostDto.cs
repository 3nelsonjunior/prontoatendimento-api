using ProntoAtendimento.Domain.Enums;
using System;

namespace ProntoAtendimento.Domain.Dto.AtivoDto
{
    public class AtivoPostDto : BaseDto
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public CriticidadeAtivoEnum CriticidadeAtivo { get; set; }
        public Guid SetorId { get; set; }

        public AtivoPostDto()
        {

        }
    }
}
