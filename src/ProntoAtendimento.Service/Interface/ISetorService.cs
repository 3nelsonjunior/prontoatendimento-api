using ProntoAtendimento.Domain.Dto.SetorDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProntoAtendimento.Service.Interface
{
    public interface ISetorService
    {
        Task<bool> CadastrarSetorAsync(SetorPostDto setorPostDto);
        Task<bool> EditarSetorAsync(SetorPutDto setorPutDtor, SetorViewDto setorResultDto);
        Task<bool> ExcluirSetorAsync(Guid setorId);
        Task<SetorViewDto> PesquisarSetorPorIdAsync(Guid setorId);
        Task<SetorViewDto> PesquisarSetorPorIncAsync(int incSetor);
        Task<List<SetorSelectboxViewDto>> PesquisarSetoresParaSelectboxAsync();
        Task<SetorPaginacaoViewtDto> PesquisarSetoresPorFiltrosPaginacaoAsync(SetorFiltroDto filtroDto);
        Task<List<SetorViewDto>> PesquisarTodosSetoresAsync();
    }
}
