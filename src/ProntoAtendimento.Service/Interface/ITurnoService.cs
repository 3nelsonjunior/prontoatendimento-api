using ProntoAtendimento.Domain.Dto.TurnoDto;
using ProntoAtendimento.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProntoAtendimento.Service.Interface
{
    public interface ITurnoService
    {
        Task<bool> AbrirTurno(TurnoPostDto turnoPostDto);
        Task<bool> ReabrirTurno(Guid turnoId);
        Task<bool> FecharTurno(Guid turnoId);
        Task<bool> EditarTurno(TurnoPutDto turnoPutDto, TurnoResultDto turnoResultDto);
        Task<bool> ExcluirTurno(Guid turnoId);
        Task<TurnoResultDto> PesquisarTurnoPorId(Guid turnoId);
        Task<TurnoResultDto> PesquisarTurnoPorInc(int incTurno);
        Task<ICollection<TurnoResultDto>> PesquisarTurnosPorUsuario(Guid usuarioId);
        Task<TurnoPaginacaoViewtDto> PesquisarTurnosPorFiltrosPaginacaoAsync(TurnoFiltroDto filtroDto);
    }
}
