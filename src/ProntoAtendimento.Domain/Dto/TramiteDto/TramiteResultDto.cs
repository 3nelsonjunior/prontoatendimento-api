namespace ProntoAtendimento.Domain.Dto.TramiteDto
{
    public class TramiteResultDto
    {
        public string Id { get; set; }
        public string IncTramite { get; set; }
        public string Descricao { get; set; }
        public string DataHora { get; set; }
        public string Solucao { get; set; }
        public string OcorrenciaId { get; set; }
        public string UsuarioId { get; set; }
        public string DescricaoOcorrencia { get; set; }
        public string DescricaoUsuario { get; set; }

        public TramiteResultDto()
        {

        }

    }
}
