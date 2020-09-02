using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProntoAtendimento.Domain.Dto.PerfilDto
{
    public class UsuarioPerfilPutDto
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(8, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 8)]
        public string Matricula { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public List<string> ListaPerfis { get; set; }

        public UsuarioPerfilPutDto()
        {

        }
    }
}
