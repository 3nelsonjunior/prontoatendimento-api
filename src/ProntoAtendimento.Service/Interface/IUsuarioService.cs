using Microsoft.AspNetCore.Identity;
using ProntoAtendimento.Domain.Dto.UsuarioDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProntoAtendimento.Service.Interface
{
    public interface IUsuarioService
    {
        Task<IdentityResult> CadastrarUsuarioAsync(UsuarioPostDto postDto);
        Task<IdentityResult> EditarUsuarioAsync(UsuarioPutDto putDto, UsuarioViewDto viewDto);
        Task<IdentityResult> ExcluirUsuarioAsync(Guid usuarioId);
        Task<UsuarioViewDto> PesquisarUsuarioPorIdAsync(Guid usuarioId);
        Task<UsuarioViewDto> PesquisarUsuarioPorMatriculaAsync(string matricula);
        Task<List<UsuarioTabelaViewDto>> PesquisarTodosUsuariosAsync();
        Task<List<UsuarioPerfilPaSelectboxViewDto>> PesquisarUsuariosPerfilPaParaSelectboxAsync();
        Task<UsuarioPaginacaoViewtDto> PesquisarUsuariosPorFiltrosPaginacaoAsync(UsuarioFiltroDto filtroDto);
        Task<UsuarioPerfilPaginacaoViewDto> PesquisarUsuariosPorFiltrosPaginacaoParaPaginaPerfilAsync(UsuarioPerfilFiltroDto filtroDto);
    }
}
