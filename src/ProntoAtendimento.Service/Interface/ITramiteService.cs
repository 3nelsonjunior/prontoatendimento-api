using ProntoAtendimento.Domain.Dto.TramiteDto;
using ProntoAtendimento.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProntoAtendimento.Service.Interface
{
    public interface ITramiteService
    {
        Task<bool> CadastrarTramite(TramitePostDto tramitePostDto);
        Task<bool> EditarTramite(TramitePutDto tramitePutDto, TramiteResultDto tramiteResultDto);
        Task<bool> ExcluirTramite(Guid tramiteId);
        Task<TramiteResultDto> PesquisarTramitePorId(Guid tramiteId);
        Task<TramiteResultDto> PesquisarTramitePorInc(int incTramite);
        Task<ICollection<TramiteResultDto>> PesquisarTramitesPorFiltros(TramiteFiltroDto tramiteFiltroDto);
        Task<ICollection<TramiteResultDto>> PesquisarTramitePorOcorrencia(Guid ocorrenciaId);
        Task<ICollection<TramiteResultDto>> PesquisarTramitePorUsuario(Guid usuarioId);
    }
}
