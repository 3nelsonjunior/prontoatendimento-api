using ProntoAtendimento.Domain.Dto.OcorrenciaDto;
using ProntoAtendimento.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProntoAtendimento.Repository.Interface
{
    public interface IOcorrenciaRepository : IBaseRepository<Ocorrencia>
    {
        Task<OcorrenciaResultDto> AbrirOcorrencia(Ocorrencia ocorrencia);
        Task<bool> ExcluirOcorrencia(Guid ocorrenciaId);
        Task<ICollection<OcorrenciaResultDto>> PesquisarOcorrenciasEmAndamento();
        Task<OcorrenciaResultDto> PesquisarOcorrenciaPorId(Guid ocorrenciaId);
        Task<OcorrenciaResultDto> PesquisarOcorrenciaPorInc(int incOcorrencia);
        Task<ICollection<OcorrenciaResultDto>> PesquisarOcorrenciasPorUsuario(Guid usuarioId);
        Task<ICollection<OcorrenciaResultDto>> PesquisarOcorrenciasPorTurno(Guid turnoId);
        Task<ICollection<OcorrenciaResultDto>> PesquisarOcorrenciasPorFiltros(OcorrenciaFiltroDto ocorrenciaFiltroDto);
    }
}
