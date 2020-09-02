using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProntoAtendimento.Domain.Dto.PerfilDto;
using ProntoAtendimento.Domain.Entity;
using ProntoAtendimento.Domain.Identity;
using ProntoAtendimento.Service.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProntoAtendimento.API.Controllers
{
    [Route("api/perfis")]
    [ApiController]
    public class PerfilController : BasicController
    {
        private readonly RoleManager<Perfil> _roleManager;
        private readonly UserManager<Usuario> _userManager;

        public PerfilController(INotificador notificador,
                                RoleManager<Perfil> roleManager,
                                UserManager<Usuario> userManager) : base(notificador)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN_TI, ADMIN, DEV, PA")]
        public async Task<IActionResult> CriarPerfil(PerfilPostDto postDto)
        {

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _roleManager.CreateAsync(new Perfil { Name = postDto.Nome });

            if(result.Succeeded)
            {
                return CustomResponse(postDto);
            }

            foreach (var error in result.Errors)
            {
                NotificarErro(error.Description);
            }

            return CustomResponse(postDto);
           
        }

        [HttpPut("editar-perfis")]
        [Authorize(Roles = "ADMIN_TI, ADMIN, DEV, PA")]
        public async Task<IActionResult> EditarPerfisUsuario(UsuarioPerfilPutDto putDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if(putDto.ListaPerfis.Count == 0)
            {
                NotificarErro("O usuário precisa ter um perfil de acesso!");
                return CustomResponse(putDto);
            }

            var user = await _userManager.FindByNameAsync(putDto.Matricula);

            if (user != null)
            {
                var perfisAntigos = await _userManager.GetRolesAsync(user);
                var result = await _userManager.RemoveFromRolesAsync(user, perfisAntigos);
                result = await _userManager.AddToRolesAsync(user, putDto.ListaPerfis);

                if (result.Succeeded)
                {
                    if (result.Succeeded)
                    {
                        return CustomResponse(new
                        {
                            matricula = putDto.Matricula,
                            mensagem = "O perfil foi alterado com sucesso!",
                        });
                    }
                }

                foreach (var error in result.Errors)
                {
                    NotificarErro(error.Description);
                }

                return CustomResponse(putDto);

            }    

            return NotFound(new
            {
                success = false,
                status = 404,
                mensagem = "O usuário ou perfil informado não foi encontrado!",
            });

        }


        [HttpGet]
        [Authorize(Roles = "ADMIN_TI, ADMIN, DEV, PA")]
        public async Task<IActionResult> PesquisarTodosPerfis()
        {
            List<PerfilViewDto> viewDto = await _roleManager.Roles.Select(item => new PerfilViewDto
            {
                Id = item.Id,
                Nome = item.Name,
            }).ToListAsync();

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
    }
}
