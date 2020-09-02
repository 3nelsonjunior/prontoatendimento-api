using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProntoAtendimento.Domain.Dto.AtivoDto;
using ProntoAtendimento.Domain.Entity;
using ProntoAtendimento.Repository.Context;
using ProntoAtendimento.Repository.Interface;

namespace ProntoAtendimento.Repository.Implementation
{
    public class AtivoRepository : BaseRepository<Ativo>, IAtivoRepository
    {
        public AtivoRepository(EntityContext context) : base(context)
        {
        }

        public async Task<AtivoViewDto> PesquisarAtivoPorIdAsync(Guid ativoId)
        {
            try
            {
                AtivoViewDto result = new AtivoViewDto();
                result = await _context.Ativos
                    .Where(ati => ati.Id == ativoId)
                    .Include(ati => ati.SetorId)
                    .DefaultIfEmpty()
                    .Select(ati => new AtivoViewDto
                    {
                        Id = ati.Id,
                        IncAtivo = ati.IncAtivo,
                        Nome = ati.Nome,
                        Descricao = ati.Descricao,
                        DataHoraCadastro = ati.DataHoraCadastro,
                        DataHoraUltimaAtualizacao = ati.DataHoraUltimaAtualizacao,
                        StatusAtivo = ati.StatusAtivo.Value,
                        CriticidadeAtivo = ati.CriticidadeAtivo.Value,
                        DescricaoStatusAtivo = result.descricaoStatusAtivoEnum(ati.StatusAtivo.Value.ToString()),
                        DescricaoCriticidadeAtivo = result.descricaoCriticidadeAtivoEnum(ati.CriticidadeAtivo.Value.ToString()),
                        SetorId = Guid.Parse(ati.SetorId.ToString()),
                        DescricaoSetor = ati.Setor.IncSetor.ToString() + " - " + ati.Setor.Nome,
                    }).SingleAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<AtivoViewDto> PesquisarAtivoPorIncAsync(int incAtivo)
        {
            try
            {
                AtivoViewDto result = new AtivoViewDto();
                result = await _context.Ativos
                    .Where(ati => ati.IncAtivo == incAtivo)
                    .Include(ati => ati.SetorId)
                    .DefaultIfEmpty()
                    .Select(ati => new AtivoViewDto
                    {
                        Id = ati.Id,
                        IncAtivo = ati.IncAtivo,
                        Nome = ati.Nome,
                        Descricao = ati.Descricao,
                        DataHoraCadastro = ati.DataHoraCadastro,
                        DataHoraUltimaAtualizacao = ati.DataHoraUltimaAtualizacao,
                        StatusAtivo = ati.StatusAtivo.Value,
                        CriticidadeAtivo = ati.CriticidadeAtivo.Value,
                        DescricaoStatusAtivo = result.descricaoStatusAtivoEnum(ati.StatusAtivo.Value.ToString()),
                        DescricaoCriticidadeAtivo = result.descricaoCriticidadeAtivoEnum(ati.CriticidadeAtivo.Value.ToString()),
                        SetorId = Guid.Parse(ati.SetorId.ToString()),
                        DescricaoSetor = ati.Setor.IncSetor.ToString() + " - " + ati.Setor.Nome,
                    }).SingleAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<AtivoViewDto>> PesquisarAtivosPorSetorAsync(Guid setorId)
        {
            try
            {
                AtivoViewDto aux = new AtivoViewDto();
                List<AtivoViewDto> listaResult = await _context.Ativos
                    .Where(ati => ati.SetorId.Equals(setorId))
                    .Include(ativ => ativ.SetorId)
                    .DefaultIfEmpty()
                    .Select(ati => new AtivoViewDto
                    {
                        Id = ati.Id,
                        IncAtivo = ati.IncAtivo,
                        Nome = ati.Nome,
                        Descricao = ati.Descricao,
                        DataHoraCadastro = ati.DataHoraCadastro,
                        DataHoraUltimaAtualizacao = ati.DataHoraUltimaAtualizacao,
                        StatusAtivo = ati.StatusAtivo.Value,
                        CriticidadeAtivo = ati.CriticidadeAtivo.Value,
                        DescricaoStatusAtivo = aux.descricaoStatusAtivoEnum(ati.StatusAtivo.Value.ToString()),
                        DescricaoCriticidadeAtivo = aux.descricaoCriticidadeAtivoEnum(ati.CriticidadeAtivo.Value.ToString()),
                        SetorId = Guid.Parse(ati.SetorId.ToString()),
                        DescricaoSetor = ati.Setor.IncSetor.ToString() + " - " + ati.Setor.Nome,
                    }).ToListAsync();
                return listaResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<AtivoPaginacaoViewDto> PesquisarAtivosPorFiltrosPaginacaoAsync(AtivoFiltroDto ativoFiltroDto)
        {
            try
            {
                AtivoPaginacaoViewDto ativoResultPaginacaoDto = new AtivoPaginacaoViewDto();

                ativoFiltroDto.RegistroInicial = ativoFiltroDto.RegistroInicial <= 1 ? 0 : ativoFiltroDto.RegistroInicial - 1;

                string query = @"SELECT SQL_CALC_FOUND_ROWS DISTINCT ati.Id, ati.Inc_Ativo, ati.Nome, ati.Criticidade, ati.Status_Ativo, ati.Setor_Id, CONCAT(seto.Inc_Setor, ' - ', seto.Nome) as 'Descricao_Setor' FROM Ativo AS ati";

                query = query + $" INNER JOIN setor AS seto ON ati.Setor_Id = seto.Id";

                query = query + " WHERE 1 = 1";

                if (!string.IsNullOrEmpty(ativoFiltroDto.IncAtivo))
                {
                    query = query + $" AND ati.Inc_Ativo = {int.Parse(ativoFiltroDto.IncAtivo)}";
                }

                if (!string.IsNullOrEmpty(ativoFiltroDto.Nome))
                {
                    query = query + $" AND ati.Nome LIKE '%{ativoFiltroDto.Nome}%'";
                }

                if (!string.IsNullOrEmpty(ativoFiltroDto.DataHoraCadastroInicio) && !string.IsNullOrEmpty(ativoFiltroDto.DataHoraCadastroFim))
                {
                    query = query + $" AND ati.Data_Hora_Cadastro >= '{ativoFiltroDto.DataHoraCadastroInicio}'";

                    query = query + $" AND ati.Data_Hora_Cadastro <= '{ativoFiltroDto.DataHoraCadastroFim}'";
                }

                if (!string.IsNullOrEmpty(ativoFiltroDto.DataHoraUltimaAtualizacaoInicio) && !string.IsNullOrEmpty(ativoFiltroDto.DataHoraUltimaAtualizacaoFim))
                {
                    query = query + $" AND ati.Data_Hora_Ultima_Atualizacao >= '{ativoFiltroDto.DataHoraUltimaAtualizacaoInicio}'";

                    query = query + $" AND ati.Data_Hora_Ultima_Atualizacao <= '{ativoFiltroDto.DataHoraUltimaAtualizacaoFim}'";
                }

                if (!string.IsNullOrEmpty(ativoFiltroDto.CriticidadeAtivo))
                {
                    query = query + $" AND ati.Criticidade = {int.Parse(ativoFiltroDto.CriticidadeAtivo)}";
                }

                if (!string.IsNullOrEmpty(ativoFiltroDto.StatusAtivo))
                {
                    query = query + $" AND ati.Status_Ativo = {int.Parse(ativoFiltroDto.StatusAtivo)}";
                }

                if (!string.IsNullOrEmpty(ativoFiltroDto.SetorId))
                {
                    query = query + $" AND ati.Setor_Id = '{ativoFiltroDto.SetorId}'";
                }

                query = query + " ORDER BY ati.Nome";

                // LIMIT RegistroInicial,QtdRegistroPorPagina;
                query = query + $" LIMIT {ativoFiltroDto.RegistroInicial},{ativoFiltroDto.QtdRegistroPorPagina}";

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
                                // Registros.: AtivoResult
                                AtivoTabelaViewDto ativoTabelaViewDto = new AtivoTabelaViewDto();
                                ativoTabelaViewDto.Id = Guid.Parse(dataReader["Id"].ToString());
                                ativoTabelaViewDto.IncAtivo = int.Parse(dataReader["Inc_Ativo"].ToString());
                                ativoTabelaViewDto.Nome = dataReader["Nome"].ToString();
                                ativoTabelaViewDto.DescricaoAtivo = dataReader["Inc_Ativo"].ToString() + " - " + dataReader["Nome"].ToString();
                                ativoTabelaViewDto.CriticidadeAtivo = ativoTabelaViewDto.descricaoCriticidadeAtivoEnum(dataReader["Criticidade"].ToString());
                                ativoTabelaViewDto.StatusAtivo = ativoTabelaViewDto.descricaoStatusAtivoEnum(dataReader["Status_Ativo"].ToString());
                                ativoTabelaViewDto.SetorId = Guid.Parse(dataReader["Setor_Id"].ToString());
                                ativoTabelaViewDto.DescricaoSetor = dataReader["Descricao_Setor"].ToString();
                                ativoResultPaginacaoDto.listaAtivoTabelaViewDto.Add(ativoTabelaViewDto);
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
                                ativoResultPaginacaoDto.TotalRegistros = int.Parse(dataReader["totalRegistros"].ToString()); ;
                            }
                        }
                    }
                }

                ativoResultPaginacaoDto.preencherDadosPaginacao(ativoFiltroDto.QtdRegistroPorPagina, ativoFiltroDto.RegistroInicial);
                
                return ativoResultPaginacaoDto;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
