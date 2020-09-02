using ProntoAtendimento.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProntoAtendimento.Domain.Dto.UsuarioDto
{
    public class UsuarioViewDto : BaseDto
    {
        public string Matricula { get; set; }
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public StatusUsuarioEnum StatusUsuario { get; set; }
        public string DescricaoStatusUsuario { get; set; }
        public DateTime DataHoraCadastro { get; set; }
        public List<string> ListaPerfis { get; set; }

        public UsuarioViewDto()
        {

        }
    }
}
