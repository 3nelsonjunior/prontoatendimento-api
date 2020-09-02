using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProntoAtendimento.Domain.Dto.AtivoDto;
using ProntoAtendimento.Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProntoAtendimento.API.Controllers
{
    [Route("api/ativos")]
    [ApiController]
    public class AtivoController : BasicController
    {
        private readonly IAtivoService _ativoService;

        public AtivoController(IAtivoService ativoService,
                               INotificador notificador) : base(notificador)
        {
            _ativoService = ativoService;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN_TI, ADMIN, DEV, PA")]
        public async Task<ActionResult> CadastrarAtivo([FromBody] AtivoPostDto ativoPostDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _ativoService.CadastrarAtivoAsync(ativoPostDto);

            AtivoViewDto viewDto = await _ativoService.PesquisarAtivoPorIdAsync(ativoPostDto.Id);

            return CustomResponse(new
            {
                Inc = viewDto.IncAtivo,
                mensagem = "Ativo foi cadastrado com sucesso!",
            });
        }

        [HttpPut]
        [Route("{ativoId}")]
        [Authorize(Roles = "ADMIN_TI, ADMIN, DEV, PA")]
        public async Task<ActionResult> EditarAtivo(Guid ativoId, [FromBody] AtivoPutDto ativoPutDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (ativoId != ativoPutDto.Id)
            {
                NotificarErro("Id da request diferente do Id da Entidade!");
                return CustomResponse(ativoPutDto);
            }

            AtivoViewDto viewDto = await _ativoService.PesquisarAtivoPorIdAsync(ativoId);

            if (viewDto == null) return NotFound(new
            {
                success = true,
                status = 404,
                mensagem = "O ativo informado não foi encontrado!",
            });

            await _ativoService.EditarAtivoAsync(ativoPutDto, viewDto);

            return CustomResponse(new
            {
                Inc = viewDto.IncAtivo,
                mensagem = "Ativo foi editado com sucesso!",
            });
        }

        [HttpDelete]
        [Route("{ativoId}")]
        [Authorize(Roles = "ADMIN_TI, ADMIN, DEV, PA")]
        public async Task<ActionResult> ExcluirAtivo(Guid ativoId)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            AtivoViewDto viewDto = await _ativoService.PesquisarAtivoPorIdAsync(ativoId);

            if (viewDto == null) return NotFound(new
            {
                success = true,
                status = 404,
                mensagem = "O ativo informado não foi encontrado!",
            });

            await _ativoService.ExcluirAtivoAsync(ativoId);

            return CustomResponse(new { 
                mensagem = "Ativo foi excluído com sucesso!",
             });
        }

        [HttpGet]
        [Route("{ativoId}")]
        [Authorize(Roles = "ADMIN_TI, ADMIN, DEV, PA")]
        public async Task<ActionResult> PesquisarAtivoPorId(Guid ativoId)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            AtivoViewDto viewDto = await _ativoService.PesquisarAtivoPorIdAsync(ativoId);

            if (viewDto == null) return NotFound(new
            {
                success = true,
                status = 404,
                mensagem = "O ativo informado não foi encontrado!",
            });

            return CustomResponse(viewDto);
        }

        [HttpGet]
        [Route("pesquisar-por-inc/{incAtivo}")]
        [Authorize(Roles = "ADMIN_TI, ADMIN, DEV, PA")]
        public async Task<ActionResult> PesquisarAtivoPorInc(int incAtivo)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            AtivoViewDto viewDto = await _ativoService.PesquisarAtivoPorIncAsync(incAtivo);

            if (viewDto == null) return NotFound(new
            {
                success = true,
                status = 404,
                mensagem = "O ativo informado não foi encontrado!",
            });

            return CustomResponse(viewDto);
        }

        [HttpGet]
        [Route("perquisar-por-setor/{setorId}")]
        [Authorize(Roles = "ADMIN_TI, ADMIN, DEV, PA")]
        public async Task<ActionResult> PesquisarAtivosPorSetor(Guid setorId)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            List<AtivoViewDto> listaAtivoResultDto = await _ativoService.PesquisarAtivosPorSetorAsync(setorId);

            if (listaAtivoResultDto == null || listaAtivoResultDto.Count == 0 || listaAtivoResultDto.Contains(null))
            {
                return NotFound(new
                {
                    success = true,
                    status = 404,
                    mensagem = "Nenhum ativo foi encontrado!",
                });
            }

            return CustomResponse(listaAtivoResultDto);
        }

        [HttpGet]
        [Route("pesquisar-por-paginacao")]
        [Authorize(Roles = "ADMIN_TI, ADMIN, DEV, PA")]
        public async Task<ActionResult> PesquisarAtivosPorFiltrosComPaginacaoAsync([FromQuery] AtivoFiltroDto ativoFiltroDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            AtivoPaginacaoViewDto ativoPaginacaoViewDto = await _ativoService.PesquisarAtivosPorFiltrosPaginacaoAsync(ativoFiltroDto);

            if (ativoPaginacaoViewDto.listaAtivoTabelaViewDto == null || ativoPaginacaoViewDto.listaAtivoTabelaViewDto.Count == 0 || ativoPaginacaoViewDto.listaAtivoTabelaViewDto.Contains(null))
            {
                return NotFound(new
                {
                    success = true,
                    status = 404,
                    mensagem = "Nenhum ativo foi encontrado!",
                });
            }

            return CustomResponse(ativoPaginacaoViewDto);
        }
    }
}
