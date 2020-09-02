using ProntoAtendimento.Domain.Entity;
using ProntoAtendimento.Domain.Enums;
using System;


namespace ProntoAtendimento.Domain.Dto.UsuarioDto
{
    public class UsuarioFiltroDto : BaseFiltroDto
    {
        public string Matricula { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string StatusUsuario { get; set; }
        public string DataHoraCadastroInicio { get; set; }
        public string DataHoraCadastroFim { get; set; }

        public UsuarioFiltroDto()
        {

        }

    }
}
