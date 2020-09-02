using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProntoAtendimento.Domain.Dto.OcorrenciaDto;
using ProntoAtendimento.Domain.Entity;
using ProntoAtendimento.Domain.Enums;
using ProntoAtendimento.Repository.Context;
using ProntoAtendimento.Repository.Interface;

namespace ProntoAtendimento.Repository.Implementation
{
    public class OcorrenciaRepository : BaseRepository<Ocorrencia>, IOcorrenciaRepository
    {
        public OcorrenciaRepository(EntityContext context) : base(context)
        {
        }

        public async Task<OcorrenciaResultDto> AbrirOcorrencia(Ocorrencia ocorrencia)
        {

            try
            {
                _context.Ocorrencias.Add(ocorrencia);
                await _context.SaveChangesAsync();
                return new OcorrenciaResultDto
                {
                    Id = ocorrencia.Id.ToString(),
                    IncOcorrecia = ocorrencia.IncOcorrencia.ToString(),
                    Titulo = ocorrencia.Titulo,
                    DataHoraInicio = ocorrencia.DataHoraInicio.ToString("dd/MM/yyyy HH:mm:ss"),
                    DataHoraFim = string.IsNullOrEmpty(ocorrencia.DataHoraFim.ToString()) ? null : ocorrencia.DataHoraFim.ToString(),
                    DataHoraUltimaAtualizacao = ocorrencia.DataHoraUltimaAtualizacao.ToString("dd/MM/yyyy HH:mm:ss"),
                    ChamadoTI = ocorrencia.ChamadoTI,
                    ChamadoFornecedor = ocorrencia.ChamadoFornecedor,
                    OcorrenciaCCM = ocorrencia.OcorrenciaCCM,
                    Acionamento = ocorrencia.Acionamento ? "SIM" : "NAO",
                    Impacto = ocorrencia.Impacto ? "SIM" : "NAO",
                    DescricaoImpacto = ocorrencia.DescricaoImpacto,
                    StatusAtualOcorrencia = ocorrencia.StatusAtualOcorrencia.ToString(),
                    UsuarioId = ocorrencia.UsuarioId.ToString(),
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ExcluirOcorrencia(Guid ocorrenciaId)
        {
            try
            {
                // Ação custosa... a fim de controlar efeito cascate e minimizar impacto na base, excluir dados vinculados a ocorrencia

                // excluir tramites
                foreach (var itemTra in await _context.Tramites.Where(tra => tra.OcorrenciaId.Equals(ocorrenciaId)).ToListAsync())
                {
                    _context.Tramites.Remove(itemTra);
                }

                // excluir OcorrenciaAtivos
                foreach (var itemOA in await _context.OcorrenciaAtivos.Where(oa => oa.OcorrenciaId.Equals(ocorrenciaId)).ToListAsync())
                {
                    _context.OcorrenciaAtivos.Remove(itemOA);
                }

                // excluir TurnoOcorrencia
                foreach (var itemTO in await _context.TurnoOcorrencias.Where(to => to.OcorrenciaId.Equals(ocorrenciaId)).ToListAsync())
                {
                    _context.TurnoOcorrencias.Remove(itemTO);
                }

                // excluir ocorrencia
                Ocorrencia ocorrencia = await _context.Ocorrencias.Where(oco => oco.Id.Equals(ocorrenciaId)).FirstOrDefaultAsync();
                _context.Ocorrencias.Remove(ocorrencia);

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ICollection<OcorrenciaResultDto>> PesquisarOcorrenciasEmAndamento()
        {
            try
            {
                ICollection<OcorrenciaResultDto> listaResult = await _context.Ocorrencias
                    .Where(oco => oco.StatusAtualOcorrencia.Equals(StatusOcorrenciaEnum.ANDAMENTO))
                    .Include(tur => tur.UsuarioId)
                    .DefaultIfEmpty()
                    .Select(oco => new OcorrenciaResultDto
                    {
                        Id = oco.Id.ToString(),
                        IncOcorrecia = oco.IncOcorrencia.ToString(),
                        Titulo = oco.Titulo,
                        DataHoraInicio = oco.DataHoraInicio.ToString("dd/MM/yyyy HH:mm:ss"),
                        DataHoraFim = string.IsNullOrEmpty(oco.DataHoraFim.ToString()) ? null : oco.DataHoraFim.ToString(),
                        DataHoraUltimaAtualizacao = oco.DataHoraUltimaAtualizacao.ToString("dd/MM/yyyy HH:mm:ss"),
                        ChamadoTI = oco.ChamadoTI,
                        ChamadoFornecedor = oco.ChamadoFornecedor,
                        OcorrenciaCCM = oco.OcorrenciaCCM,
                        Acionamento = oco.Acionamento ? "SIM" : "NAO",
                        Impacto = oco.Impacto ? "SIM" : "NAO",
                        DescricaoImpacto = oco.DescricaoImpacto,
                        StatusAtualOcorrencia = oco.StatusAtualOcorrencia.ToString(),
                        UsuarioId = oco.UsuarioId.ToString(),
                        DescricaoUsuario = oco.Usuario.UserName + " - " + oco.Usuario.NomeCompleto,
                    }).ToListAsync();
                return listaResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<OcorrenciaResultDto> PesquisarOcorrenciaPorId(Guid ocorrenciaId)
        {
            try
            {
                OcorrenciaResultDto result = await _context.Ocorrencias
                    .Where(oco => oco.Id == ocorrenciaId)
                    .Include(oco => oco.UsuarioId)
                    .DefaultIfEmpty()
                    .Select(oco => new OcorrenciaResultDto
                    {
                        Id = oco.Id.ToString(),
                        IncOcorrecia = oco.IncOcorrencia.ToString(),
                        Titulo = oco.Titulo,
                        DataHoraInicio = oco.DataHoraInicio.ToString("dd/MM/yyyy HH:mm:ss"),
                        DataHoraFim = string.IsNullOrEmpty(oco.DataHoraFim.ToString()) ? null : oco.DataHoraFim.ToString(),
                        DataHoraUltimaAtualizacao = oco.DataHoraUltimaAtualizacao.ToString("dd/MM/yyyy HH:mm:ss"),
                        ChamadoTI = oco.ChamadoTI,
                        ChamadoFornecedor = oco.ChamadoFornecedor,
                        OcorrenciaCCM = oco.OcorrenciaCCM,
                        Acionamento = oco.Acionamento ? "SIM" : "NAO",
                        Impacto = oco.Impacto ? "SIM" : "NAO",
                        DescricaoImpacto = oco.DescricaoImpacto,
                        StatusAtualOcorrencia = oco.StatusAtualOcorrencia.ToString(),
                        UsuarioId = oco.UsuarioId.ToString(),
                        DescricaoUsuario = oco.Usuario.UserName + " - " + oco.Usuario.NomeCompleto,
                    }
                    ).SingleAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<OcorrenciaResultDto> PesquisarOcorrenciaPorInc(int incOcorrencia)
        {
            try
            {
                OcorrenciaResultDto result = await _context.Ocorrencias
                    .Where(oco => oco.IncOcorrencia == incOcorrencia)
                    .Include(oco => oco.UsuarioId)
                    .DefaultIfEmpty()
                    .Select(oco => new OcorrenciaResultDto
                    {
                        Id = oco.Id.ToString(),
                        IncOcorrecia = oco.IncOcorrencia.ToString(),
                        Titulo = oco.Titulo,
                        DataHoraInicio = oco.DataHoraInicio.ToString("dd/MM/yyyy HH:mm:ss"),
                        DataHoraFim = string.IsNullOrEmpty(oco.DataHoraFim.ToString()) ? null : oco.DataHoraFim.ToString(),
                        DataHoraUltimaAtualizacao = oco.DataHoraUltimaAtualizacao.ToString("dd/MM/yyyy HH:mm:ss"),
                        ChamadoTI = oco.ChamadoTI,
                        ChamadoFornecedor = oco.ChamadoFornecedor,
                        OcorrenciaCCM = oco.OcorrenciaCCM,
                        Acionamento = oco.Acionamento ? "SIM" : "NAO",
                        Impacto = oco.Impacto ? "SIM" : "NAO",
                        DescricaoImpacto = oco.DescricaoImpacto,
                        StatusAtualOcorrencia = oco.StatusAtualOcorrencia.ToString(),
                        UsuarioId = oco.UsuarioId.ToString(),
                        DescricaoUsuario = oco.Usuario.UserName + " - " + oco.Usuario.NomeCompleto,
                    }
                    ).SingleAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ICollection<OcorrenciaResultDto>> PesquisarOcorrenciasPorUsuario(Guid usuarioId)
        {
            try
            {
                ICollection<OcorrenciaResultDto> listaResult = await _context.Ocorrencias
                    .Where(oco => oco.UsuarioId.Equals(usuarioId))
                    .Include(oco => oco.UsuarioId)
                    .DefaultIfEmpty()
                    .Select(oco => new OcorrenciaResultDto
                    {
                        Id = oco.Id.ToString(),
                        IncOcorrecia = oco.IncOcorrencia.ToString(),
                        Titulo = oco.Titulo,
                        DataHoraInicio = oco.DataHoraInicio.ToString("dd/MM/yyyy HH:mm:ss"),
                        DataHoraFim = string.IsNullOrEmpty(oco.DataHoraFim.ToString()) ? null : oco.DataHoraFim.ToString(),
                        DataHoraUltimaAtualizacao = oco.DataHoraUltimaAtualizacao.ToString("dd/MM/yyyy HH:mm:ss"),
                        ChamadoTI = oco.ChamadoTI,
                        ChamadoFornecedor = oco.ChamadoFornecedor,
                        OcorrenciaCCM = oco.OcorrenciaCCM,
                        Acionamento = oco.Acionamento ? "SIM" : "NAO",
                        Impacto = oco.Impacto ? "SIM" : "NAO",
                        DescricaoImpacto = oco.DescricaoImpacto,
                        StatusAtualOcorrencia = oco.StatusAtualOcorrencia.ToString(),
                        UsuarioId = oco.UsuarioId.ToString(),
                        DescricaoUsuario = oco.Usuario.UserName + " - " + oco.Usuario.NomeCompleto,
                    }).ToListAsync();
                return listaResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ICollection<OcorrenciaResultDto>> PesquisarOcorrenciasPorTurno(Guid turnoId)
        {
            try
            {
             
                var listaOcorrenciaResultDto =  

                    from oco in _context.Ocorrencias
                    join tuoc in _context.TurnoOcorrencias on oco.Id equals tuoc.OcorrenciaId
                    join usu in _context.Users on oco.UsuarioId equals usu.Id
                    where tuoc.TurnoId == turnoId

                    select new OcorrenciaResultDto
                    {
                        Id = oco.Id.ToString(),
                        IncOcorrecia = oco.IncOcorrencia.ToString(),
                        Titulo = oco.Titulo,
                        DataHoraInicio = oco.DataHoraInicio.ToString("dd/MM/yyyy HH:mm:ss"),
                        DataHoraFim = string.IsNullOrEmpty(oco.DataHoraFim.ToString()) ? null : oco.DataHoraFim.ToString(),
                        DataHoraUltimaAtualizacao = oco.DataHoraUltimaAtualizacao.ToString("dd/MM/yyyy HH:mm:ss"),
                        ChamadoTI = oco.ChamadoTI,
                        ChamadoFornecedor = oco.ChamadoFornecedor,
                        OcorrenciaCCM = oco.OcorrenciaCCM,
                        Acionamento = oco.Acionamento ? "SIM" : "NAO",
                        Impacto = oco.Impacto ? "SIM" : "NAO",
                        DescricaoImpacto = oco.DescricaoImpacto,
                        StatusAtualOcorrencia = oco.StatusAtualOcorrencia.ToString(),
                        UsuarioId = oco.UsuarioId.ToString(),
                        DescricaoUsuario = oco.Usuario.UserName + " - " + oco.Usuario.NomeCompleto,
                    };

                return await listaOcorrenciaResultDto.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ICollection<OcorrenciaResultDto>> PesquisarOcorrenciasPorFiltros(OcorrenciaFiltroDto ocorrenciaFiltroDto)
        {
            try
            {
                IQueryable<Ocorrencia> query = _context.Ocorrencias;

                if (!string.IsNullOrEmpty(ocorrenciaFiltroDto.Titulo))
                {
                    query = query.Where(oco => oco.Titulo.Contains(ocorrenciaFiltroDto.Titulo));
                }

                if (!string.IsNullOrEmpty(ocorrenciaFiltroDto.Titulo))
                {
                    query = query.Where(oco => oco.Titulo.Contains(ocorrenciaFiltroDto.Titulo));
                }
                if (!string.IsNullOrEmpty(ocorrenciaFiltroDto.DataHoraInicio))
                {
                    query = query.Where(oco => oco.DataHoraInicio >= DateTime.Parse(ocorrenciaFiltroDto.DataHoraInicio));
                }
                if (!string.IsNullOrEmpty(ocorrenciaFiltroDto.DataHoraFim))
                {
                    query = query.Where(oco => oco.DataHoraFim <= DateTime.Parse(ocorrenciaFiltroDto.DataHoraFim));
                }
                if (!string.IsNullOrEmpty(ocorrenciaFiltroDto.ChamadoTI))
                {
                    query = query.Where(oco => oco.ChamadoTI.Contains(ocorrenciaFiltroDto.ChamadoTI));
                }
                if (!string.IsNullOrEmpty(ocorrenciaFiltroDto.ChamadoFornecedor))
                {
                    query = query.Where(oco => oco.ChamadoFornecedor.Contains(ocorrenciaFiltroDto.ChamadoFornecedor));
                }
                if (!string.IsNullOrEmpty(ocorrenciaFiltroDto.OcorrenciaCCM))
                {
                    query = query.Where(oco => oco.OcorrenciaCCM.Contains(ocorrenciaFiltroDto.OcorrenciaCCM));
                }
                if (!ocorrenciaFiltroDto.Acionamento.Equals(null))
                {
                    query = query.Where(oco => oco.Acionamento.Equals(ocorrenciaFiltroDto.Acionamento));
                }
                if (!ocorrenciaFiltroDto.Impacto.Equals(null))
                {
                    query = query.Where(oco => oco.Acionamento.Equals(ocorrenciaFiltroDto.Acionamento));
                }
                if (!string.IsNullOrEmpty(ocorrenciaFiltroDto.StatusAtualOcorrencia))
                {
                    query = query.Where(oco => oco.StatusAtualOcorrencia.ToString().Equals(ocorrenciaFiltroDto.StatusAtualOcorrencia));
                }
                if (!ocorrenciaFiltroDto.UsuarioId.Equals(null))
                {
                    query = query.Where(oco => oco.UsuarioId.Equals(ocorrenciaFiltroDto.UsuarioId));
                }

                ICollection<OcorrenciaResultDto> listaResult = await query
                    .DefaultIfEmpty()
                    .Select(oco => new OcorrenciaResultDto
                    {
                        Id = oco.Id.ToString(),
                        IncOcorrecia = oco.IncOcorrencia.ToString(),
                        Titulo = oco.Titulo,
                        DataHoraInicio = oco.DataHoraInicio.ToString("dd/MM/yyyy HH:mm:ss"),
                        DataHoraFim = string.IsNullOrEmpty(oco.DataHoraFim.ToString()) ? null : oco.DataHoraFim.ToString(),
                        DataHoraUltimaAtualizacao = oco.DataHoraUltimaAtualizacao.ToString("dd/MM/yyyy HH:mm:ss"),
                        ChamadoTI = oco.ChamadoTI,
                        ChamadoFornecedor = oco.ChamadoFornecedor,
                        OcorrenciaCCM = oco.OcorrenciaCCM,
                        Acionamento = oco.Acionamento ? "SIM" : "NAO",
                        Impacto = oco.Impacto ? "SIM" : "NAO",
                        DescricaoImpacto = oco.DescricaoImpacto,
                        StatusAtualOcorrencia = oco.StatusAtualOcorrencia.ToString(),
                        UsuarioId = oco.UsuarioId.ToString(),
                        DescricaoUsuario = oco.Usuario.UserName + " - " + oco.Usuario.NomeCompleto,
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
