namespace ProntoAtendimento.Domain.Dto.AtivoDto
{
    public class AtivoFiltroDto : BaseFiltroDto
    {
        public string IncAtivo { get; set; }
        public string Nome { get; set; }
        public string DataHoraCadastroInicio { get; set; }
        public string DataHoraCadastroFim { get; set; }
        public string DataHoraUltimaAtualizacaoInicio { get; set; }
        public string DataHoraUltimaAtualizacaoFim { get; set; }
        public string CriticidadeAtivo { get; set; }
        public string StatusAtivo { get; set; }
        public string SetorId { get; set; }
    }
}
