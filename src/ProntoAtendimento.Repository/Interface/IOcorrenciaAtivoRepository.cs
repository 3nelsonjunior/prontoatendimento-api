using ProntoAtendimento.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProntoAtendimento.Repository.Interface
{
    public interface IOcorrenciaAtivoRepository
    {
        Task<bool> CadastrarOcorrenciaAtivo(OcorrenciaAtivo ocorrenciaAtivo);
        Task<bool> EditarPrincipalOcorrenciaAtivo(OcorrenciaAtivo ocorrenciaAtivo);
        Task<bool> ExcluirOcorrenciaAtivo(OcorrenciaAtivo ocorrenciaAtivo);
        Task<OcorrenciaAtivo> PesquisarOcorrenciaAtivo(Guid ocorrenciaId, Guid ativoId);
        Task<ICollection<OcorrenciaAtivo>> PesquisarOcorrenciaAtivoPorAtivo(Guid ativoId);
        Task<ICollection<OcorrenciaAtivo>> PesquisarOcorrenciaAtivoPorOcorrencia(Guid ocorrenciaId);
    }
}
