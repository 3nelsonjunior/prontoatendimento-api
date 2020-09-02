using Microsoft.EntityFrameworkCore;
using ProntoAtendimento.Domain.Dto.OcorrenciaDto;
using ProntoAtendimento.Domain.Entity;
using ProntoAtendimento.Domain.Enums;
using ProntoAtendimento.Repository.Context;
using ProntoAtendimento.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProntoAtendimento.Repository.Implementation
{
    public class TurnoOcorrenciaRepository : ITurnoOcorrenciaRepository
    {
        protected readonly EntityContext _context;
        private readonly DbSet<TurnoOcorrencia> _dtTO;

        public TurnoOcorrenciaRepository(EntityContext context)
        {
            _context = context;
            _dtTO = context.Set<TurnoOcorrencia>(); ;
        }


        public async Task<bool> CadastrarTurnoOcorrencia(TurnoOcorrencia turnoOcorrencia)
        {
            try
            {
                _context.TurnoOcorrencias.Add(turnoOcorrencia);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> AdicionarOcorrenciasEmAbertoAoTurno(Guid turnoId, ICollection<OcorrenciaResultDto> listaOcorrenciasEmAberto)
        {
            try
            {
                foreach(OcorrenciaResultDto item in listaOcorrenciasEmAberto)
                {
                    TurnoOcorrencia tur_oco = new TurnoOcorrencia(turnoId, 
                                                                  Guid.Parse(item.Id),
                                                                  Enum.Parse<StatusOcorrenciaEnum>(item.StatusAtualOcorrencia));
                    _context.TurnoOcorrencias.Add(tur_oco);
                    await _context.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        public async Task<bool> EditarStatusTurnoOcorrencia(TurnoOcorrencia turnoOcorrencia)
        {
            try
            {
                var result = await _context.TurnoOcorrencias.SingleOrDefaultAsync(to => to.TurnoId.Equals(turnoOcorrencia.TurnoId) && to.OcorrenciaId.Equals(turnoOcorrencia.OcorrenciaId));
                if (result != null)
                {
                    _context.Entry(result).CurrentValues.SetValues(turnoOcorrencia);
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

        public async Task<bool> ExcluirTurnoOcorrencia(TurnoOcorrencia turnoOcorrencia)
        {
            try
            {
                var result = await _context.TurnoOcorrencias.SingleOrDefaultAsync(to => to.TurnoId.Equals(turnoOcorrencia.TurnoId) && to.OcorrenciaId.Equals(turnoOcorrencia.OcorrenciaId));
                if (result != null)
                {
                    _context.TurnoOcorrencias.Remove(result);
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

        public async Task<TurnoOcorrencia> PesquisarTurnoOcorrencia(Guid turnoId, Guid ocorrenciaId)
        {
            try
            {
                var result = await _context.TurnoOcorrencias.SingleOrDefaultAsync(to => to.TurnoId.Equals(ocorrenciaId) && to.OcorrenciaId.Equals(ocorrenciaId));
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

        // quantas/quais ocorrencias o turno tem - (retorna dados para extrari count)
        public async Task<ICollection<TurnoOcorrencia>> PesquisarTurnoOcorrenciaPorTurno(Guid turnoId)
        {
            try
            {
                return await _context.TurnoOcorrencias.Where(to => to.TurnoId.Equals(turnoId)).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // quantos/quais turnos uma ocorrencia pertence  - (retorna dados para extrari count)
        public async Task<ICollection<TurnoOcorrencia>> PesquisarTurnoOcorrenciaPorOcorrencia(Guid ocorrenciaId)
        {
            try
            {
                return await _context.TurnoOcorrencias.Where(to => to.OcorrenciaId.Equals(ocorrenciaId)).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
