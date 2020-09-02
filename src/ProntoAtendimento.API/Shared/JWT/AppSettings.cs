namespace ProntoAtendimento.API.Shared.JWT
{
    public class AppSettings
    {
        public string PalavraChave { get; set; }
        public int ExperacaoHoras { get; set; }
        public string Emissor { get; set; } // issuer
        public string ValidoEm { get; set; } // audience
    }
}
