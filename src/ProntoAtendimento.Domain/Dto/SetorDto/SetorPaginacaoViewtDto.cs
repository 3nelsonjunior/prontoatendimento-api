using System.Collections.Generic;

namespace ProntoAtendimento.Domain.Dto.SetorDto
{
    public class SetorPaginacaoViewtDto : BasePaginacaoDto
    {
        public List<SetorViewDto> listaSetorViewDto { get; set; }

        public SetorPaginacaoViewtDto()
        {
            listaSetorViewDto = new List<SetorViewDto>();
        }
    }
}
