using Microsoft.EntityFrameworkCore;
using ProntoAtendimento.Domain.Entity;
using ProntoAtendimento.Repository.Context;
using ProntoAtendimento.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProntoAtendimento.Repository.Implementation
{
    public class OcorrenciaAtivoRepository : IOcorrenciaAtivoRepository
    {
        protected readonly EntityContext _context;
        private DbSet<OcorrenciaAtivo> _dtOA;

        public OcorrenciaAtivoRepository(EntityContext context)
        {
            _context = context;
            _dtOA = context.Set<OcorrenciaAtivo>(); ;
        }

        public async Task<bool> CadastrarOcorrenciaAtivo(OcorrenciaAtivo ocorrenciaAtivo)
        {
            try
            {
                _context.OcorrenciaAtivos.Add(ocorrenciaAtivo);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ExcluirOcorrenciaAtivo(OcorrenciaAtivo ocorrenciaAtivo)
        {
            try
            {
                var result = await _context.OcorrenciaAtivos.SingleOrDefaultAsync(oa => oa.OcorrenciaId.Equals(ocorrenciaAtivo.OcorrenciaId) && oa.AtivoId.Equals(ocorrenciaAtivo.AtivoId));
                if (result != null)
                {
                    _context.OcorrenciaAtivos.Remove(result);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> EditarPrincipalOcorrenciaAtivo(OcorrenciaAtivo ocorrenciaAtivo)
        {
            try
            {
                var result = await _context.OcorrenciaAtivos.SingleOrDefaultAsync(oa => oa.OcorrenciaId.Equals(ocorrenciaAtivo.OcorrenciaId) && oa.AtivoId.Equals(ocorrenciaAtivo.AtivoId));
                if (result != null)
                {
                    result.Principal = ocorrenciaAtivo.Principal;
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<OcorrenciaAtivo> PesquisarOcorrenciaAtivo(Guid ocorrenciaId, Guid ativoId)
        {
            try
            {
                var result = await _context.OcorrenciaAtivos.SingleOrDefaultAsync(oa => oa.OcorrenciaId.Equals(ocorrenciaId) && oa.AtivoId.Equals(ativoId));
                if (result != null) return result;
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // quantas/quais ativos de uma ocorrencia - (retorna dados para extrair count)
        public async Task<ICollection<OcorrenciaAtivo>> PesquisarOcorrenciaAtivoPorOcorrencia(Guid ocorrenciaId)
        {
            try
            {
                return await _context.OcorrenciaAtivos.Where(to => to.OcorrenciaId.Equals(ocorrenciaId)).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // quantas/quais ocorrencias o ativo está - (retorna dados para extrair count)
        public async Task<ICollection<OcorrenciaAtivo>> PesquisarOcorrenciaAtivoPorAtivo(Guid ativoId)
        {
            try
            {
                return await _context.OcorrenciaAtivos.Where(to => to.AtivoId.Equals(ativoId)).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
