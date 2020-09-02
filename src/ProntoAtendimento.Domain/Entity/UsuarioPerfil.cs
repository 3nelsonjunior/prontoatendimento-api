using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProntoAtendimento.Domain.Entity
{
    public class UsuarioPerfil : IdentityUserRole<Guid>
    {
        public Usuario Usuario { get; set; }
        public Perfil Perfil { get; set; }
    }
}
