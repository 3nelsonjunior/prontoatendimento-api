namespace ProntoAtendimento.Domain.Dto.SetorDto
{
    public class SetorViewDto : BaseDto
    {
        public int IncSetor { get; set; }
        public string Nome { get; set; }
        public string Coordenacao { get; set; }
        public string DescricaoSetor { get; set; }

        public SetorViewDto()
        {
        }

    }
}
