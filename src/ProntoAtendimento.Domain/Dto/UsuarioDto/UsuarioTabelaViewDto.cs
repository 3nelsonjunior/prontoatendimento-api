using System;
using System.Collections.Generic;
using System.Text;

namespace ProntoAtendimento.Domain.Dto.UsuarioDto
{
    public class UsuarioTabelaViewDto : BaseDto
    {
        public string Matricula { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string StatusUsuario { get; set; }
        public List<string> ListaPerfis { get; set; }

        public UsuarioTabelaViewDto()
        {

        }
    }
}
