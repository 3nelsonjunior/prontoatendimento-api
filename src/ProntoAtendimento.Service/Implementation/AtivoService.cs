using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProntoAtendimento.Domain.Dto.AtivoDto;
using ProntoAtendimento.Domain.Dto.AtivoDto.Validation;
using ProntoAtendimento.Domain.Entity;
using ProntoAtendimento.Repository.Interface;
using ProntoAtendimento.Service.Interface;

namespace ProntoAtendimento.Service.Implementation
{
    public class AtivoService : BaseService, IAtivoService
    {
        private readonly IAtivoRepository _ativoRepository;
        private readonly IOcorrenciaAtivoRepository _ocorrenciaAtivoRepository;
        private readonly ISetorRepository _setorRepository;

        public AtivoService(IAtivoRepository ativoRepository,
                            IOcorrenciaAtivoRepository ocorrenciaAtivoRepository,
                            ISetorRepository setorRepository,
                            INotificador notificador) : base(notificador)
        {
            _ativoRepository = ativoRepository;
            _ocorrenciaAtivoRepository = ocorrenciaAtivoRepository;
            _setorRepository = setorRepository;
        }

        public async Task<bool> CadastrarAtivoAsync(AtivoPostDto ativoPostDto)
        {
            // validação domínio
            if (!ExecutarValidacao(new AtivoPostDtoValidation(), ativoPostDto)) return false;

            // validação regra de negócio
            if (!await EhSetorValido(ativoPostDto.SetorId)) return false;

            Ativo ativo = new Ativo(ativoPostDto);

            return await _ativoRepository.CadastrarAsync(ativo);
        }

        public async Task<bool> EditarAtivoAsync(AtivoPutDto ativoPutDto, AtivoViewDto viewDto)
        {
            // validação domínio
            if (!ExecutarValidacao(new AtivoPutDtoValidation(), ativoPutDto)) return false;

            // validação regra de negócio
            if (!await EhSetorValido(ativoPutDto.SetorId)) return false;

            Ativo ativo = new Ativo(ativoPutDto, viewDto);

            await _ativoRepository.EditarAsync(ativo);

            return true;
        }

        public async Task<bool> ExcluirAtivoAsync(Guid ativoId)
        {
            // validar se ativo não está vinculado a nenhuma ocorrencia
            ICollection<OcorrenciaAtivo> listaOA = await _ocorrenciaAtivoRepository.PesquisarOcorrenciaAtivoPorAtivo(ativoId);
            if(listaOA.Count > 0 && !listaOA.Contains(null))
            {
                Notificar("Ativo não pode ser excluído, pois possui Ocorrências vinculadas!");
                return false;
            }

            return await _ativoRepository.ExcluirAsync(ativoId);
        }

        public async Task<AtivoViewDto> PesquisarAtivoPorIdAsync(Guid ativoId)
        {
            if (ativoId.Equals(null))
            {
                Notificar("O id informado é inválido!");
                return null;
            }

            return await _ativoRepository.PesquisarAtivoPorIdAsync(ativoId);
        }

        public async Task<AtivoViewDto> PesquisarAtivoPorIncAsync(int incAtivo)
        {
            return await _ativoRepository.PesquisarAtivoPorIncAsync(incAtivo);
        }

        public async Task<AtivoPaginacaoViewDto> PesquisarAtivosPorFiltrosPaginacaoAsync(AtivoFiltroDto filtroDto)
        {
            if (filtroDto.Equals(null))
            {
                Notificar("O id informado é inválido!");
                return null;
            }

            return await _ativoRepository.PesquisarAtivosPorFiltrosPaginacaoAsync(filtroDto); 
        }

        public async Task<List<AtivoViewDto>> PesquisarAtivosPorSetorAsync(Guid setorId)
        {
            if (!await _setorRepository.ExisteAsync(setorId))
            {
                Notificar("Nenhum setor foi encontrado com Id informado!");
                return null;
            }

            return await _ativoRepository.PesquisarAtivosPorSetorAsync(setorId);
        }

        public void Dispose()
        {
            _ativoRepository?.Dispose();
        }

        // serviços
        private async Task<bool> EhSetorValido(Guid setorId)
        {
            if (!await _setorRepository.ExisteAsync(setorId))
            {
                Notificar("O setor informado não foi encontrado!");
                return false;
            }

            return true;
        }
    }
}
