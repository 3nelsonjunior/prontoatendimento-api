using System.ComponentModel.DataAnnotations;

namespace ProntoAtendimento.Domain.Identity
{
    public class LoginDto
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(8, ErrorMessage = "O campo {0} precisa ter 8 caracteres", MinimumLength = 8)]
        public string Matricula { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(15, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Senha { get; set; }
    }
}
