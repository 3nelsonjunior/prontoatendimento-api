using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProntoAtendimento.Domain.Dto.UsuarioDto;
using ProntoAtendimento.Domain.Entity;
using ProntoAtendimento.Repository.Context;
using ProntoAtendimento.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProntoAtendimento.Repository.Implementation
{
    public class UsuarioRepository :  IUsuarioRepository
    {
        private readonly EntityContext _context;
        private readonly RoleManager<Perfil> _roleManager;
        private readonly UserManager<Usuario> _userManager;

        public UsuarioRepository(   EntityContext context,
                                    RoleManager<Perfil> roleManager,
                                    UserManager<Usuario> userManager) 
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IdentityResult> CadastrarUsuarioAsync(Usuario usuario, string senha)
        {
            try
            {
                var result = await _userManager.CreateAsync(usuario, senha);
                if (result.Succeeded)
                {
                    usuario = await _userManager.FindByNameAsync(usuario.UserName);
                    var perfil = "CONSULTA";
                    await _userManager.AddToRoleAsync(usuario, perfil);
                }
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IdentityResult> EditarUsuarioAsync(Usuario usuario)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(usuario.UserName);

                user.UserName = usuario.UserName;
                user.NomeCompleto = usuario.NomeCompleto;
                user.Email = usuario.Email;
                user.StatusUsuario = usuario.StatusUsuario;
                user.LockoutEnd = usuario.LockoutEnd;

                return await _userManager.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IdentityResult> AlterarSenhaUsuarioAsync(Usuario usuario, string senha)
        {
            try
            {
                var result = await _userManager.RemovePasswordAsync(usuario);
                
                if (result.Succeeded)
                {
                    return await _userManager.AddPasswordAsync(usuario, senha);
                }
                else
                    return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IdentityResult> ExcluirUsuarioAsync(Guid usuarioId)
        {
            try
            {
                var result = await _userManager.FindByIdAsync(usuarioId.ToString());

                return await _userManager.DeleteAsync(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        

        public  async Task<UsuarioViewDto> PesquisarUsuarioPorIdAsync(Guid usuarioId)
        {
            try
            {
                UsuarioViewDto viewDto = new UsuarioViewDto();
                viewDto = await _context.Users
                    .Include(p => p.ListaUsuarioPerfil)
                    .Where(mat => mat.Id.Equals(usuarioId))
                    .Select(item => new UsuarioViewDto
                    {
                        Id = item.Id,
                        Matricula = item.UserName,
                        NomeCompleto = item.NomeCompleto,
                        Email = item.Email,
                        StatusUsuario = item.StatusUsuario,
                        DescricaoStatusUsuario = viewDto.descricaoStatusUsuarioEnum(item.StatusUsuario.ToString()),
                        DataHoraCadastro = item.DataHoraCadastro,
                        ListaPerfis = item.ListaUsuarioPerfil.Select(x => x.Perfil.Name).ToList(),
                    }).SingleAsync();
                return viewDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UsuarioViewDto> PesquisarUsuarioPorMatriculaAsync(string matricula)
        {
            try
            {
                UsuarioViewDto viewDto = new UsuarioViewDto();
                viewDto = await _context.Users
                    .Include(p => p.ListaUsuarioPerfil)
                    .Where(mat => mat.UserName.Equals(matricula))
                    .Select(item => new UsuarioViewDto
                    {
                        Id = item.Id,
                        Matricula = item.UserName,
                        NomeCompleto = item.NomeCompleto,
                        Email = item.Email,
                        StatusUsuario = item.StatusUsuario,
                        DescricaoStatusUsuario = viewDto.descricaoStatusUsuarioEnum(item.StatusUsuario.ToString()),
                        DataHoraCadastro = item.DataHoraCadastro,
                        ListaPerfis = item.ListaUsuarioPerfil.Select(x => x.Perfil.Name).ToList(),
                    }).SingleAsync();
                return viewDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<UsuarioTabelaViewDto>> PesquisarTodosUsuariosAsync()
        {
            try
            {
                var auxDto = new UsuarioTabelaViewDto();
                var listaViewDto = await _context.Users
                    .Include(p => p.ListaUsuarioPerfil)
                    .OrderBy(u => u.NomeCompleto)
                    .Select(item => new UsuarioTabelaViewDto
                    {
                        Id = item.Id,
                        Matricula = item.UserName,
                        Nome = item.NomeCompleto,
                        Email = item.Email,
                        StatusUsuario = auxDto.descricaoStatusUsuarioEnum(item.StatusUsuario.ToString()),
                        ListaPerfis = item.ListaUsuarioPerfil.OrderBy(x => x.Perfil.Name).Select(x => x.Perfil.Name).ToList(),
                    }).ToListAsync();
                return listaViewDto;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<UsuarioPerfilPaSelectboxViewDto>> PesquisarUsuariosPerfilPaParaSelectboxAsync()
        {
            try
            {
                List<UsuarioPerfilPaSelectboxViewDto> listaViewDto = new List<UsuarioPerfilPaSelectboxViewDto>();

                string query = "SELECT 	DISTINCT ";

                query = query + "usu.Id, " +
                                "CONCAT(usu.UserName, ' - ', usu.NomeCompleto) as 'Descricao_Usuario' ";

                query = query + "FROM usuario AS usu ";

                query = query + "INNER JOIN usuarioperfil AS up ON  up.UserId =  usu.Id ";

                query = query + "INNER JOIN perfil AS per ON per.Id = up.RoleId ";

                query = query + "WHERE per.Name = 'PA' ";

                query = query + "ORDER BY usu.NomeCompleto;";

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
                                UsuarioPerfilPaSelectboxViewDto viewDto = new UsuarioPerfilPaSelectboxViewDto();
                                viewDto.Id = Guid.Parse(dataReader["Id"].ToString());
                                viewDto.DescricaoUsuario = dataReader["Descricao_Usuario"].ToString();
                                listaViewDto.Add(viewDto);
                            }
                        }
                    }

                }
                    
                return listaViewDto;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UsuarioPaginacaoViewtDto> PesquisarUsuariosPorFiltrosPaginacaoAsync(UsuarioFiltroDto filtroDto)
        {
            try
            {
                UsuarioPaginacaoViewtDto paginacaoViewDto = new UsuarioPaginacaoViewtDto();

                filtroDto.RegistroInicial = filtroDto.RegistroInicial <= 1 ? 0 : filtroDto.RegistroInicial - 1;

                string query = @"SELECT SQL_CALC_FOUND_ROWS DISTINCT ";
                
                query = query + "usu.Id, usu.UserName, " +
                    "            usu.NomeCompleto, " +
                    "            usu.Email, " +
                    "            usu.StatusUsuario, ";

                query = query + "GROUP_CONCAT(  (SELECT per.Name FROM perfil AS per WHERE Id = up.RoleId) SEPARATOR ', ' ) AS 'ListaPerfis' ";

                query = query + "FROM Usuario AS usu ";

                query = query + "INNER JOIN Usuarioperfil AS up ON up.UserId = usu.Id ";

                // WHERE
                query = query + " WHERE 1 = 1 ";

                if (!string.IsNullOrEmpty(filtroDto.Matricula))
                {
                    query = query + $" AND usu.UserName = {filtroDto.Matricula}";
                }

                if (!string.IsNullOrEmpty(filtroDto.Nome))
                {
                    query = query + $" AND usu.NomeCompleto LIKE '%{filtroDto.Nome}%'";
                }

                if (!string.IsNullOrEmpty(filtroDto.StatusUsuario))
                {
                    query = query + $" AND usu.StatusUsuario = {int.Parse(filtroDto.StatusUsuario)}";
                }

                if (!string.IsNullOrEmpty(filtroDto.DataHoraCadastroInicio) && !string.IsNullOrEmpty(filtroDto.DataHoraCadastroFim))
                {
                    query = query + $" AND usu.DataHoraCadastro >= '{filtroDto.DataHoraCadastroInicio}'";

                    query = query + $" AND usu.DataHoraCadastro <= '{filtroDto.DataHoraCadastroFim}'";
                }

                query = query + "GROUP BY usu.UserName ";

                query = query + "ORDER BY  usu.NomeCompleto";

                // LIMIT RegistroInicial, QtdRegistroPorPagina;
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
                                // Registros
                                UsuarioTabelaViewDto viewDto = new UsuarioTabelaViewDto();
                                viewDto.Id = Guid.Parse(dataReader["Id"].ToString());
                                viewDto.Matricula = dataReader["UserName"].ToString();
                                viewDto.Nome = dataReader["NomeCompleto"].ToString();
                                viewDto.Email = dataReader["Email"].ToString();
                                viewDto.StatusUsuario = viewDto.descricaoStatusUsuarioEnum(dataReader["StatusUsuario"].ToString());
                                viewDto.ListaPerfis = dataReader["ListaPerfis"].ToString().Split(",").Select(item => item.Trim()).ToList();
                                paginacaoViewDto.ListaUsuarioTabelaViewDto.Add(viewDto);
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

        public async Task<UsuarioPerfilPaginacaoViewDto> PesquisarUsuariosPorFiltrosPaginacaoParaPaginaPerfilAsync(UsuarioPerfilFiltroDto filtroDto)
        {
            try
            {
                UsuarioPerfilPaginacaoViewDto paginacaoViewDto = new UsuarioPerfilPaginacaoViewDto();

                filtroDto.RegistroInicial = filtroDto.RegistroInicial <= 1 ? 0 : filtroDto.RegistroInicial - 1;

                string query = @"SELECT SQL_CALC_FOUND_ROWS DISTINCT ";

                query = query + "usu.Id, usu.UserName, " +
                    "            usu.NomeCompleto, " +
                    "            usu.Email, " +
                    "            usu.StatusUsuario, ";

                query = query + "GROUP_CONCAT(  (SELECT per.Name FROM perfil AS per WHERE Id = up.RoleId) SEPARATOR ', ' ) AS 'ListaPerfis' ";

                query = query + "FROM Usuario AS usu ";

                query = query + "INNER JOIN Usuarioperfil AS up ON up.UserId = usu.Id";

                // WHERE
                query = query + " WHERE 1 = 1 ";

                if (!string.IsNullOrEmpty(filtroDto.Matricula))
                {
                    query = query + $" AND usu.UserName = {filtroDto.Matricula}";
                }

                if (!string.IsNullOrEmpty(filtroDto.Nome))
                {
                    query = query + $" AND usu.NomeCompleto LIKE '%{filtroDto.Nome}%'";
                }

                if (!string.IsNullOrEmpty(filtroDto.StatusUsuario))
                {
                    query = query + $" AND usu.StatusUsuario = {int.Parse(filtroDto.StatusUsuario)}";
                }

                query = query + " GROUP BY usu.UserName";

                query = query + " ORDER BY  usu.NomeCompleto";

                // LIMIT RegistroInicial, QtdRegistroPorPagina;
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
                                // Registros
                                UsuarioPerfilTabelaViewDto viewDto = new UsuarioPerfilTabelaViewDto();
                                viewDto.Id = Guid.Parse(dataReader["Id"].ToString());
                                viewDto.Matricula = dataReader["UserName"].ToString();
                                viewDto.Nome = dataReader["NomeCompleto"].ToString();
                                viewDto.Email = dataReader["Email"].ToString();
                                viewDto.StatusUsuario = viewDto.descricaoStatusUsuarioEnum(dataReader["StatusUsuario"].ToString());
                                viewDto.ListaPerfis = dataReader["ListaPerfis"].ToString().Split(",").Select(item => item.Trim()).ToList();
                                paginacaoViewDto.ListaUsuarioPerfilTabelaViewDto.Add(viewDto);
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

        public void Dispose()
        {
            _context?.Dispose();
        }

    }

    
}
