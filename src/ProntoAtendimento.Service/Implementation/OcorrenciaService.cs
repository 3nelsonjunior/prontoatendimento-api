using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProntoAtendimento.Domain.Dto.AtivoDto;
using ProntoAtendimento.Domain.Dto.OcorrenciaDto;
using ProntoAtendimento.Domain.Dto.OcorrenciaDto.Validation;
using ProntoAtendimento.Domain.Dto.TramiteDto;
using ProntoAtendimento.Domain.Dto.TurnoDto;
using ProntoAtendimento.Domain.Dto.UsuarioDto;
using ProntoAtendimento.Domain.Entity;
using ProntoAtendimento.Domain.Enums;
using ProntoAtendimento.Repository.Interface;
using ProntoAtendimento.Service.Interface;

namespace ProntoAtendimento.Service.Implementation
{
    public class OcorrenciaService : BaseService, IOcorrenciaService
    {
        private readonly IAtivoRepository _ativoRepository;
        private readonly IOcorrenciaAtivoRepository _ocorrenciaAtivoRepository;
        private readonly IOcorrenciaRepository _ocorrenciaRepository;
        private readonly ITramiteRepository _tramiteRepository;
        private readonly ITurnoOcorrenciaRepository _turnoOcorrenciaRepository;
        private readonly ITurnoRepository _turnoRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public OcorrenciaService(IAtivoRepository ativoRepository,
                                 IOcorrenciaAtivoRepository ocorrenciaAtivoRepository,
                                 IOcorrenciaRepository ocorrenciaRepository,
                                 ITramiteRepository tramiteRepository,
                                 ITurnoOcorrenciaRepository turnoOcorrenciaRepository,
                                 ITurnoRepository turnoRepository,
                                 IUsuarioRepository usuarioRepository,
                                 INotificador notificador) : base(notificador)
        {
            _ativoRepository = ativoRepository;
            _ocorrenciaAtivoRepository = ocorrenciaAtivoRepository;
            _ocorrenciaRepository = ocorrenciaRepository;
            _tramiteRepository = tramiteRepository;
            _turnoOcorrenciaRepository = turnoOcorrenciaRepository;
            _turnoRepository = turnoRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<bool> AbrirOcorrencia(OcorrenciaPostDto ocorrenciaPostDto)
        {
            // validação domínio
            if (!ExecutarValidacao(new OcorrenciaPostDtoValidation(), ocorrenciaPostDto)) return false;

            // validação regra de negócio
            if (!await EhDataHoraValida(ocorrenciaPostDto.TurnoId, ocorrenciaPostDto.DataHoraInicio, ocorrenciaPostDto.DataHoraFim)) return false;
            
            if (!await EhTurnoValido(ocorrenciaPostDto.TurnoId)) return false;

            if (!await EhUsuarioValido(ocorrenciaPostDto.UsuarioId)) return false;

            if (!await EhListaAtivoValida(ocorrenciaPostDto.Ativos)) return false;

            Ocorrencia ocorrencia = new Ocorrencia(ocorrenciaPostDto);

            OcorrenciaResultDto ocorrenciaResultDto = await _ocorrenciaRepository.AbrirOcorrencia(ocorrencia);

            await CadastrarTurnoOcorrencia(ocorrenciaPostDto.TurnoId, ocorrenciaResultDto);

            await CadastrarOcorrenciaAtivo(Guid.Parse(ocorrenciaResultDto.Id), ocorrenciaPostDto.Ativos);

            return true;
        }

        public async Task<bool> EditarOcorrencia(OcorrenciaPutDto ocorrenciaPutDto, OcorrenciaResultDto ocorrenciaResultDto)
        {
            // validação domínio
            if (!ExecutarValidacao(new OcorrenciaPutDtoValidation(), ocorrenciaPutDto)) return false;

            // validação regra de negócio
            if (!await EhDataHoraValida(ocorrenciaPutDto.TurnoId, ocorrenciaPutDto.DataHoraInicio, ocorrenciaPutDto.DataHoraFim)) return false;

            if (!await EhTurnoValido(ocorrenciaPutDto.TurnoId)) return false;

            if (!await EhUsuarioValido(ocorrenciaPutDto.UsuarioId)) return false;

            if (!await EhListaAtivoValida(ocorrenciaPutDto.Ativos)) return false;

            Ocorrencia ocorrencia = new Ocorrencia(ocorrenciaPutDto, ocorrenciaResultDto);

            await _ocorrenciaRepository.EditarAsync(ocorrencia);

            await EditarTurnoOcorrencia(ocorrenciaPutDto, Guid.Parse(ocorrenciaResultDto.Id));

            await ExcluirOcorrenciaAtivo(ocorrenciaPutDto.Id);

            await CadastrarOcorrenciaAtivo(ocorrenciaPutDto.Id, ocorrenciaPutDto.Ativos);

            return true;
        }

        public async Task<bool> ExcluirOcorrencia(Guid ocorrenciaId)
        {
            if (ocorrenciaId.Equals(null))
            {
                Notificar("O id informado é inválido!");
                return false;
            }

            return await _ocorrenciaRepository.ExcluirOcorrencia(ocorrenciaId);
        }

        public async Task<OcorrenciaResultDto> PesquisarOcorrenciaPorId(Guid ocorrenciaId)
        {
            if(ocorrenciaId.Equals(null))
            {
                Notificar("O id informado é inválido!");
                return null;
            }

            return await _ocorrenciaRepository.PesquisarOcorrenciaPorId(ocorrenciaId);
        }

        public async Task<OcorrenciaResultDto> PesquisarOcorrenciaPorInc(int incOcorrencia)
        {
            if (incOcorrencia.Equals(null))
            {
                Notificar("O inc informado é inválido!");
                return null;
            }

            return await _ocorrenciaRepository.PesquisarOcorrenciaPorInc(incOcorrencia);
        }

        public async Task<ICollection<OcorrenciaResultDto>> PesquisarOcorrenciasPorFiltros(OcorrenciaFiltroDto ocorrenciaFiltroDto)
        {
            if (ocorrenciaFiltroDto == null)
            {
                Notificar("O id informado é inválido!");
                return null;
            }

            return await _ocorrenciaRepository.PesquisarOcorrenciasPorFiltros(ocorrenciaFiltroDto);
        }

        public async Task<ICollection<OcorrenciaResultDto>> PesquisarOcorrenciasPorTurno(Guid turnoId)
        {
            if (turnoId.Equals(null))
            {
                Notificar("O id informado é inválido!");
                return null;
            }

            return await _ocorrenciaRepository.PesquisarOcorrenciasPorTurno(turnoId);

        }

        public async Task<ICollection<OcorrenciaResultDto>> PesquisarOcorrenciasEmAndamento()
        {
            return await _ocorrenciaRepository.PesquisarOcorrenciasEmAndamento();
        }

        public async Task<ICollection<OcorrenciaResultDto>> PesquisarOcorrenciasPorUsuario(Guid usuarioId)
        {
            if (usuarioId.Equals(null))
            {
                Notificar("O id informado é inválido!");
                return null;
            }

            return await _ocorrenciaRepository.PesquisarOcorrenciasPorUsuario(usuarioId);
        }

        public void Dispose()
        {
            _ocorrenciaRepository?.Dispose();
        }

        // serviços
        private async Task<bool> EhUsuarioValido(Guid usuarioId)
        {
            UsuarioViewDto viewDto = await _usuarioRepository.PesquisarUsuarioPorIdAsync(usuarioId);
            if (viewDto == null)
            {
                Notificar("Nenhum usuário foi encontrado com Id informado!");
                return false;
            }

            return true;
        }

        private async Task<bool> EhDataHoraValida(Guid turnoId, DateTime dataHoraInicioOcorrrencia, DateTime? dataHoraFimOcorrrencia)
        {
            TurnoResultDto turnoResultDto = await _turnoRepository.PesquisarTurnoPorId(turnoId);

            if (turnoResultDto != null)
            {
                if (dataHoraInicioOcorrrencia < DateTime.Parse(turnoResultDto.DataHoraInicio))
                {
                    Notificar("A Data/Hora inicio da ocorrência não pode ser menor que Data/Hora inicio do turno!");
                    return false;
                }

                if (dataHoraInicioOcorrrencia > DateTime.Parse(turnoResultDto.DataHoraFim))
                {
                    Notificar("A Data/Hora inicio da ocorrência não pode ser maior que Data/Hora fim do turno!");
                    return false;
                }

                if (!dataHoraFimOcorrrencia.Equals(null))
                {
                    if (dataHoraFimOcorrrencia < DateTime.Parse(turnoResultDto.DataHoraInicio))
                    {
                        Notificar("A Data/Hora fim da ocorrência não pode ser menor que Data/Hora inicio do turno!");
                        return false;
                    }


                    if (dataHoraFimOcorrrencia > DateTime.Parse(turnoResultDto.DataHoraFim))
                    {
                        Notificar("A Data/Hora fim da ocorrência não pode ser maior que Data/Hora fim do turno!");
                        return false;
                    }
                }
            }

            return true;
        }

        private async Task<bool> EhTurnoValido(Guid turnoId)
        {
            TurnoResultDto turnoResultDto = await _turnoRepository.PesquisarTurnoPorId(turnoId);
            if (turnoResultDto == null)
            {
                Notificar("Nenhum turno foi encontrado com Id informado!");
                return false;
            }

            return true;
        }

        private async Task<bool> EhListaAtivoValida(ICollection<AtivoOcorrenciaPostDto> listaAtivosDto)
        {


            int qtdAtivoPrincipal = 0;

            foreach (AtivoOcorrenciaPostDto itemAtivoDto in listaAtivosDto)
            {
                AtivoViewDto viewDto = await _ativoRepository.PesquisarAtivoPorIdAsync(itemAtivoDto.Id);
                
                if (viewDto == null)
                {
                    Notificar("Existe um ativo na lista com Id inexistente!");
                    return false;
                }

                if (itemAtivoDto.Principal.Equals(true)) qtdAtivoPrincipal++;

                if (itemAtivoDto.Principal.Equals(true))
                {
                    // verificar se há ocorrencia em aberto com este ativo
                    ICollection<OcorrenciaAtivo> listaOcorrenciaAtivo = await _ocorrenciaAtivoRepository.PesquisarOcorrenciaAtivoPorAtivo(itemAtivoDto.Id);

                    if (listaOcorrenciaAtivo.Count >= 1 && !listaOcorrenciaAtivo.Contains(null))
                    {
                        Notificar("Existe uma ocorrência com mesmo ativo principal!");
                        return false;
                    }
                }
            }

            if(qtdAtivoPrincipal == 0)
            {
                Notificar("Não existe ativo principal na ocorrência!");
                return false;
            }

            if (qtdAtivoPrincipal > 1)
            {
                Notificar("Existem mais de um ativo principal na lista!");
                return false;
            }

            


            return true;
        }

        private async Task<bool> CadastrarTurnoOcorrencia(Guid turnoId, OcorrenciaResultDto ocorrenciaResultDto)
        {
            TurnoOcorrencia turnoOcorrencia = new TurnoOcorrencia(turnoId,
                                                                  Guid.Parse(ocorrenciaResultDto.Id),
                                                                  Enum.Parse<StatusOcorrenciaEnum>(ocorrenciaResultDto.StatusAtualOcorrencia));
            return await _turnoOcorrenciaRepository.CadastrarTurnoOcorrencia(turnoOcorrencia);
        }

        private async Task<bool> EditarTurnoOcorrencia(OcorrenciaPutDto ocorrenciaPutDto, Guid ocorrenciaId)
        {
            TurnoOcorrencia turnoOcorrencia = new TurnoOcorrencia(ocorrenciaPutDto.TurnoId,
                                                                 ocorrenciaId,
                                                                 ocorrenciaPutDto.StatusAtualOcorrencia);

            // atualizar TurnoOcorrencia caso altere Status ocorrencia

            return await _turnoOcorrenciaRepository.EditarStatusTurnoOcorrencia(turnoOcorrencia);
        }

        

        private async Task<bool> CadastrarOcorrenciaAtivo(Guid ocorrenciaId, ICollection<AtivoOcorrenciaPostDto> listaAtivosODTO)
        {
            foreach (AtivoOcorrenciaPostDto itemAtivoDto in listaAtivosODTO)
            {
                OcorrenciaAtivo ocorrenciaAtivo = new OcorrenciaAtivo(ocorrenciaId,
                                                                      itemAtivoDto.Id,
                                                                      itemAtivoDto.Principal);
                await _ocorrenciaAtivoRepository.CadastrarOcorrenciaAtivo(ocorrenciaAtivo);
            }
            return true;
        }

        private async Task<bool> ExcluirOcorrenciaAtivo(Guid ocorrenciaId)
        {
            ICollection<OcorrenciaAtivo> listaOcorrenciaAtivo = await _ocorrenciaAtivoRepository.PesquisarOcorrenciaAtivoPorOcorrencia(ocorrenciaId);

            // excluir todos registros de ativos desta ocorrencia
            foreach (OcorrenciaAtivo itemAtivoDto in listaOcorrenciaAtivo)
            {
                await _ocorrenciaAtivoRepository.ExcluirOcorrenciaAtivo(itemAtivoDto);
            }
            return true;
        }

        
    }
}
