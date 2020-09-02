using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProntoAtendimento.Domain.Dto.TramiteDto;
using ProntoAtendimento.Domain.Entity;
using ProntoAtendimento.Repository.Context;
using ProntoAtendimento.Repository.Interface;

namespace ProntoAtendimento.Repository.Implementation
{
    public class TramiteRepository : BaseRepository<Tramite>, ITramiteRepository
    {
        public TramiteRepository(EntityContext context) : base(context)
        {
        }

        public async Task<TramiteResultDto> PesquisarTramitePorId(Guid tramiteId)
        {
            try
            {
                TramiteResultDto result = await _context.Tramites
                    .Include(tra => tra.UsuarioId)
                    .Include(tra => tra.OcorrenciaId)
                    .Where(tra => tra.Id.Equals(tramiteId))
                    .DefaultIfEmpty()
                    .Select(tra => new TramiteResultDto
                    {
                        Id = tra.Id.ToString(),
                        IncTramite = tra.IncTramite.ToString(),
                        Descricao = tra.Descricao,
                        DataHora = tra.DataHora.ToString("dd/MM/yyyy HH:mm:ss"),
                        Solucao = tra.Solucao ? "SIM" : "NAO",
                        OcorrenciaId = tra.OcorrenciaId.ToString(),
                        UsuarioId = tra.UsuarioId.ToString(),
                        DescricaoOcorrencia = tra.Ocorrencia.IncOcorrencia.ToString() + " - " + tra.Ocorrencia.Titulo,
                        DescricaoUsuario = tra.Usuario.UserName + " - " + tra.Usuario.NomeCompleto,
                    }).SingleAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TramiteResultDto> PesquisarTramitePorInc(int incTramite)
        {
            try
            {
                TramiteResultDto result = await _context.Tramites
                    .Include(tra => tra.UsuarioId)
                    .Include(tra => tra.OcorrenciaId)
                    .Where(tra => tra.IncTramite.Equals(incTramite))
                    .DefaultIfEmpty()
                    .Select(tra => new TramiteResultDto
                    {
                        Id = tra.Id.ToString(),
                        IncTramite = tra.IncTramite.ToString(),
                        Descricao = tra.Descricao,
                        DataHora = tra.DataHora.ToString("dd/MM/yyyy HH:mm:ss"),
                        Solucao = tra.Solucao ? "SIM" : "NAO",
                        OcorrenciaId = tra.OcorrenciaId.ToString(),
                        UsuarioId = tra.UsuarioId.ToString(),
                        DescricaoOcorrencia = tra.Ocorrencia.IncOcorrencia.ToString() + " - " + tra.Ocorrencia.Titulo,
                        DescricaoUsuario = tra.Usuario.UserName + " - " + tra.Usuario.NomeCompleto,
                    }).SingleAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ICollection<TramiteResultDto>> PesquisarTramitesPorOcorrencia(Guid ocorrenciaId)
        {
            try
            {
                ICollection<TramiteResultDto> listaResult = await _context.Tramites
                    .Include(tra => tra.UsuarioId)
                    .Include(tra => tra.OcorrenciaId)
                    .Where(tra => tra.OcorrenciaId.Equals(ocorrenciaId))
                    .DefaultIfEmpty()
                    .Select(tra => new TramiteResultDto
                    {
                        Id = tra.Id.ToString(),
                        IncTramite = tra.IncTramite.ToString(),
                        Descricao = tra.Descricao,
                        DataHora = tra.DataHora.ToString("dd/MM/yyyy HH:mm:ss"),
                        Solucao = tra.Solucao ? "SIM" : "NAO",
                        OcorrenciaId = tra.OcorrenciaId.ToString(),
                        UsuarioId = tra.UsuarioId.ToString(),
                        DescricaoOcorrencia = tra.Ocorrencia.IncOcorrencia.ToString() + " - " + tra.Ocorrencia.Titulo,
                        DescricaoUsuario = tra.Usuario.UserName + " - " + tra.Usuario.NomeCompleto,
                    }).ToListAsync();
                return listaResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ICollection<TramiteResultDto>> PesquisarTramitesPorUsuario(Guid usuarioId)
        {
            try
            {
                ICollection<TramiteResultDto> listaResult = await _context.Tramites
                    .Include(tra => tra.UsuarioId)
                    .Include(tra => tra.OcorrenciaId)
                    .Where(tra => tra.UsuarioId.Equals(usuarioId))
                    .DefaultIfEmpty()
                    .Select(tra => new TramiteResultDto
                    {
                        Id = tra.Id.ToString(),
                        IncTramite = tra.IncTramite.ToString(),
                        Descricao = tra.Descricao,
                        DataHora = tra.DataHora.ToString("dd/MM/yyyy HH:mm:ss"),
                        Solucao = tra.Solucao ? "SIM" : "NAO",
                        OcorrenciaId = tra.OcorrenciaId.ToString(),
                        UsuarioId = tra.UsuarioId.ToString(),
                        DescricaoOcorrencia = tra.Ocorrencia.IncOcorrencia.ToString() + " - " + tra.Ocorrencia.Titulo,
                        DescricaoUsuario = tra.Usuario.UserName + " - " + tra.Usuario.NomeCompleto,
                    }).ToListAsync();
                return listaResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ICollection<TramiteResultDto>> PesquisarTramitesPorFiltros(TramiteFiltroDto tramiteFiltroDto)
        {
            try
            {
                IQueryable<Tramite> query = _context.Tramites;

                if (!string.IsNullOrEmpty(tramiteFiltroDto.Descricao))
                {
                    query = query.Where(tra => tra.Descricao.Contains(tramiteFiltroDto.Descricao));
                }
                if (!string.IsNullOrEmpty(tramiteFiltroDto.DataHoraInicio) && !string.IsNullOrEmpty(tramiteFiltroDto.DataHoraFim))
                {
                    query = query.Where(tra => tra.DataHora >= DateTime.Parse(tramiteFiltroDto.DataHoraInicio)
                                            && tra.DataHora <= DateTime.Parse(tramiteFiltroDto.DataHoraFim));
                }
                if (!string.IsNullOrEmpty(tramiteFiltroDto.Solucao))
                {
                    query = query.Where(tra => tra.Solucao.ToString().Equals(tramiteFiltroDto.Solucao));
                }
                if (!string.IsNullOrEmpty(tramiteFiltroDto.OcorrenciaId))
                {
                    query = query.Where(tra => tra.OcorrenciaId.ToString().Equals(tramiteFiltroDto.OcorrenciaId));
                }
                if (!string.IsNullOrEmpty(tramiteFiltroDto.UsuarioId))
                {
                    query = query.Where(tra => tra.UsuarioId.ToString().Equals(tramiteFiltroDto.UsuarioId));
                }

                ICollection<TramiteResultDto> listaResult = await query
                    .DefaultIfEmpty()
                    .Select(tra => new TramiteResultDto
                    {
                        Id = tra.Id.ToString(),
                        IncTramite = tra.IncTramite.ToString(),
                        Descricao = tra.Descricao,
                        DataHora = tra.DataHora.ToString("dd/MM/yyyy HH:mm:ss"),
                        Solucao = tra.Solucao ? "SIM" : "NAO",
                        OcorrenciaId = tra.OcorrenciaId.ToString(),
                        UsuarioId = tra.UsuarioId.ToString(),
                        DescricaoOcorrencia = tra.Ocorrencia.IncOcorrencia.ToString() + " - " + tra.Ocorrencia.Titulo,
                        DescricaoUsuario = tra.Usuario.UserName+ " - " + tra.Usuario.NomeCompleto,
                    }).ToListAsync();
                return listaResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
