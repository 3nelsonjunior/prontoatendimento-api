using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProntoAtendimento.Domain.Dto.UsuarioDto;
using ProntoAtendimento.Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProntoAtendimento.API.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuariosController : BasicController
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService,
                                  INotificador notificador) : base(notificador)
        {
            _usuarioService = usuarioService;
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> CadastrarUsuario([FromBody] UsuarioPostDto postDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _usuarioService.CadastrarUsuarioAsync(postDto);

            if (result == null)
            {
                return CustomResponse(postDto);
            }

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    NotificarErro(item.Description);
                    return CustomResponse(postDto);
                }
            }

            return CustomResponse(new
            {
                matricula = postDto.Matricula,
                mensagem = "O usuário foi cadastrado com sucesso!",
            });
        }

        [HttpPut]
        [Route("{usuarioId}")]
        [Authorize(Roles = "ADMIN_TI, ADMIN, DEV, PA")]
        public async Task<ActionResult> EditarUsuario(Guid usuarioId, [FromBody] UsuarioPutDto putDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if(usuarioId != putDto.Id)
            {
                NotificarErro("Id da request diferente do Id da Entidade!");
                return CustomResponse(putDto);
            }

            UsuarioViewDto viewDto = await _usuarioService.PesquisarUsuarioPorIdAsync(usuarioId);

            if (viewDto == null) return NotFound(new
            {
                success = false,
                status = 404,
                mensagem = "O usuário informado não foi encontrado!",
            });


            var result = await _usuarioService.EditarUsuarioAsync(putDto, viewDto);
            
            if(result == null)
            {
                return CustomResponse(putDto);
            }
            
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    NotificarErro(item.Description);
                    return CustomResponse(putDto);
                }
            }
            
            return CustomResponse(new
            {
                matricula = putDto.Matricula,
                mensagem = "O usuário foi editado com sucesso!",
            });


        }



        [HttpDelete]
        [Route("{usuarioId}")]
        [Authorize(Roles = "ADMIN_TI, ADMIN, DEV, PA")]
        public async Task<ActionResult> ExcluirUsuario(Guid usuarioId)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            UsuarioViewDto viewDto = await _usuarioService.PesquisarUsuarioPorIdAsync(usuarioId);

            if (viewDto == null) return NotFound(new
            {
                success = false,
                status = 404,
                mensagem = "O usuário informado não foi encontrado!",
            });

            await _usuarioService.ExcluirUsuarioAsync(usuarioId);

            return CustomResponse(new
            {
                mensagem = "O usuário foi excluído com sucesso!",
            });
        }

        [HttpGet]
        [Route("{usuarioId}")]
        [Authorize(Roles = "ADMIN_TI, ADMIN, DEV, PA")]
        public async Task<ActionResult> PesquisarUsuarioPorId(Guid usuarioId)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            UsuarioViewDto viewDto = await _usuarioService.PesquisarUsuarioPorIdAsync(usuarioId);

            if (viewDto == null) return NotFound(new
            {
                success = false,
                status = 404,
                mensagem = "O usuário informado não foi encontrado!",
            });

            return CustomResponse(viewDto);
        }

        [HttpGet]
        [Route("pesquisar-por-matricula/{matricula}")]
        [Authorize(Roles = "ADMIN_TI, ADMIN, DEV, PA")]
        public async Task<ActionResult> PesquisarUsuarioPorMatricula(string matricula)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            UsuarioViewDto viewDto = await _usuarioService.PesquisarUsuarioPorMatriculaAsync(matricula);

            if (viewDto == null) return NotFound(new
            {
                success = false,
                status = 404,
                mensagem = "O usuário informado não foi encontrado!",
            });

            return CustomResponse(viewDto);
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN_TI, ADMIN, DEV, PA")]
        public async Task<ActionResult> PesquisarTodosUsuariosAsync()
        {
            List<UsuarioTabelaViewDto> viewDto = await _usuarioService.PesquisarTodosUsuariosAsync();

            if (viewDto == null || viewDto.Count == 0 || viewDto.Contains(null))
            {
                return NotFound(new
                {
                    success = false,
                    status = 404,
                    mensagem = "Nenhum usuário foi encontrado!",
                });
            }

            return CustomResponse(viewDto);
        }

        [HttpGet]
        [Route("pesquisar-para-selectbox")]
        [Authorize(Roles = "ADMIN_TI, ADMIN, CONSULTA, DEV, PA, TI")]
        public async Task<ActionResult> obterUsuariosPerfilPaParaSelectbox()
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            List<UsuarioPerfilPaSelectboxViewDto> listaViewDto = await _usuarioService.PesquisarUsuariosPerfilPaParaSelectboxAsync();

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
        public async Task<ActionResult> PesquisarUsuariosPorFiltrosPaginacao([FromQuery] UsuarioFiltroDto usuarioFiltroDto)
        {
            UsuarioPaginacaoViewtDto viewDto = await _usuarioService.PesquisarUsuariosPorFiltrosPaginacaoAsync(usuarioFiltroDto);

            if (viewDto.ListaUsuarioTabelaViewDto == null || viewDto.ListaUsuarioTabelaViewDto.Count == 0 || viewDto.ListaUsuarioTabelaViewDto.Contains(null))
            {
                return NotFound(new
                {
                    success = false,
                    status = 404,
                    mensagem = "Nenhum usuário foi encontrado!",
                });
            }
            
            return CustomResponse(viewDto);
        }

        [HttpGet]
        [Route("pesquisar-usuario-perfil-por-paginacao")]
        [Authorize(Roles = "ADMIN_TI, ADMIN, DEV, PA")]
        public async Task<ActionResult> PesquisarUsuariosPorFiltrosPaginacaoParaPaginaPerfil([FromQuery] UsuarioPerfilFiltroDto usuarioFiltroDto)
        {
            UsuarioPerfilPaginacaoViewDto viewDto = await _usuarioService.PesquisarUsuariosPorFiltrosPaginacaoParaPaginaPerfilAsync(usuarioFiltroDto);

            if (viewDto.ListaUsuarioPerfilTabelaViewDto == null || viewDto.ListaUsuarioPerfilTabelaViewDto.Count == 0 || viewDto.ListaUsuarioPerfilTabelaViewDto.Contains(null))
            {
                return NotFound(new
                {
                    success = false,
                    status = 404,
                    mensagem = "Nenhum usuário foi encontrado!",
                });
            }

            return CustomResponse(viewDto);
        }
    }
}
