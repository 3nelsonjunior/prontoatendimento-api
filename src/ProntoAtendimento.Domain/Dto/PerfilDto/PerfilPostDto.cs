using System.ComponentModel.DataAnnotations;

namespace ProntoAtendimento.Domain.Dto.PerfilDto
{
    public class PerfilPostDto
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(15, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }

        public PerfilPostDto()
        {

        }
    }

}
