namespace ProntoAtendimento.Domain.Dto
{
    public abstract class BasePaginacaoDto
    {
        public int TotalRegistros { get; set; }
        public int QtdRegistroPorPagina { get; set; }
        public int RegistroInicial { get; set; }
        public int RegistroFinal { get; set; }
        public int QtdPaginas { get; set; }
        public BasePaginacaoDto()
        {

        }

        
        public void preencherDadosPaginacao(int qtdRegistroPorPagina, int registroInicial)
        {
            // TotalRegistro já preenchido via query

            // Quantidade de Registro Por Página
            QtdRegistroPorPagina = qtdRegistroPorPagina;

            // Registro inicial
            RegistroInicial = registroInicial + 1;

            // Registro Final.: RegistroFinal
            RegistroFinal = (RegistroInicial + QtdRegistroPorPagina) > TotalRegistros ? TotalRegistros : (RegistroInicial - 1 + QtdRegistroPorPagina);

            // Quantidade de Paginas.: QuantidadePaginas
            QtdPaginas = TotalRegistros / QtdRegistroPorPagina;
            if ((TotalRegistros % QtdRegistroPorPagina) > 0)
            {
                QtdPaginas = QtdPaginas + 1;
            }

        }
    }
}
