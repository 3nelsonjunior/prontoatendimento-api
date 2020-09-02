using ProntoAtendimento.Domain.Dto.OcorrenciaDto;
using ProntoAtendimento.Domain.Enums;
using System;
using System.Collections.Generic;

namespace ProntoAtendimento.Domain.Entity
{
    public class Ocorrencia: BaseEntity
    {
        public int IncOcorrencia { get; set; }
        public string Titulo { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime? DataHoraFim { get; set; }
        public DateTime DataHoraUltimaAtualizacao { get; set; }
        public string ChamadoTI { get; set; }
        public string ChamadoFornecedor { get; set; }
        public string OcorrenciaCCM { get; set; }
        public bool Acionamento { get; set; }
        public bool Impacto { get; set; }
        public string DescricaoImpacto { get; set; }
        public StatusOcorrenciaEnum StatusAtualOcorrencia { get; set; }
        public Guid UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }

        public virtual ICollection<Tramite> Tramites { get; set; }
        public virtual ICollection<TurnoOcorrencia> TurnoOcorrencias { get; set; } 
        public virtual ICollection<OcorrenciaAtivo> OcorrenciaAtivos { get; set; }

        public Ocorrencia()
        {

        }

        public Ocorrencia(OcorrenciaPostDto ocorrenciaPostDto)
        {
            Titulo = ocorrenciaPostDto.Titulo;
            DataHoraInicio = ocorrenciaPostDto.DataHoraInicio;
            DataHoraFim = ocorrenciaPostDto.DataHoraFim;
            DataHoraUltimaAtualizacao = DateTime.Now;
            ChamadoTI = ocorrenciaPostDto.ChamadoTI;
            ChamadoFornecedor = ocorrenciaPostDto.ChamadoFornecedor;
            ChamadoTI = ocorrenciaPostDto.OcorrenciaCCM;
            Acionamento = ocorrenciaPostDto.Acionamento;
            Impacto = ocorrenciaPostDto.Impacto;
            DescricaoImpacto = ocorrenciaPostDto.DescricaoImpacto;
            StatusAtualOcorrencia = StatusOcorrenciaEnum.ANDAMENTO;
            UsuarioId = ocorrenciaPostDto.UsuarioId;
        }

        public Ocorrencia(OcorrenciaPutDto ocorrenciaPutDto, OcorrenciaResultDto ocorrenciaResultDto)
        {
            Id = Guid.Parse(ocorrenciaResultDto.Id);
            IncOcorrencia = int.Parse(ocorrenciaResultDto.IncOcorrecia);
            Titulo = ocorrenciaPutDto.Titulo;
            DataHoraInicio = ocorrenciaPutDto.DataHoraInicio;
            DataHoraFim = ocorrenciaPutDto.DataHoraFim;
            DataHoraUltimaAtualizacao = DateTime.Now;
            ChamadoTI = ocorrenciaPutDto.ChamadoTI;
            ChamadoFornecedor = ocorrenciaPutDto.ChamadoFornecedor;
            ChamadoTI = ocorrenciaPutDto.OcorrenciaCCM;
            Acionamento = ocorrenciaPutDto.Acionamento;
            Impacto = ocorrenciaPutDto.Impacto;
            DescricaoImpacto = ocorrenciaPutDto.DescricaoImpacto;
            StatusAtualOcorrencia = ocorrenciaPutDto.StatusAtualOcorrencia;
            UsuarioId = ocorrenciaPutDto.UsuarioId;
        }
    }
}
