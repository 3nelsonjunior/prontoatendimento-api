using ProntoAtendimento.Domain.Enums;
using System;

namespace ProntoAtendimento.Domain.Dto.UsuarioDto
{
    public class UsuarioPutDto : BaseDto
    {
        public string Matricula { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public PerfilUsuarioEnum PerfilUsuario { get; set; }
        public StatusUsuarioEnum StatusUsuario { get; set; }

        public UsuarioPutDto(Guid Id) : base(Id)
        {
        }
    }
}
