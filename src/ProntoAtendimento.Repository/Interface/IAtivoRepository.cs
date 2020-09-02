using ProntoAtendimento.Domain.Dto.AtivoDto;
using ProntoAtendimento.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProntoAtendimento.Repository.Interface
{
    public interface IAtivoRepository : IBaseRepository<Ativo>
    {
        Task<AtivoViewDto> PesquisarAtivoPorIdAsync(Guid ativoId);
        Task<AtivoViewDto> PesquisarAtivoPorIncAsync(int incAtivo);
        Task<List<AtivoViewDto>> PesquisarAtivosPorSetorAsync(Guid setorId);
        Task<AtivoPaginacaoViewDto> PesquisarAtivosPorFiltrosPaginacaoAsync(AtivoFiltroDto filtroDto);
    }
}
