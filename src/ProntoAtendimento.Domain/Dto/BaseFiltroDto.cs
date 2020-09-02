namespace ProntoAtendimento.Domain.Dto
{
    public abstract class BaseFiltroDto
    {
        public int RegistroInicial { get; set; }
        public int QtdRegistroPorPagina { get; set; }

        public BaseFiltroDto()
        {

        }
    }
}
