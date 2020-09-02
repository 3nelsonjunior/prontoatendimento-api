using ProntoAtendimento.Domain.Enums;
using System;

namespace ProntoAtendimento.Domain.Dto.AtivoDto
{
    public class AtivoTabelaViewDto : BaseDto
    {
        public int IncAtivo { get; set; }
        public string Nome { get; set; }
        public string DescricaoAtivo { get; set; }
        public string CriticidadeAtivo { get; set; }
        public string StatusAtivo { get; set; }
        public Guid SetorId { get; set; }
        public string DescricaoSetor { get; set; }

        public AtivoTabelaViewDto()
        {
            
        }

    }
}
