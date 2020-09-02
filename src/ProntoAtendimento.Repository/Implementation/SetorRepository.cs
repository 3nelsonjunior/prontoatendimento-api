using Microsoft.EntityFrameworkCore;
using ProntoAtendimento.Domain.Dto.SetorDto;
using ProntoAtendimento.Domain.Entity;
using ProntoAtendimento.Repository.Context;
using ProntoAtendimento.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProntoAtendimento.Repository.Implementation
{
    public class SetorRepository : BaseRepository<Setor>, ISetorRepository
    {
        public SetorRepository(EntityContext context) : base(context)
        {

        }

        public async Task<SetorViewDto> PesquisarSetorPorIdAsync(Guid setorId)
        {
            try
            {
                SetorViewDto result = await _context.Setores
                    .Where(set => set.Id == setorId)
                    .DefaultIfEmpty()
                    .Select(set => new SetorViewDto
                    {
                        Id = set.Id,
                        IncSetor = set.IncSetor,
                        Nome = set.Nome,
                        Coordenacao = set.Coordenacao,
                        DescricaoSetor = set.IncSetor + " - "  + set.Nome,
                    }).SingleAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<SetorViewDto> PesquisarSetorPorIncAsync(int incSetor)
        {
            try
            {
                SetorViewDto result = await _context.Setores
                    .Where(set => set.IncSetor == incSetor)
                    .DefaultIfEmpty()
                    .Select(set => new SetorViewDto
                    {
                        Id = set.Id,
                        IncSetor = set.IncSetor,
                        Nome = set.Nome,
                        Coordenacao = set.Coordenacao,
                        DescricaoSetor = set.IncSetor + " - " + set.Nome,
                    }).SingleAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<SetorSelectboxViewDto>> PesquisarSetoresParaSelectboxAsync()
        {
            try
            {
                List<SetorSelectboxViewDto> listaViewDto = await _context.Setores
                    .DefaultIfEmpty()
                    .Select(set => new SetorSelectboxViewDto
                    {
                        Id = set.Id,
                        DescricaoSetor = set.IncSetor + " - " + set.Nome,
                    }).OrderBy(set => set.DescricaoSetor).ToListAsync();
                return listaViewDto;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<SetorPaginacaoViewtDto> PesquisarSetoresPorFiltrosPaginacaoAsync(SetorFiltroDto filtroDto)
        {
            try
            {
                SetorPaginacaoViewtDto paginacaoViewDto = new SetorPaginacaoViewtDto();

                filtroDto.RegistroInicial = filtroDto.RegistroInicial <= 1 ? 0 : filtroDto.RegistroInicial - 1;

                string query = @"SELECT SQL_CALC_FOUND_ROWS DISTINCT * FROM Setor AS seto WHERE 1 = 1";

                if (!string.IsNullOrEmpty(filtroDto.IncSetor))
                {
                    query = query + $" AND seto.Inc_Setor = {int.Parse(filtroDto.IncSetor)}"; 
                }
                if (!string.IsNullOrEmpty(filtroDto.Nome))
                {
                    query = query + $" AND seto.Nome LIKE '%{filtroDto.Nome}%'";
                }
                if (!string.IsNullOrEmpty(filtroDto.Coordenacao))
                {
                    query = query + $" AND seto.Coordenacao LIKE '%{filtroDto.Coordenacao}%'";
                }

                query = query + " ORDER BY seto.Nome";

                // LIMIT RegistroInicial,QtdRegistroPorPagina;
                query = query + $" LIMIT {filtroDto.RegistroInicial},{filtroDto.QtdRegistroPorPagina}";

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
                                // Registros.: SetoresViewDto[]
                                SetorViewDto viewDto = new SetorViewDto();
                                viewDto.Id = Guid.Parse(dataReader["Id"].ToString());
                                viewDto.IncSetor = int.Parse(dataReader["Inc_Setor"].ToString());
                                viewDto.Nome = dataReader["Nome"].ToString();
                                viewDto.Coordenacao = dataReader["Coordenacao"].ToString();
                                viewDto.DescricaoSetor = dataReader["Inc_Setor"].ToString() + " - " + dataReader["Nome"].ToString();
                                paginacaoViewDto.listaSetorViewDto.Add(viewDto);
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
                    connection.Close();
                }

                paginacaoViewDto.preencherDadosPaginacao(filtroDto.QtdRegistroPorPagina, filtroDto.RegistroInicial);

                return paginacaoViewDto;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<SetorViewDto>> PesquisarTodosSetoresAsync()
        {
            try
            {
                List<SetorViewDto> listaViewDto = await _context.Setores
                    .DefaultIfEmpty()
                    .Select(set => new SetorViewDto
                    {
                        Id = set.Id,
                        IncSetor = set.IncSetor,
                        Nome = set.Nome,
                        Coordenacao = set.Coordenacao,
                        DescricaoSetor = set.IncSetor + " - " + set.Nome,
                    }).OrderBy(set => set.Nome).ToListAsync();
                return listaViewDto;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
