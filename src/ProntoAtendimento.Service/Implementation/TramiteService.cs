using ProntoAtendimento.Domain.Dto.OcorrenciaDto;
using ProntoAtendimento.Domain.Dto.TramiteDto;
using ProntoAtendimento.Domain.Dto.TramiteDto.Validation;
using ProntoAtendimento.Domain.Dto.UsuarioDto;
using ProntoAtendimento.Domain.Entity;
using ProntoAtendimento.Repository.Interface;
using ProntoAtendimento.Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProntoAtendimento.Service.Implementation
{
    public class TramiteService : BaseService, ITramiteService
    {
        private readonly IOcorrenciaRepository _ocorrenciaRepository;
        private readonly ITramiteRepository _tramiteRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public TramiteService(IOcorrenciaRepository ocorrenciaRepository,
                              ITramiteRepository tramiteRepository,
                              IUsuarioRepository usuarioRepository,
                              INotificador notificador) : base(notificador)
        {
            _ocorrenciaRepository = ocorrenciaRepository;
            _tramiteRepository = tramiteRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<bool> CadastrarTramite(TramitePostDto tramitePostDto)
        {
            // validação domínio
            if (!ExecutarValidacao(new TramitePostDtoValidation(), tramitePostDto)) return false;

            // validação regra de negócio
            if (!await ValidarUsuarioEOcorrencia(tramitePostDto.UsuarioId, tramitePostDto.OcorrenciaId, tramitePostDto.DataHora)) return false;

            Tramite tramite = new Tramite(tramitePostDto);

            return await _tramiteRepository.CadastrarAsync(tramite);
        }

        public async Task<bool> EditarTramite(TramitePutDto tramitePutDto, TramiteResultDto tramiteResultDto)
        {
            // validação domínio
            if (!ExecutarValidacao(new TramitePutDtoValidation(), tramitePutDto)) return false;

            // validação regra de negócio
            if (!await ValidarUsuarioEOcorrencia(tramitePutDto.UsuarioId, tramitePutDto.OcorrenciaId, tramitePutDto.DataHora)) return false;

            // Verificar se já existe um trâmite solução
            ICollection<TramiteResultDto> listaTramiteResultDto = await _tramiteRepository.PesquisarTramitesPorOcorrencia(tramitePutDto.OcorrenciaId);
            foreach (TramiteResultDto itemTra in listaTramiteResultDto)
            {
                if (bool.Parse(itemTra.Solucao))
                {
                    Notificar("Já existe um trâmite definido como solução!");
                    return false;
                }
            }

            Tramite tramite = new Tramite(tramitePutDto, tramiteResultDto);

            await _tramiteRepository.EditarAsync(tramite);

            return true;
        }

        public async Task<bool> ExcluirTramite(Guid tramiteId)
        {
            if (tramiteId.Equals(null))
            {
                Notificar("O id informado é inválido!");
                return false;
            }

            return await _tramiteRepository.ExcluirAsync(tramiteId);
        }

        public async Task<TramiteResultDto> PesquisarTramitePorId(Guid tramiteId)
        {
            if (tramiteId.Equals(null))
            {
                Notificar("O id informado é inválido!");
                return null;
            }

            return await _tramiteRepository.PesquisarTramitePorId(tramiteId);
        }

        public async Task<TramiteResultDto> PesquisarTramitePorInc(int incTramite)
        {
            if (incTramite.Equals(null))
            {
                Notificar("O inc informado é inválido!");
                return null;
            }

            return await _tramiteRepository.PesquisarTramitePorInc(incTramite);
        }

        public async Task<ICollection<TramiteResultDto>> PesquisarTramitesPorFiltros(TramiteFiltroDto tramiteFiltroDto)
        {
            if (tramiteFiltroDto.Equals(null))
            {
                Notificar("O filtro informado é inválido!");
                return null;
            }

            return await _tramiteRepository.PesquisarTramitesPorFiltros(tramiteFiltroDto);
        }

        public async Task<ICollection<TramiteResultDto>> PesquisarTramitePorOcorrencia(Guid ocorrenciaId)
        {
            OcorrenciaResultDto ocorrenciaResultDto = await _ocorrenciaRepository.PesquisarOcorrenciaPorId(ocorrenciaId);
            if (ocorrenciaResultDto == null)
            {
                Notificar("Nenhuma ocorrência foi encontrado com Id informado!");
                return null;
            }

            return await _tramiteRepository.PesquisarTramitesPorOcorrencia(ocorrenciaId);
        }

        public async Task<ICollection<TramiteResultDto>> PesquisarTramitePorUsuario(Guid usuarioId)
        {
            UsuarioViewDto viewDto = await _usuarioRepository.PesquisarUsuarioPorIdAsync(usuarioId);
            if (viewDto == null)
            {
                Notificar("Nenhum usuário foi encontrado com Id informado!");
                return null;
            }

            return await _tramiteRepository.PesquisarTramitesPorUsuario(usuarioId);
        }

        public void Dispose()
        {
            _tramiteRepository?.Dispose();
        }


        // serviços
        private async Task<bool> ValidarUsuarioEOcorrencia(Guid usuarioId, Guid ocorrenciaId, DateTime dataHoraTramite)
        {
            UsuarioViewDto viewDto = await _usuarioRepository.PesquisarUsuarioPorIdAsync(usuarioId);
            if (viewDto == null)
            {
                Notificar("Usuário informado não foi encontrado!");
                return false;
            }

            OcorrenciaResultDto ocorrenciaResultDto = await _ocorrenciaRepository.PesquisarOcorrenciaPorId(ocorrenciaId);
            if (ocorrenciaResultDto.Equals(null))
            {
                Notificar("Ocorrência informada não foi encontrada!");
                return false;
            }

            if (dataHoraTramite < DateTime.Parse(ocorrenciaResultDto.DataHoraInicio))
            {
                Notificar("Data/Hota do trâmite informada é menor que Data/Hora inicio da ocorrência!");
                return false;
            }
            if (!DateTime.Parse(ocorrenciaResultDto.DataHoraFim).Equals(null))
            {
                if (dataHoraTramite > DateTime.Parse(ocorrenciaResultDto.DataHoraFim))
                {
                    Notificar("Data/Hota do trâmite informada é maior que Data/Hora fim da ocorrência!");
                    return false;
                }
            }

            return true;
        }

    }
}
