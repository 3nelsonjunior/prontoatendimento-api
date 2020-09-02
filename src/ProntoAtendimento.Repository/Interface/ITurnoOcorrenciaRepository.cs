using ProntoAtendimento.Domain.Dto.OcorrenciaDto;
using ProntoAtendimento.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProntoAtendimento.Repository.Interface
{
    public interface ITurnoOcorrenciaRepository
    {
        Task<bool> CadastrarTurnoOcorrencia(TurnoOcorrencia turnoOcorrencia);
        Task<bool> AdicionarOcorrenciasEmAbertoAoTurno(Guid turnoId, ICollection<OcorrenciaResultDto> listaOcorrenciasEmAberto);
        Task<bool> EditarStatusTurnoOcorrencia(TurnoOcorrencia turnoOcorrencia);
        Task<bool> ExcluirTurnoOcorrencia(TurnoOcorrencia turnoOcorrencia);
        Task<TurnoOcorrencia> PesquisarTurnoOcorrencia(Guid turnoId, Guid ocorrenciaId);
        Task<ICollection<TurnoOcorrencia>> PesquisarTurnoOcorrenciaPorTurno(Guid turnoId);
        Task<ICollection<TurnoOcorrencia>> PesquisarTurnoOcorrenciaPorOcorrencia(Guid ocorrenciaId);

    }
}
