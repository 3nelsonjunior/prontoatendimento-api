using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProntoAtendimento.Domain.Dto.TramiteDto;
using ProntoAtendimento.Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProntoAtendimento.API.Controllers
{
    [Route("api/tramites")]
    [ApiController]
    public class TramiteController : BasicController
    {
        private readonly ITramiteService _tramiteService;

        public TramiteController(ITramiteService tramiteService,
                               INotificador notificador) : base(notificador)
        {
            _tramiteService = tramiteService;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN_TI, ADMIN, DEV, PA")]
        public async Task<ActionResult> CadastrarTramite([FromBody] TramitePostDto tramitePostDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _tramiteService.CadastrarTramite(tramitePostDto);

            return CustomResponse(new
            {
                mensagem = "O trâmite foi cadastrado com sucesso!",
            });
        }

        [HttpPut]
        [Route("{tramiteId}")]
        [Authorize(Roles = "ADMIN_TI, ADMIN, DEV, PA")]
        public async Task<ActionResult> EditarTramite(Guid tramiteId, [FromBody] TramitePutDto tramitePutDto)
        {
            if (tramiteId != tramitePutDto.Id)
            {
                NotificarErro("Id da request diferente do Id da Entidade!");
                return CustomResponse(tramitePutDto);
            }

            TramiteResultDto tramiteResultDto = await _tramiteService.PesquisarTramitePorId(tramiteId);

            if (tramiteResultDto == null) return NotFound(new
            {
                success = true,
                status = 404,
                mensagem = "O trâmite informado não foi encontrado!",
            });

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _tramiteService.EditarTramite(tramitePutDto, tramiteResultDto);

            return CustomResponse(new
            {
                mensagem = "O trâmite foi editado com sucesso!",
            });
        }

        [HttpDelete]
        [Route("{tramiteId}")]
        [Authorize(Roles = "ADMIN_TI, ADMIN, DEV, PA")]
        public async Task<ActionResult> ExcluirTramite(Guid tramiteId)
        {
            TramiteResultDto tramiteResultDto = await _tramiteService.PesquisarTramitePorId(tramiteId);

            if (tramiteResultDto == null) return NotFound(new
            {
                success = true,
                status = 404,
                mensagem = "O trâmite informado não foi encontrado!",
            });

            await _tramiteService.ExcluirTramite(tramiteId);

            return CustomResponse(new
            {
                mensagem = "O trâmite foi excluído com sucesso!",
            });
        }

        [HttpGet]
        [Route("{tramiteId}")]
        [Authorize(Roles = "ADMIN_TI, ADMIN, DEV, PA")]
        public async Task<ActionResult> PesquisarTramitePorId(Guid tramiteId)
        {
            TramiteResultDto tramiteResultDto = await _tramiteService.PesquisarTramitePorId(tramiteId);

            if (tramiteResultDto == null) return NotFound(new
            {
                success = true,
                status = 404,
                mensagem = "O trâmite informado não foi encontrado!",
            });

            return CustomResponse(tramiteResultDto);
        }

        [HttpGet]
        [Route("pesquisar-por-inc/{incTramite}")]
        [Authorize(Roles = "ADMIN_TI, ADMIN, DEV, PA")]
        public async Task<ActionResult> PesquisarTramitePorInc(int incTramite)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            TramiteResultDto tramiteResultDto = await _tramiteService.PesquisarTramitePorInc(incTramite);

            if (tramiteResultDto == null) return NotFound(new
            {
                success = true,
                status = 404,
                mensagem = "O setor informado não foi encontrado!",
            });

            return CustomResponse(tramiteResultDto);
        }

        [HttpGet]
        [Route("perquisar-por-ocorrencia/{ocorrenciaId}")]
        [Authorize(Roles = "ADMIN_TI, ADMIN, DEV, PA")]
        public async Task<ActionResult> PesquisarTramitePorOcorrencia(Guid ocorrenciaId)
        {
            ICollection<TramiteResultDto> listaTramiteResultDto = await _tramiteService.PesquisarTramitePorOcorrencia(ocorrenciaId);

            if (listaTramiteResultDto == null || listaTramiteResultDto.Count == 0 || listaTramiteResultDto.Contains(null))
            {
                return NotFound(new
                {
                    success = true,
                    status = 404,
                    mensagem = "Nenhum trâmite foi encontrado!",
                });
            }

            return Ok(listaTramiteResultDto);
        }

        [HttpGet]
        [Route("pesquisar-por-filtros")]
        [Authorize(Roles = "ADMIN_TI, ADMIN, DEV, PA")]
        public async Task<ActionResult> PesquisarTramitesPorFiltros([FromBody] TramiteFiltroDto tramiteFiltroDto)
        {
            ICollection<TramiteResultDto> listaTramiteResultDto = await _tramiteService.PesquisarTramitesPorFiltros(tramiteFiltroDto);

            if (listaTramiteResultDto == null || listaTramiteResultDto.Count == 0 || listaTramiteResultDto.Contains(null))
            {
                return NotFound(new
                {
                    success = true,
                    status = 404,
                    mensagem = "Nenhum trâmite foi encontrado!",
                });
            }

            return Ok(listaTramiteResultDto);
        }
    }
}
