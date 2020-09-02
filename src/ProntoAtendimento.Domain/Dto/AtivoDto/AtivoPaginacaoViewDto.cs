using System.Collections.Generic;

namespace ProntoAtendimento.Domain.Dto.AtivoDto
{
    public class AtivoPaginacaoViewDto : BasePaginacaoDto
    {
        public List<AtivoTabelaViewDto> listaAtivoTabelaViewDto { get; set; }

        public AtivoPaginacaoViewDto()
        {
            listaAtivoTabelaViewDto = new List<AtivoTabelaViewDto>();
        }
    }
}
