using ProntoAtendimento.Domain.Dto.TramiteDto;
using System;


namespace ProntoAtendimento.Domain.Entity
{
    public class Tramite : BaseEntity
    {
        public int IncTramite { get; set; }
        public string Descricao { get; set; }
        public DateTime DataHora { get; set; }
        public bool Solucao { get; set; }
        public Guid OcorrenciaId { get; set; }
        public Guid UsuarioId { get; set; }
        public virtual Ocorrencia Ocorrencia { get; private set; }
        public virtual Usuario Usuario { get; private set; }

        public Tramite()
        {

        }

        public Tramite(TramitePostDto tramitePostDto)
        {
            Descricao = tramitePostDto.Descricao;
            DataHora = tramitePostDto.DataHora;
            Solucao = tramitePostDto.Solucao;
            OcorrenciaId = tramitePostDto.OcorrenciaId;
            OcorrenciaId = tramitePostDto.UsuarioId;
        }

        public Tramite(TramitePutDto tramitePutDto, TramiteResultDto tramiteResultDto)
        {
            Id = Guid.Parse(tramiteResultDto.Id);
            IncTramite = int.Parse(tramiteResultDto.IncTramite);
            Descricao = tramitePutDto.Descricao;
            DataHora = tramitePutDto.DataHora;
            Solucao = tramitePutDto.Solucao;
            OcorrenciaId = tramitePutDto.OcorrenciaId;
            UsuarioId = tramitePutDto.UsuarioId;
        }
    }
}
