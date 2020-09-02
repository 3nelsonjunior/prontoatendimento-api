using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProntoAtendimento.API.Shared.JWT;
using ProntoAtendimento.Domain.Entity;
using ProntoAtendimento.Domain.Enums;
using ProntoAtendimento.Domain.Identity;
using ProntoAtendimento.Service.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProntoAtendimento.API.Controllers
{
    [Route("api/autenticacao")]
    [ApiController]
    public class AutenticacaoController : BasicController
    {
        private readonly AppSettings _appSettings;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;

        
        public AutenticacaoController(  INotificador notificador,
                                        SignInManager<Usuario> signInManager,
                                        UserManager<Usuario> userManager,
                                        IOptions<AppSettings> appSettings) : base(notificador)
        {
            _appSettings = appSettings.Value;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(loginDto.Matricula, loginDto.Senha, false, true);

            if (result.Succeeded)
            {
                var usuario = await _userManager.FindByNameAsync(loginDto.Matricula);

                if(usuario.StatusUsuario == StatusUsuarioEnum.BLOQUEADO || await _userManager.IsLockedOutAsync(usuario))
                {
                    NotificarErro("Usuário encontra-se bloqueado!");
                    return CustomResponse(loginDto);
                }

                if (usuario.StatusUsuario == StatusUsuarioEnum.INATIVO)
                {
                    NotificarErro("Usuário encontra-se desativado!");
                    return CustomResponse(loginDto);
                }

                return CustomResponse(await GerarJwt(usuario));
            }

            if (result.IsLockedOut)
            {
                var usuario = await _userManager.FindByNameAsync(loginDto.Matricula);

                usuario.StatusUsuario = StatusUsuarioEnum.BLOQUEADO;

                await _userManager.UpdateAsync(usuario);

                NotificarErro("Usuário temporareamente bloqueado por tentativas inválidas!");
                return CustomResponse(loginDto);
            }

            NotificarErro("Usuário ou Senha incorretos!");
            return CustomResponse(loginDto);
        }

        

        [HttpPost]
        [Route("logout")]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            
            return CustomResponse("Até mais!");
        }


        private async Task<string> GerarJwt(Usuario usuario)
        {
            // perfis
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.UserName),
                new Claim(ClaimTypes.GivenName, usuario.NomeCompleto),
                new Claim(ClaimTypes.Email, usuario.Email)
            };

            var roles = await _userManager.GetRolesAsync(usuario);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            // token
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_appSettings.PalavraChave);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExperacaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token); // transformar token compativel com web

            return encodedToken;

        }
        
    }


}
