namespace ProntoAtendimento.Domain.Dto.UsuarioDto

{
    public class UsuarioPostDto : BaseDto
    {
        public string Matricula { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public UsuarioPostDto()
        {

        }

    }
}
