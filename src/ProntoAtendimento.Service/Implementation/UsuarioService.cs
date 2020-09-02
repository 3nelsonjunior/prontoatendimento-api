using Microsoft.AspNetCore.Identity;
using ProntoAtendimento.Domain.Dto.UsuarioDto;
using ProntoAtendimento.Domain.Dto.UsuarioDto.Validation;
using ProntoAtendimento.Domain.Entity;
using ProntoAtendimento.Domain.Enums;
using ProntoAtendimento.Repository.Interface;
using ProntoAtendimento.Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProntoAtendimento.Service.Implementation
{
    public class UsuarioService : BaseService, IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IOcorrenciaRepository _ocorrenciaRepository;
        private readonly ITramiteRepository _tramiteRepository;
        private readonly ITurnoRepository _turnoRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository,
                              IOcorrenciaRepository ocorrenciaRepository,
                              ITramiteRepository tramiteRepository,
                              ITurnoRepository turnoRepository,
                              INotificador notificador) : base(notificador)
        {
            _usuarioRepository = usuarioRepository;
            _ocorrenciaRepository = ocorrenciaRepository;
            _tramiteRepository = tramiteRepository;
            _turnoRepository = turnoRepository;
        }

        public async Task<IdentityResult> CadastrarUsuarioAsync(UsuarioPostDto postDto)
        {
            // validação domínio
            if (!ExecutarValidacao(new UsuarioPostDtoValidation(), postDto)) return null;

            Usuario usuario = new Usuario(postDto);

            return  await _usuarioRepository.CadastrarUsuarioAsync(usuario, postDto.Senha);
        }

        public async Task<IdentityResult> EditarUsuarioAsync(UsuarioPutDto putDto, UsuarioViewDto viewDto)
        {
            // validação domínio
            if (!ExecutarValidacao(new UsuarioPutDtoValidation(), putDto)) return null;


            Usuario usuario = new Usuario(putDto, viewDto);

            return await _usuarioRepository.EditarUsuarioAsync(usuario);

        }

        public async Task<IdentityResult> ExcluirUsuarioAsync(Guid usuarioId)
        {
            // validação regra de negócio
            if (!await VerificarSeHaDadosVinculadosAoUsuario(usuarioId)) return null;

            return await _usuarioRepository.ExcluirUsuarioAsync(usuarioId);
        }

        public async Task<UsuarioViewDto> PesquisarUsuarioPorIdAsync(Guid usuarioId)
        {

            return await _usuarioRepository.PesquisarUsuarioPorIdAsync(usuarioId);
        }

        public async Task<UsuarioViewDto> PesquisarUsuarioPorMatriculaAsync(string matricula)
        {
            if (!EhMatriculaValida(matricula)) return null;

            return await _usuarioRepository.PesquisarUsuarioPorMatriculaAsync(matricula);
        }

        public async Task<List<UsuarioTabelaViewDto>> PesquisarTodosUsuariosAsync()
        {
            return await _usuarioRepository.PesquisarTodosUsuariosAsync();
        }

        public async Task<List<UsuarioPerfilPaSelectboxViewDto>> PesquisarUsuariosPerfilPaParaSelectboxAsync()
        {
            return await _usuarioRepository.PesquisarUsuariosPerfilPaParaSelectboxAsync();
        }

        public async Task<UsuarioPaginacaoViewtDto> PesquisarUsuariosPorFiltrosPaginacaoAsync(UsuarioFiltroDto filtroDto)
        {
            if (filtroDto == null)
            {
                Notificar("O filtro informado é inválido!");
                return null;
            }

            return await _usuarioRepository.PesquisarUsuariosPorFiltrosPaginacaoAsync(filtroDto);
        }

        public async Task<UsuarioPerfilPaginacaoViewDto> PesquisarUsuariosPorFiltrosPaginacaoParaPaginaPerfilAsync(UsuarioPerfilFiltroDto filtroDto)
        {
            if (filtroDto == null)
            {
                Notificar("O filtro informado é inválido!");
                return null;
            }

            return await _usuarioRepository.PesquisarUsuariosPorFiltrosPaginacaoParaPaginaPerfilAsync(filtroDto);
        }

        public void Dispose()
        {
            _usuarioRepository.Dispose();
        }

        // serviços -------------------------------------------------------------------------------

        private async Task<bool> VerificarSeHaDadosVinculadosAoUsuario(Guid usuarioId)
        {
            var listaTurnos = await _turnoRepository.PesquisarTurnosPorUsuario(usuarioId);
            if (listaTurnos != null)
            {
                if (listaTurnos.Count > 0 && !listaTurnos.Contains(null))
                {
                    Notificar("Usuário não pode ser excluído, pois possui Turnos vinculados a sua matricula, considere desativá-lo!");
                    return false;
                }
            }

            var listaOcorrencias = await _ocorrenciaRepository.PesquisarOcorrenciasPorUsuario(usuarioId);
            if (listaOcorrencias != null)
            {
                if (listaOcorrencias.Count > 0 && !listaOcorrencias.Contains(null))
                {
                    Notificar("Usuário não pode ser excluído, pois possui Ocorrências vinculadas a sua matricula, considere desativá-lo!");
                    return false;
                }
            }

            var listaTramites = await _tramiteRepository.PesquisarTramitesPorUsuario(usuarioId);
            if (listaTramites != null)
            {
                if (listaTramites.Count > 0 && !listaTramites.Contains(null))
                {
                    Notificar("Usuário não pode ser excluído, pois possui Tramites vinculadas a sua matricula, considere desativá-lo!");
                    return false;
                }
            }

            return true;
        }

        private bool EhMatriculaValida(string matricula)
        {
            if (string.IsNullOrEmpty(matricula) || matricula.Length != 8)
            {
                Notificar("A matrícula informada é inválida!");
                return false;
            }

            return true;
        }
        

    }
}
