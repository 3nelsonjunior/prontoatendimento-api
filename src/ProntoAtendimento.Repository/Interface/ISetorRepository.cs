using ProntoAtendimento.Domain.Dto.SetorDto;
using ProntoAtendimento.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProntoAtendimento.Repository.Interface
{
    public interface ISetorRepository : IBaseRepository<Setor>
    {
        Task<SetorViewDto> PesquisarSetorPorIdAsync(Guid setorId);
        Task<SetorViewDto> PesquisarSetorPorIncAsync(int incSetor);
        Task<List<SetorSelectboxViewDto>> PesquisarSetoresParaSelectboxAsync();
        Task<SetorPaginacaoViewtDto> PesquisarSetoresPorFiltrosPaginacaoAsync(SetorFiltroDto filtroDto);
        Task<List<SetorViewDto>> PesquisarTodosSetoresAsync();

    }
}
