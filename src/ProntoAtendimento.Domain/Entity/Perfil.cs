using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace ProntoAtendimento.Domain.Entity
{
    public class Perfil : IdentityRole<Guid>
    {
        public List<UsuarioPerfil> ListaUsuarioPerfil { get; set; }
    }
}
