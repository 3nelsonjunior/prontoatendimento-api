using ProntoAtendimento.Domain.Dto.OcorrenciaDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProntoAtendimento.Service.Interface
{
    public interface IOcorrenciaService
    {
        Task<bool> AbrirOcorrencia(OcorrenciaPostDto ocorrenciaPostDto);
        Task<bool> EditarOcorrencia(OcorrenciaPutDto ocorrenciaPutDto, OcorrenciaResultDto ocorrenciaResultDto);
        Task<bool> ExcluirOcorrencia(Guid id);
        Task<OcorrenciaResultDto> PesquisarOcorrenciaPorId(Guid ocorrenciaId);
        Task<OcorrenciaResultDto> PesquisarOcorrenciaPorInc(int incOcorrencia);
        Task<ICollection<OcorrenciaResultDto>> PesquisarOcorrenciasPorUsuario(Guid usuarioId);
        Task<ICollection<OcorrenciaResultDto>> PesquisarOcorrenciasPorTurno(Guid turnoId);
        Task<ICollection<OcorrenciaResultDto>> PesquisarOcorrenciasEmAndamento();
        Task<ICollection<OcorrenciaResultDto>> PesquisarOcorrenciasPorFiltros(OcorrenciaFiltroDto ocorrenciaFiltroDto);
    }
}
