using ProntoAtendimento.Domain.Dto.AtivoDto;
using ProntoAtendimento.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProntoAtendimento.Service.Interface
{
    public interface IAtivoService
    {
        Task<bool> CadastrarAtivoAsync(AtivoPostDto ativoPostDto);
        Task<bool> EditarAtivoAsync(AtivoPutDto ativoPutDto, AtivoViewDto viewDto);
        Task<bool> ExcluirAtivoAsync(Guid ativoId);
        Task<AtivoViewDto> PesquisarAtivoPorIdAsync(Guid ativoId);
        Task<AtivoViewDto> PesquisarAtivoPorIncAsync(int incAtivo);
        Task<List<AtivoViewDto>> PesquisarAtivosPorSetorAsync(Guid setorId);
        Task<AtivoPaginacaoViewDto> PesquisarAtivosPorFiltrosPaginacaoAsync(AtivoFiltroDto filtroDto);
    }
}
