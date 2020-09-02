using Microsoft.AspNetCore.Identity;
using ProntoAtendimento.Domain.Dto.UsuarioDto;
using ProntoAtendimento.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProntoAtendimento.Repository.Interface
{
    public interface IUsuarioRepository 
    {
        Task<IdentityResult> CadastrarUsuarioAsync(Usuario usuario, string senha);
        Task<IdentityResult> EditarUsuarioAsync(Usuario usuario);
        Task<IdentityResult> AlterarSenhaUsuarioAsync(Usuario usuario, string senha);
        Task<IdentityResult> ExcluirUsuarioAsync(Guid usuarioId);
        Task<UsuarioViewDto> PesquisarUsuarioPorIdAsync(Guid usuarioId);
        Task<UsuarioViewDto> PesquisarUsuarioPorMatriculaAsync(string matricula);
        Task<List<UsuarioTabelaViewDto>> PesquisarTodosUsuariosAsync();
        Task<List<UsuarioPerfilPaSelectboxViewDto>> PesquisarUsuariosPerfilPaParaSelectboxAsync();
        Task<UsuarioPaginacaoViewtDto> PesquisarUsuariosPorFiltrosPaginacaoAsync(UsuarioFiltroDto usuarioFiltroDto);
        Task<UsuarioPerfilPaginacaoViewDto> PesquisarUsuariosPorFiltrosPaginacaoParaPaginaPerfilAsync(UsuarioPerfilFiltroDto filtroDto);
        void Dispose();

    }
}
