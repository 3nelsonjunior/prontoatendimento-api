using System;

namespace ProntoAtendimento.Domain.Dto.TramiteDto
{
    public class TramitePutDto : BaseDto
    {
        public int IncTramite { get; set; }
        public string Descricao { get; set; }
        public DateTime DataHora { get; set; }
        public bool Solucao { get; set; }
        public Guid OcorrenciaId { get; set; }
        public Guid UsuarioId { get; set; }

        public TramitePutDto()
        {

        }

    }
}
