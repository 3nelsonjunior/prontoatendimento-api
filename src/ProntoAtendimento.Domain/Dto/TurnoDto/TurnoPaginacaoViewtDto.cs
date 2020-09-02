using System.Collections.Generic;

namespace ProntoAtendimento.Domain.Dto.TurnoDto
{
    public class TurnoPaginacaoViewtDto : BasePaginacaoDto
    {
        public List<TurnoTabelaViewDto> ListaTurnoTabelaViewDto { get; set; }

        public TurnoPaginacaoViewtDto()
        {
            ListaTurnoTabelaViewDto = new List<TurnoTabelaViewDto>();
        }
    }
}
