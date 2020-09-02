namespace ProntoAtendimento.Domain.Dto.TramiteDto
{
    public class TramiteFiltroDto
    {
        public string Descricao { get; set; }
        public string DataHoraInicio { get; set; }
        public string DataHoraFim { get; set; }
        public string Solucao { get; set; }
        public string OcorrenciaId { get; set; }
        public string UsuarioId { get; set; }
    }
}
