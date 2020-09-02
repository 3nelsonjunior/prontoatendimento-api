using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProntoAtendimento.Domain.Dto.OcorrenciaDto;
using ProntoAtendimento.Domain.Dto.TramiteDto;
using ProntoAtendimento.Domain.Dto.TurnoDto;
using ProntoAtendimento.Domain.Dto.TurnoDto.Validation;
using ProntoAtendimento.Domain.Dto.UsuarioDto;
using ProntoAtendimento.Domain.Entity;
using ProntoAtendimento.Domain.Enums;

using ProntoAtendimento.Repository.Interface;
using ProntoAtendimento.Service.Interface;

namespace ProntoAtendimento.Service.Implementation
{
    public class TurnoService : BaseService, ITurnoService
    {
        private readonly IOcorrenciaAtivoRepository _ocorrenciaAtivoRepository;
        private readonly IOcorrenciaRepository _ocorrenciaRepository;
        private readonly ITramiteRepository _tramiteRepository;
        private readonly ITurnoOcorrenciaRepository _turnoOcorrenciaRepository;
        private readonly ITurnoRepository _turnoRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public TurnoService(IOcorrenciaAtivoRepository ocorrenciaAtivoRepository,
                            IOcorrenciaRepository ocorrenciaRepository,
                            ITramiteRepository tramiteRepository,
                            ITurnoOcorrenciaRepository turnoOcorrenciaRepository,
                            ITurnoRepository turnoRepository,
                            IUsuarioRepository usuarioRepository,
                            INotificador notificador) : base(notificador)
        {
            _ocorrenciaAtivoRepository = ocorrenciaAtivoRepository;
            _ocorrenciaRepository = ocorrenciaRepository;
            _tramiteRepository = tramiteRepository;
            _turnoOcorrenciaRepository = turnoOcorrenciaRepository;
            _turnoRepository = turnoRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<bool> AbrirTurno(TurnoPostDto turnoPostDto)
        {
            // validação domínio
            if (!ExecutarValidacao(new TurnoPostDtoValidation(), turnoPostDto)) return false;

            // validação regra de negócio
            if(await ExisteTurnoEmaberto()) return false;

            if(!await EhDataHoraValida(AcaoEnum.CADASTRAR, turnoPostDto.DataHoraInicio, turnoPostDto.DataHoraFim, turnoPostDto.Id)) return false;

            if (!await EhUsuarioValido(turnoPostDto.UsuarioId)) return false;

            Turno turno = new Turno(turnoPostDto);

            // abrir turno
            Guid turnoId = await _turnoRepository.AbrirTurno(turno);

            // vincular ocorrencias abertas a TurnoOcorrencias
            if (!turnoId.Equals(null))
            {
                await CadastrarTurnoOcorrencia(turnoId);
                return true;
            }

            return false;
        }

        public async Task<bool> ReabrirTurno(Guid turnoId)
        {
            if (await ExisteTurnoEmaberto()) return false;

            if (!await ValidarPrazoMaximoReabrirTurno(turnoId)) return false;

            return await _turnoRepository.ReabrirTurno(turnoId);
        }

        public async Task<bool> FecharTurno(Guid turnoId)
        {
            return await _turnoRepository.FecharTurno(turnoId);
        }

        public async Task<bool> EditarTurno(TurnoPutDto turnoPutDto, TurnoResultDto turnoResultDto)
        {
            // validação domínio
            if (!ExecutarValidacao(new TurnoPutDtoValidation(), turnoPutDto)) return false;


            // validação regra de negócio
            // não precisa validar status, pois há um endpoint especifico para isso
            if (!await EhDataHoraValida(AcaoEnum.EDITAR, turnoPutDto.DataHoraInicio, turnoPutDto.DataHoraFim, turnoPutDto.Id)) return false;

            if (!await EhUsuarioValido(turnoPutDto.UsuarioId)) return false;

            Turno turno = new Turno(turnoPutDto, turnoResultDto);
            
            return await _turnoRepository.EditarTurno(turno);
        }

        public async Task<bool> ExcluirTurno(Guid turnoId)
        {
            // verificar se turno está fechado
            if (!await EhStatusTurnoAberto(turnoId)) return false;

            return await _turnoRepository.ExcluirTurno(turnoId);

        }

        public async Task<TurnoResultDto> PesquisarTurnoPorId(Guid turnoId)
        {
            if (turnoId.Equals(null))
            {
                Notificar("O id informado é inválido!");
                return null;
            }

            return await _turnoRepository.PesquisarTurnoPorId(turnoId);
        }

        public async Task<TurnoResultDto> PesquisarTurnoPorInc(int incTurno)
        {
            return await _turnoRepository.PesquisarTurnoPorInc(incTurno);
        }

        public async Task<ICollection<TurnoResultDto>> PesquisarTurnosPorUsuario(Guid usuarioId)
        {
            if (!await EhUsuarioValido(usuarioId)) return null;

            return await _turnoRepository.PesquisarTurnosPorUsuario(usuarioId);   
        }

        public async Task<TurnoPaginacaoViewtDto> PesquisarTurnosPorFiltrosPaginacaoAsync(TurnoFiltroDto filtroDto)
        {
            if (filtroDto == null)
            {
                Notificar("O filtro informado é inválido!");
                return null;
            }

            return await _turnoRepository.PesquisarTurnosPorFiltrosPaginacaoAsync(filtroDto);
        }

        public void Dispose()
        {
            _turnoRepository?.Dispose();
        }

        // serviços
        private async Task<bool> EhUsuarioValido(Guid usuarioId)
        {
            if (usuarioId.Equals(null))
            {
                Notificar("O id informado é inválido!");
                return false;
            }

            UsuarioViewDto viewDto = await _usuarioRepository.PesquisarUsuarioPorIdAsync(usuarioId);
            if (viewDto.Equals(null))
            {
                Notificar("Nenhum usuário foi encontrado com Id informado!");
                return false;
            }

            return true;
        }

        private async Task<bool> ExisteTurnoEmaberto()
        {
            if (await _turnoRepository.PesquisarExisteTurnoAberto())
            {
                Notificar("Existe um turno com status de aberto!");
                return true;
            }

            return false;
        }

        private async Task<bool> EhDataHoraValida(AcaoEnum acaoEnum, DateTime dataHoraInicio, DateTime dataHoraFim, Guid turnoId)
        {
            if (acaoEnum.Equals(AcaoEnum.CADASTRAR))
            {

                bool result = await _turnoRepository.PesquisarExisteTurnoDataHoraMaisRecente(dataHoraInicio);
                if (result)
                {
                    Notificar("A data/hora não é a mais recente!");
                    return false;
                }
            }

            TimeSpan diferencaDataHora = dataHoraFim.Subtract(dataHoraInicio);
            if (diferencaDataHora.Hours > 12 || diferencaDataHora.Days > 0)
            {
                Notificar("Não é possivel abrir um turno com mais de 12 horas!");
                return false;
            }

            if (await _turnoRepository.PesquisarExisteTurnoDataHoraRepetido(dataHoraInicio, dataHoraFim, turnoId))
            {
                Notificar("Já exite um turno com a mesma data/hora Início e data/hora Fim!");
                return false;
            }

            return true;
        }

        private async Task<bool> CadastrarTurnoOcorrencia(Guid turnoId)
        {
            ICollection<OcorrenciaResultDto> listaOcorrenciasEmAberto = await _ocorrenciaRepository.PesquisarOcorrenciasEmAndamento();

            if (!listaOcorrenciasEmAberto.Equals(null))
            {
                if (listaOcorrenciasEmAberto.Count > 0 && !listaOcorrenciasEmAberto.Contains(null))
                {
                    return await _turnoOcorrenciaRepository.AdicionarOcorrenciasEmAbertoAoTurno(turnoId, listaOcorrenciasEmAberto);
                }
            }
            return true;
        }

        private async Task<bool> ValidarPrazoMaximoReabrirTurno(Guid turnoId)
        {
            TurnoResultDto turnoResultDto = await _turnoRepository.PesquisarTurnoPorId(turnoId);
            
            DateTime DataHoraAtual = DateTime.Now;
            
            TimeSpan diferencaDataHora = DataHoraAtual.Subtract(DateTime.Parse(turnoResultDto.DataHoraInicio));
            
            if (diferencaDataHora.Days > 1)
            {
                Notificar("Não é possivel abrir um turno fechado a mais de 24 horas!");
                return false;
            }

            return true;
        }

        private async Task<bool> EhStatusTurnoAberto(Guid turnoId)
        {
            TurnoResultDto turnoResultDto = await _turnoRepository.PesquisarTurnoPorId(turnoId);

            if (Enum.Parse<StatusTurnoEnum>(turnoResultDto.StatusTurno) == StatusTurnoEnum.FECHADO)
            {
                Notificar("Não é permitido excluir um turno com status FECHADO!");
                return false;
            }

            return true;
        }

        

        

    }
}
