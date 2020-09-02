using ProntoAtendimento.Domain.Entity;
using ProntoAtendimento.Domain.Enums;
using System;

namespace ProntoAtendimento.Domain.Dto.UsuarioDto
{
    public class UsuarioResultDto
    {
        public string Id { get; set; }
        public string Matricula { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string PerfilUsuario { get; set; }
        public string StatusUsuario { get; set; }
        public string DataHoraCadastro { get; set; }
        public string DescricaoUsuario { get; set; }


        public UsuarioResultDto()
        {
        }
    }
}
