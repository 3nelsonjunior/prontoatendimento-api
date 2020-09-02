using ProntoAtendimento.Domain.Dto.SetorDto;
using ProntoAtendimento.Domain.Dto.SetorDto.Validation;
using ProntoAtendimento.Domain.Entity;
using ProntoAtendimento.Repository.Interface;
using ProntoAtendimento.Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProntoAtendimento.Service.Implementation
{
    public class SetorService : BaseService, ISetorService
    {
        private readonly IAtivoRepository _ativoRepository;
        private readonly ISetorRepository _setorRepository;

        public SetorService(IAtivoRepository ativoRepository,
                            ISetorRepository setorRepository,
                              INotificador notificador) : base(notificador)
        {
            _ativoRepository = ativoRepository;
            _setorRepository = setorRepository;
        }

        public async Task<bool> CadastrarSetorAsync(SetorPostDto setorPostDto)
        {
            // validação domínio
            if (!ExecutarValidacao(new SetorPostDtoValidation(), setorPostDto)) return false;         

            Setor setor = new Setor(setorPostDto);

            return await _setorRepository.CadastrarAsync(setor);
        }

        public async Task<bool> EditarSetorAsync(SetorPutDto setorPutDto, SetorViewDto viewDto)
        {
            // validação domínio
            if (!ExecutarValidacao(new SetorPutDtoValidation(), setorPutDto)) return false;

            Setor setor = new Setor(setorPutDto, viewDto);

            await _setorRepository.EditarAsync(setor);

            return true;
        }

        public async Task<bool> ExcluirSetorAsync(Guid setorId)
        {
            // validação regra de negócio
            var listaAtivos = await _ativoRepository.PesquisarAtivosPorSetorAsync(setorId);
            if (listaAtivos != null)
            {
                if (listaAtivos.Count > 0 && !listaAtivos.Contains(null))
                {
                    Notificar("O setor não pode ser excluído, pois possui ativos vinculados!");
                    return false;
                }
            }

            return await _setorRepository.ExcluirAsync(setorId);
        }

        public async Task<SetorViewDto> PesquisarSetorPorIdAsync(Guid setorId)
        {
            if (setorId.Equals(null))
            {
                Notificar("O id informado é inválido!");
                return null;
            }

            return await _setorRepository.PesquisarSetorPorIdAsync(setorId);
        }

        public async Task<SetorViewDto> PesquisarSetorPorIncAsync(int incSetor)
        {
            return await _setorRepository.PesquisarSetorPorIncAsync(incSetor);
        }

        public async Task<List<SetorSelectboxViewDto>> PesquisarSetoresParaSelectboxAsync()
        {
            return await _setorRepository.PesquisarSetoresParaSelectboxAsync();
        }

        public async Task<SetorPaginacaoViewtDto> PesquisarSetoresPorFiltrosPaginacaoAsync(SetorFiltroDto filtroDto)
        {
            if (filtroDto == null)
            {
                Notificar("O filtro informado é inválido!");
                return null;
            }

            return await _setorRepository.PesquisarSetoresPorFiltrosPaginacaoAsync(filtroDto);
        }

        public async Task<List<SetorViewDto>> PesquisarTodosSetoresAsync()
        {
            return await _setorRepository.PesquisarTodosSetoresAsync();
        }

        public void Dispose()
        {
            _setorRepository?.Dispose();
        }

    }
}
