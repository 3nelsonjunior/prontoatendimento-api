using ProntoAtendimento.Domain.Dto.TramiteDto;
using ProntoAtendimento.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProntoAtendimento.Repository.Interface
{
    public interface ITramiteRepository : IBaseRepository<Tramite>
    {
        Task<TramiteResultDto> PesquisarTramitePorId(Guid tramiteId);
        Task<TramiteResultDto> PesquisarTramitePorInc(int incTramite);
        Task<ICollection<TramiteResultDto>> PesquisarTramitesPorOcorrencia(Guid ocorrenciaId);
        Task<ICollection<TramiteResultDto>> PesquisarTramitesPorUsuario(Guid usuarioId);
        Task<ICollection<TramiteResultDto>> PesquisarTramitesPorFiltros(TramiteFiltroDto tramiteFiltroDto);
    }
}
