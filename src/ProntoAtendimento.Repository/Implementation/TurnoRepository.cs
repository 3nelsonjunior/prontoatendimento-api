using Microsoft.EntityFrameworkCore;
using ProntoAtendimento.Domain.Dto.TurnoDto;
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
    public class TurnoRepository : BaseRepository<Turno>, ITurnoRepository
    {
        public TurnoRepository(EntityContext context) : base(context)
        {
        }

        public async Task<Guid> AbrirTurno(Turno turno)
        {
            try
            {
                _context.Turnos.Add(turno);
                await _context.SaveChangesAsync();
                return turno.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ReabrirTurno(Guid turnoId)
        {
            try
            {
                Turno turno = await _context.Turnos.SingleOrDefaultAsync(aux => aux.Id.Equals(turnoId));
                if (turno != null)
                {
                    turno.StatusTurno = StatusTurnoEnum.ABERTO;
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

        public async Task<bool> FecharTurno(Guid turnoId)
        {
            try
            {
                var turno = await _context.Turnos.SingleOrDefaultAsync(aux => aux.Id.Equals(turnoId));
                if (turno != null)
                {
                    turno.StatusTurno = StatusTurnoEnum.FECHADO;
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

        public async Task<bool> EditarTurno(Turno turno)
        {
            try
            {
                _context.Turnos.Update(turno);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Excluir turno e seus
        public async Task<bool> ExcluirTurno(Guid turnoId)
        {
            try
            {
                ICollection<TurnoOcorrencia> listaTurnoOcorrencia = await _context.TurnoOcorrencias.Where(to => to.TurnoId.Equals(turnoId)).ToListAsync();
                
                if(!listaTurnoOcorrencia.Contains(null))
                {
                    // Ação custosa... a fim de controlar efeito cascate e minimizar impacto na base, para cada ocorrência relacionada a este turno
                    // 1º verifica se a ocorrência está vinculada apaenas a este turno(totalTurnos)
                    // 2º se SIM deleta a ocorrência e suas dependencias
                    //    se NÃO deleta apenas os dados relacionados em TurnoOcorrencia      
                    foreach (TurnoOcorrencia itemTO in listaTurnoOcorrencia)
                    {
                        ICollection<TurnoOcorrencia> listaTurnoOcorrenciaPorOcorrencia = await _context.TurnoOcorrencias.Where(to => to.OcorrenciaId.Equals(itemTO.OcorrenciaId)).ToListAsync();
                        if(listaTurnoOcorrenciaPorOcorrencia.Count == 1 && !listaTurnoOcorrenciaPorOcorrencia.Contains(null))
                        {
                            // excluir Tramites
                            foreach (var itemTra in await _context.Tramites.Where(tra => tra.OcorrenciaId.Equals(itemTO.OcorrenciaId)).ToListAsync())
                            {
                                _context.Tramites.Remove(itemTra);
                            }

                            // excluir OcorrenciaAtivos
                            foreach (var itemOA in await _context.OcorrenciaAtivos.Where(oa => oa.OcorrenciaId.Equals(itemTO.OcorrenciaId)).ToListAsync())
                            {
                                _context.OcorrenciaAtivos.Remove(itemOA);
                            }

                            // excluir TurnoOcorrencia
                            _context.TurnoOcorrencias.Remove(itemTO);

                            // excluir Ocorrência
                            Ocorrencia ocorrencia = await _context.Ocorrencias.Where(oco => oco.Id.Equals(itemTO.OcorrenciaId)).FirstOrDefaultAsync();
                            _context.Ocorrencias.Remove(ocorrencia);
                        }
                        else
                        {
                            _context.TurnoOcorrencias.Remove(itemTO);
                        }
                    }
                }

                Turno turno = await _context.Turnos.Where(tur => tur.Id.Equals(turnoId)).FirstOrDefaultAsync();
                _context.Turnos.Remove(turno);

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> PesquisarExisteTurnoAberto()
        {
            try
            {
                Turno result = await _context.Turnos.FirstOrDefaultAsync(tur => tur.StatusTurno.Equals(StatusTurnoEnum.ABERTO));
                    
                if (result == null) return false;

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // validar se existe Data/Hora é a mais recente
        public async Task<bool> PesquisarExisteTurnoDataHoraMaisRecente(DateTime dataHoraInicio)
        {
            try
            {
                Turno result = await _context.Turnos.FirstOrDefaultAsync(tur => tur.DataHoraInicio > dataHoraInicio);

                if (result == null) return false;

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // validar se existe data/hora igual
        public async Task<bool> PesquisarExisteTurnoDataHoraRepetido(DateTime dataHoraInicio, DateTime dataHoraFim, Guid turnoId)
        {
            try
            {
                Turno result = await _context.Turnos.FirstOrDefaultAsync(tur => tur.DataHoraInicio.Equals(dataHoraInicio) &&
                                                                                tur.DataHoraFim.Equals(dataHoraFim) &&
                                                                                !tur.Id.Equals(turnoId));

                if (result == null) return false;

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TurnoResultDto> PesquisarTurnoPorId(Guid turnoId)
        {
            try
            {
                TurnoResultDto result = await _context.Turnos
                    .Where(tur => tur.Id == turnoId)
                    .Include(tur => tur.UsuarioId)
                    .DefaultIfEmpty()
                    .Select(tur => new TurnoResultDto
                    {
                        Id = tur.Id.ToString(),
                        IncTurno = tur.IncTurno.ToString(),
                        DataHoraInicio = tur.DataHoraInicio.ToString("dd/MM/yyyy HH:mm:ss"),
                        DataHoraFim = tur.DataHoraFim.ToString("dd/MM/yyyy HH:mm:ss"),
                        StatusTurno = ConverterRetornoEnumString(tur.StatusTurno),
                        UsuarioId = tur.UsuarioId.ToString(),
                        DescricaoUsuario = tur.Usuario.UserName + " - " + tur.Usuario.NomeCompleto,
                    }).SingleAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TurnoResultDto> PesquisarTurnoPorInc(int incTurno)
        {
            try
            {
                TurnoResultDto result = await _context.Turnos
                    .Where(tur => tur.IncTurno == incTurno)
                    .Include(tur => tur.UsuarioId)
                    .DefaultIfEmpty()
                    .Select(tur => new TurnoResultDto
                    {
                        Id = tur.Id.ToString(),
                        IncTurno = tur.IncTurno.ToString(),
                        DataHoraInicio = tur.DataHoraInicio.ToString("dd/MM/yyyy HH:mm:ss"),
                        DataHoraFim = tur.DataHoraFim.ToString("dd/MM/yyyy HH:mm:ss"),
                        StatusTurno = ConverterRetornoEnumString(tur.StatusTurno),
                        UsuarioId = tur.UsuarioId.ToString(),
                        DescricaoUsuario = tur.Usuario.UserName + " - " + tur.Usuario.NomeCompleto,
                    }).SingleAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ICollection<TurnoResultDto>> PesquisarTurnosPorUsuario(Guid usuarioId)
        {
            try
            {
                ICollection<TurnoResultDto> listaResult = await _context.Turnos
                    .Where(tur => tur.UsuarioId.Equals(usuarioId))
                    .Include(tur => tur.UsuarioId)
                    .DefaultIfEmpty()
                    .Select(tur => new TurnoResultDto
                    {
                        Id = tur.Id.ToString(),
                        IncTurno = tur.IncTurno.ToString(),
                        DataHoraInicio = tur.DataHoraInicio.ToString("dd/MM/yyyy HH:mm:ss"),
                        DataHoraFim = tur.DataHoraFim.ToString("dd/MM/yyyy HH:mm:ss"),
                        StatusTurno =  ConverterRetornoEnumString(tur.StatusTurno),
                        UsuarioId = tur.UsuarioId.ToString(),
                        DescricaoUsuario = tur.Usuario.UserName + " - " + tur.Usuario.NomeCompleto,
                    }).ToListAsync();
                return listaResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TurnoPaginacaoViewtDto> PesquisarTurnosPorFiltrosPaginacaoAsync(TurnoFiltroDto filtroDto)
        {
            try
            {
                TurnoPaginacaoViewtDto paginacaoViewDto = new TurnoPaginacaoViewtDto();

                filtroDto.RegistroInicial = filtroDto.RegistroInicial <= 1 ? 0 : filtroDto.RegistroInicial - 1;

                string query = @"SELECT SQL_CALC_FOUND_ROWS DISTINCT ";

                query = query + "tur.Id, " +
                                "tur.Inc_Turno, " +
                                "tur.Data_Hora_Inicio, " +
                                "tur.Data_Hora_Fim, " +
                                "tur.Status_Turno, " +
                                "tur.Usuario_Id, ";

                query = query + "CONCAT(usu.UserName, ' - ', usu.NomeCompleto) as 'Descricao_Usuario' ";

                query = query + "FROM Turno AS tur";

                query = query + $" INNER JOIN usuario AS usu ON usu.Id = tur.Usuario_Id";

                // WHERE
                query = query + " WHERE 1 = 1 ";

                if (!string.IsNullOrEmpty(filtroDto.DataHoraInicio) && !string.IsNullOrEmpty(filtroDto.DataHoraFim))
                {
                    query = query + $"AND usu.DataHoraCadastro >= '{filtroDto.DataHoraInicio}' ";

                    query = query + $"AND usu.DataHoraCadastro <= '{filtroDto.DataHoraFim}' ";
                }

                if (!string.IsNullOrEmpty(filtroDto.UsuarioId))
                {
                    query = query + $"AND tur.Usuario_Id = '{filtroDto.UsuarioId}' ";
                }

                if (!string.IsNullOrEmpty(filtroDto.StatusTurno))
                {
                    query = query + $"AND tur.Status_Turno = {int.Parse(filtroDto.StatusTurno)} ";
                }

                query = query + "ORDER BY  tur.Data_Hora_Inicio ";

                // LIMIT RegistroInicial, QtdRegistroPorPagina;
                query = query + $" LIMIT {filtroDto.RegistroInicial},{filtroDto.QtdRegistroPorPagina} ";

                string queryTotalRegitrosEncontrados = "SELECT FOUND_ROWS() AS totalRegistros";

                var connection = _context.Database.GetDbConnection();

                using (var command = connection.CreateCommand())
                {
                    await connection.OpenAsync();
                    command.CommandText = query;
                    using (var dataReader = await command.ExecuteReaderAsync())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                // Registros
                                TurnoTabelaViewDto viewDto = new TurnoTabelaViewDto();
                                viewDto.Id = Guid.Parse(dataReader["Id"].ToString());
                                viewDto.IncTurno = dataReader["Inc_Turno"].ToString();
                                viewDto.DataHoraInicio = dataReader["Data_Hora_Inicio"].ToString();
                                viewDto.DataHoraFim = dataReader["Data_Hora_Fim"].ToString();
                                viewDto.StatusTurno = dataReader["Status_Turno"].ToString();
                                viewDto.DescricaoStatusTurno = viewDto.descricaoStatusTurnoEnum(dataReader["Status_Turno"].ToString().ToUpper());
                                viewDto.UsuarioId = Guid.Parse(dataReader["Usuario_Id"].ToString());
                                viewDto.DescricaoUsuario = dataReader["Descricao_Usuario"].ToString();
                                paginacaoViewDto.ListaTurnoTabelaViewDto.Add(viewDto);
                            }
                        }
                    }

                    command.CommandText = queryTotalRegitrosEncontrados;
                    using (var dataReader = command.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                // Total Registros.: TotalRegistros
                                paginacaoViewDto.TotalRegistros = int.Parse(dataReader["totalRegistros"].ToString()); ;
                            }
                        }
                    }
                }

                paginacaoViewDto.preencherDadosPaginacao(filtroDto.QtdRegistroPorPagina, filtroDto.RegistroInicial);

                return paginacaoViewDto;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        // serviços
        public string ConverterRetornoEnumString(Enum valorEnum)
        {
            var valor = (Enum)valorEnum;
            return valor.ToString();
        }
    }
}
