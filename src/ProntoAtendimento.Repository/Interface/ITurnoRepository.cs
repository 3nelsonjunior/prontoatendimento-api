using ProntoAtendimento.Domain.Dto.TurnoDto;
using ProntoAtendimento.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProntoAtendimento.Repository.Interface
{
    public interface ITurnoRepository : IBaseRepository<Turno>
    {
        Task<Guid> AbrirTurno(Turno turno);
        Task<bool> ReabrirTurno(Guid turnoId);
        Task<bool> FecharTurno(Guid turnoId);
        Task<bool> EditarTurno(Turno turno);
        Task<bool> ExcluirTurno(Guid turnoId);
        Task<bool> PesquisarExisteTurnoAberto();
        Task<bool> PesquisarExisteTurnoDataHoraMaisRecente(DateTime dataHoraInicio);
        Task<bool> PesquisarExisteTurnoDataHoraRepetido(DateTime dataHoraInicio, DateTime dataHoraFim, Guid turnoId);
        Task<TurnoResultDto> PesquisarTurnoPorId(Guid turnoId);
        Task<TurnoResultDto> PesquisarTurnoPorInc(int incTurno);
        Task<ICollection<TurnoResultDto>> PesquisarTurnosPorUsuario(Guid usuarioId);
        Task<TurnoPaginacaoViewtDto> PesquisarTurnosPorFiltrosPaginacaoAsync(TurnoFiltroDto filtroDto);
    }
}
