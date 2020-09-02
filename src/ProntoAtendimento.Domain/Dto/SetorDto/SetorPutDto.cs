namespace ProntoAtendimento.Domain.Dto.SetorDto
{
    public class SetorPutDto : BaseDto
    {
        public int IncSetor { get; set; }
        public string Nome { get; set; }
        public string Coordenacao { get; set; }
    }
}
