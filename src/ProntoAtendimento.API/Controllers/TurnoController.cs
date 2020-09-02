using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProntoAtendimento.Domain.Dto.TurnoDto;
using ProntoAtendimento.Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProntoAtendimento.API.Controllers
{
    [Route("api/turnos")]
    [ApiController]
    public class TurnoController : BasicController
    {
        private readonly ITurnoService _turnoService;

        public TurnoController(ITurnoService turnoService,
                               INotificador notificador) : base(notificador)
        {
            _turnoService = turnoService;
        }

        [HttpPost]
        [Route("abrir-turno")]
        [Authorize(Roles = "ADMIN, DEV, PA")]
        public async Task<ActionResult> AbrirTurno([FromBody] TurnoPostDto turnoPostDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _turnoService.AbrirTurno(turnoPostDto);

            return CustomResponse(new
            {
                mensagem = "O turno foi cadastrado com sucesso!",
            });
        }

        [HttpPut]
        [Route("reabrir-turno/{turnoId}")]
        [Authorize(Roles = "ADMIN, DEV, PA")]
        public async Task<ActionResult> ReabrirTurno(Guid turnoId)
        {
            TurnoResultDto turnoResultDto = await _turnoService.PesquisarTurnoPorId(turnoId);

            if (turnoResultDto == null) return NotFound(new
            {
                success = true,
                status = 404,
                mensagem = "O turno informado não foi encontrado!",
            });

           await _turnoService.ReabrirTurno(turnoId);

            return CustomResponse(new
            {
                mensagem = "O turno foi reaberto com sucesso!",
            });
        }

        [HttpPut]
        [Route("fechar-turno/{turnoId}")]
        [Authorize(Roles = "ADMIN, DEV, PA")]
        public async Task<ActionResult> FecharTurno(Guid turnoId)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            TurnoResultDto turnoResultDto = await _turnoService.PesquisarTurnoPorId(turnoId);

            if (turnoResultDto == null) return NotFound(new
            {
                success = true,
                status = 404,
                mensagem = "O turno informado não foi encontrado!",
            });

            await _turnoService.FecharTurno(turnoId);

            return CustomResponse(new
            {
                mensagem = "O turno foi fechado com sucesso!",
            });
        }

        [HttpPut]
        [Route("editar-turno/{turnoId}")]
        [Authorize(Roles = "ADMIN, DEV, PA")]
        public async Task<ActionResult> EditarTurno(Guid turnoId, [FromBody] TurnoPutDto turnoPutDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            
            if (turnoId != turnoPutDto.Id)
            {
                NotificarErro("Id da request diferente do Id da Entidade!");
                return CustomResponse(turnoPutDto);
            }
                        
            TurnoResultDto turnoResultDto = await _turnoService.PesquisarTurnoPorId(turnoId);

            if (turnoResultDto == null) return NotFound(new
            {
                success = true,
                status = 404,
                mensagem = "O turno informado não foi encontrado!",
            });

            await _turnoService.EditarTurno(turnoPutDto, turnoResultDto);

            return CustomResponse(new
            {
                mensagem = "O turno foi editado com sucesso!",
            });
        }

        [HttpDelete]
        [Route("{turnoId}")]
        [Authorize(Roles = "ADMIN, DEV, PA")]
        public async Task<ActionResult> ExcluirTurno(Guid turnoId)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            TurnoResultDto turnoResultDto = await _turnoService.PesquisarTurnoPorId(turnoId);

            if (turnoResultDto == null) return NotFound(new
            {
                success = true,
                status = 404,
                mensagem = "O turno informado não foi encontrado!",
            });

            await _turnoService.ExcluirTurno(turnoId);

            return CustomResponse(new
            {
                mensagem = "O turno foi excluído com sucesso!",
            });
        }

        [HttpGet]
        [Route("{turnoId}")]
        [Authorize(Roles = "ADMIN, DEV, PA")]
        public async Task<ActionResult> PesquisarTurnoPorId(Guid turnoId)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            TurnoResultDto turnoResultDto = await _turnoService.PesquisarTurnoPorId(turnoId);

            if (turnoResultDto == null) return NotFound(new
            {
                success = true,
                status = 404,
                mensagem = "O turno informado não foi encontrado!",
            });

            return CustomResponse(turnoResultDto);
        }

        [HttpGet]
        [Route("pesquisar-por-inc/{incTurno}")]
        [Authorize(Roles = "ADMIN, DEV, PA")]
        public async Task<ActionResult> PesquisarTurnoPorInc(int incTurno)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            TurnoResultDto turnoResultDto = await _turnoService.PesquisarTurnoPorInc(incTurno);

            if (turnoResultDto == null) return NotFound(new
            {
                success = true,
                status = 404,
                mensagem = "O turno informado não foi encontrado!",
            });

            return CustomResponse(turnoResultDto);
        }

        [HttpGet]
        [Route("pesquisar-por-usuario/{usuarioId}")]
        [Authorize(Roles = "ADMIN, DEV, PA")]
        public async Task<ActionResult> PesquisarTurnosPorUsuario(Guid usuarioId)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            ICollection<TurnoResultDto> listaTurnoResultDto = await _turnoService.PesquisarTurnosPorUsuario(usuarioId);

            if (listaTurnoResultDto == null || listaTurnoResultDto.Count == 0 || listaTurnoResultDto.Contains(null))
            {
                return NotFound(new
                {
                    success = true,
                    status = 404,
                    mensagem = "Nenhum turno foi encontrado!",
                });
            }

            return CustomResponse(listaTurnoResultDto);
        }

        [HttpGet]
        [Route("pesquisar-por-paginacao")]
        [Authorize(Roles = "ADMIN, DEV, PA")]
        public async Task<ActionResult> PesquisarTurnosPorFiltrosPaginacao([FromQuery] TurnoFiltroDto turnoFiltroDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            TurnoPaginacaoViewtDto turnoPaginacaoViewtDto = await _turnoService.PesquisarTurnosPorFiltrosPaginacaoAsync(turnoFiltroDto);

            if (turnoPaginacaoViewtDto.ListaTurnoTabelaViewDto == null || turnoPaginacaoViewtDto.ListaTurnoTabelaViewDto.Count == 0 || turnoPaginacaoViewtDto.ListaTurnoTabelaViewDto.Contains(null))
            {
                return NotFound(new
                {
                    success = true,
                    status = 404,
                    mensagem = "Nenhum turno foi encontrado!",
                });
            }

            return CustomResponse(turnoPaginacaoViewtDto);
        }
    }
}
