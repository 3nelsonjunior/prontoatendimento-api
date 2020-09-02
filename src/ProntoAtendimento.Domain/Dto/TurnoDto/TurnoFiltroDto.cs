namespace ProntoAtendimento.Domain.Dto.TurnoDto
{
    public class TurnoFiltroDto : BaseFiltroDto
    {
        public string DataHoraInicio { get; set; }
        public string DataHoraFim { get; set; }
        public string StatusTurno { get; set; }
        public string UsuarioId { get; set; }

        public TurnoFiltroDto()
        {

        }
    }
}
