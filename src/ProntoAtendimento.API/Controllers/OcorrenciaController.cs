using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProntoAtendimento.Domain.Dto.OcorrenciaDto;
using ProntoAtendimento.Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProntoAtendimento.API.Controllers
{
    [Route("api/ocorrencias")]
    [ApiController]
    public class OcorrenciaController : BasicController
    {
        private readonly IOcorrenciaService _ocorrenciaService;

        public OcorrenciaController(IOcorrenciaService ocorrenciaService,
                                    INotificador notificador) : base(notificador)
        {
            _ocorrenciaService = ocorrenciaService;
        }

        [HttpPost]
        [Route("abrir-ocorrencia")]
        [Authorize(Roles = "ADMIN_TI, ADMIN, DEV, PA")]
        public async Task<ActionResult> CadastrarOcorrencia([FromBody] OcorrenciaPostDto ocorrenciaPostDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _ocorrenciaService.AbrirOcorrencia(ocorrenciaPostDto);

            return CustomResponse(new
            {
                mensagem = "Ocorrência foi aberta com sucesso!",
            });
        }

        [HttpPut]
        [Route("{ocorrenciaId}")]
        [Authorize(Roles = "ADMIN_TI, ADMIN, DEV, PA")]
        public async Task<ActionResult> EditarOcorrencia(Guid ocorrenciaId, [FromBody] OcorrenciaPutDto ocorrenciaPutDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (ocorrenciaId != ocorrenciaPutDto.Id)
            {
                NotificarErro("Id da request diferente do Id da Entidade!");
                return CustomResponse(ocorrenciaPutDto);
            }

            OcorrenciaResultDto ocorrenciaResultDto = await _ocorrenciaService.PesquisarOcorrenciaPorId(ocorrenciaId);

            if (ocorrenciaResultDto == null) return NotFound(new
            {
                success = true,
                status = 404,
                mensagem = "A ocorrência informada não foi encontrada!",
            });

            await _ocorrenciaService.EditarOcorrencia(ocorrenciaPutDto, ocorrenciaResultDto);

            return CustomResponse(new
            {
                mensagem = "Ocorrência foi editada com sucesso!",
            });
        }

        [HttpDelete]
        [Route("{ocorrenciaId}")]
        [Authorize(Roles = "ADMIN_TI, ADMIN, DEV, PA")]
        public async Task<ActionResult> ExcluirOcorrencia(Guid ocorrenciaId)
        {
            OcorrenciaResultDto result = await _ocorrenciaService.PesquisarOcorrenciaPorId(ocorrenciaId);

            if (result == null) return NotFound(new
            {
                success = false,
                status = 404,
                mensagem = "A ocorrência informada não foi encontrada!",
            });

            await _ocorrenciaService.ExcluirOcorrencia(ocorrenciaId);

            return CustomResponse(new
            {
                mensagem = "A ocorrência foi excluída com sucesso!",
            });
        }

        [HttpGet]
        [Route("{ocorrenciaId}")]
        [Authorize(Roles = "ADMIN_TI, ADMIN, DEV, PA")]
        public async Task<ActionResult> PesquisarOcorrenciaPorId(Guid ocorrenciaId)
        {
            OcorrenciaResultDto result = await _ocorrenciaService.PesquisarOcorrenciaPorId(ocorrenciaId);

            if (result == null) return NotFound(new
            {
                success = true,
                status = 404,
                mensagem = "Ocorrência informada não foi encontrada!",
            });

            return CustomResponse(result);
        }

        [HttpGet]
        [Route("pesquisar-por-inc/{incOcorrencia}")]
        [Authorize(Roles = "ADMIN_TI, ADMIN, DEV, PA")]
        public async Task<ActionResult> PesquisarOcorrenciaPorInc(int incOcorrencia)
        {
            OcorrenciaResultDto result = await _ocorrenciaService.PesquisarOcorrenciaPorInc(incOcorrencia);

            if (result == null) return NotFound(new
            {
                success = true,
                status = 404,
                mensagem = "Ocorrência informada não foi encontrada!",
            });

            return CustomResponse(result);
        }

        [HttpGet]
        [Route("pesquisar-por-usuario/{usuarioId}")]
        [Authorize(Roles = "ADMIN_TI, ADMIN, DEV, PA")]
        public async Task<ActionResult> PesquisarOcorrenciasPorUsuario(Guid usuarioId)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            ICollection<OcorrenciaResultDto> listaResult = await _ocorrenciaService.PesquisarOcorrenciasPorUsuario(usuarioId);

            if (listaResult == null || listaResult.Count == 0 || listaResult.Contains(null))
            {
                return NotFound(new
                {
                    success = true,
                    status = 404,
                    mensagem = "Nenhuma ocorrência foi encontrada!",
                });
            }

            return Ok(listaResult);
        }

        [HttpGet]
        [Route("pesquisar-por-turno/{turnoId}")]
        [Authorize(Roles = "ADMIN_TI, ADMIN, DEV, PA")]
        public async Task<ActionResult> PesquisarOcorrenciasPorTurno(Guid turnoId)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            ICollection<OcorrenciaResultDto> listaResult = await _ocorrenciaService.PesquisarOcorrenciasPorTurno(turnoId);

            if (listaResult == null || listaResult.Count == 0 || listaResult.Contains(null))
            {
                return NotFound(new
                {
                    success = true,
                    status = 404,
                    mensagem = "Nenhuma ocorrência foi encontrada!",
                });
            }

            return Ok(listaResult);
        }

        [HttpGet]
        [Route("pesquisar-por-filtros")]
        [Authorize(Roles = "ADMIN_TI, ADMIN, DEV, PA")]
        public async Task<ActionResult> PesquisarOcorrenciasPorFiltros([FromBody] OcorrenciaFiltroDto ocorrenciaFiltroDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            ICollection<OcorrenciaResultDto> listaResult = await _ocorrenciaService.PesquisarOcorrenciasPorFiltros(ocorrenciaFiltroDto);

            if (listaResult == null || listaResult.Count == 0 || listaResult.Contains(null))
            {
                return NotFound(new
                {
                    success = true,
                    status = 404,
                    mensagem = "Nenhuma ocorrência foi encontrada!",
                });
            }

            return Ok(listaResult);
        }
    }
}
