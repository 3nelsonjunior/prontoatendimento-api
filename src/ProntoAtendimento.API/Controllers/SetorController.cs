using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProntoAtendimento.Domain.Dto.SetorDto;
using ProntoAtendimento.Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProntoAtendimento.API.Controllers
{
    [Route("api/setores")]
    [ApiController]
    public class SetorController : BasicController
    {
        private readonly ISetorService _setorService;

        public SetorController(ISetorService setorService,
                               INotificador notificador) : base(notificador)
        {
            _setorService = setorService;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN_TI, ADMIN, DEV, PA")]
        public async Task<ActionResult> CadastrarSetor([FromBody] SetorPostDto setorPostDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _setorService.CadastrarSetorAsync(setorPostDto);

            SetorViewDto viewDto = await _setorService.PesquisarSetorPorIdAsync(setorPostDto.Id);

            return CustomResponse(new
            {
                Inc = viewDto.IncSetor,
                mensagem = "O setor foi cadastrado com sucesso!",
            });
        }

        [HttpPut]
        [Route("{setorId}")]
        [Authorize(Roles = "ADMIN_TI, ADMIN, DEV, PA")]
        public async Task<ActionResult> EditarSetor(Guid setorId, [FromBody] SetorPutDto setorPutDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            
            if (setorId != setorPutDto.Id)
            {
                NotificarErro("Id da request diferente do Id da Entidade!");
                return CustomResponse(setorPutDto);
            }

            SetorViewDto viewDto = await _setorService.PesquisarSetorPorIdAsync(setorId);

            if (viewDto == null) return NotFound(new
            {
                success = true,
                status = 404,
                mensagem = "O setor informado não foi encontrado!",
            });

            await _setorService.EditarSetorAsync(setorPutDto, viewDto);

            return CustomResponse(new
            {
                Inc = setorPutDto.IncSetor,
                mensagem = "O setor foi editado com sucesso!",
            });
        }

        [HttpDelete]
        [Route("{setorId}")]
        [Authorize(Roles = "ADMIN_TI, ADMIN, DEV, PA")]
        public async Task<ActionResult> ExcluirSetor(Guid setorId)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            SetorViewDto viewDto = await _setorService.PesquisarSetorPorIdAsync(setorId);

            if (viewDto == null) return NotFound(new
            {
                success = true,
                status = 404,
                mensagem = "O setor informado não foi encontrado!",
            });

            await _setorService.ExcluirSetorAsync(setorId);

            return CustomResponse(new
            {
                mensagem = "O setor foi excluído com sucesso!",
            });
        }

        [HttpGet]
        [Route("{setorId}")]
        [Authorize(Roles = "ADMIN_TI, ADMIN, DEV, PA")]
        public async Task<ActionResult> PesquisarSetorPorId(Guid setorId)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            SetorViewDto viewDto = await _setorService.PesquisarSetorPorIdAsync(setorId);

            if (viewDto == null) return NotFound(new
            {
                success = true,
                status = 404,
                mensagem = "O setor informado não foi encontrado!",
            });

            return CustomResponse(viewDto);
        }

        [HttpGet]
        [Route("pesquisar-por-inc/{incSetor}")]
        [Authorize(Roles = "ADMIN_TI, ADMIN, DEV, PA")]
        public async Task<ActionResult> PesquisarSetorPorInc(int incSetor)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            SetorViewDto viewDto = await _setorService.PesquisarSetorPorIncAsync(incSetor);

            if (viewDto == null) return NotFound(new
            {
                success = true,
                status = 404,
                mensagem = "O setor informado não foi encontrado!",
            });

            return CustomResponse(viewDto);
        }


        [HttpGet]
        [Authorize(Roles = "ADMIN_TI, ADMIN, DEV, PA")]
        public async Task<ActionResult> PesquisarTodosSetores()
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            ICollection<SetorViewDto> listaSetorResultDto = await _setorService.PesquisarTodosSetoresAsync();

            if (listaSetorResultDto == null || listaSetorResultDto.Count == 0 || listaSetorResultDto.Contains(null))
            {
                return NotFound(new
                {
                    success = true,
                    status = 404,
                    mensagem = "Nenhum setor foi encontrado!",
                });
            }

            return CustomResponse(listaSetorResultDto);
        }

        [HttpGet]
        [Route("pesquisar-para-selectbox")]
        [Authorize(Roles = "ADMIN_TI, ADMIN, CONSULTA, DEV, PA, TI")]
        public async Task<ActionResult> PesquisarSetoresParaSelectbox()
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            List<SetorSelectboxViewDto> listaViewDto = await _setorService.PesquisarSetoresParaSelectboxAsync();

            if (listaViewDto == null || listaViewDto.Count == 0 || listaViewDto.Contains(null))
            {
                return NotFound(new
                {
                    success = true,
                    status = 404,
                    mensagem = "Nenhum setor foi encontrado!",
                });
            }

            return CustomResponse(listaViewDto);
        }

        [HttpGet]
        [Route("pesquisar-por-paginacao")]
        [Authorize(Roles = "ADMIN_TI, ADMIN, DEV, PA")]
        public async Task<ActionResult> PesquisarSetoresPorFiltrosComPaginacaoAsync([FromQuery] SetorFiltroDto filtroDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            SetorPaginacaoViewtDto paginacaoViewDto = await _setorService.PesquisarSetoresPorFiltrosPaginacaoAsync(filtroDto);

            if (paginacaoViewDto.listaSetorViewDto == null || paginacaoViewDto.listaSetorViewDto.Count == 0 || paginacaoViewDto.listaSetorViewDto.Contains(null))
            {
                return NotFound(new
                {
                    success = true,
                    status = 404,
                    mensagem = "Nenhum setor foi encontrado!",
                });
            }

            return CustomResponse(paginacaoViewDto);
        }

    }
}
