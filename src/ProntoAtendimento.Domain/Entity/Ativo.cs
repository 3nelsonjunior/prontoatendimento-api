using ProntoAtendimento.Domain.Dto.AtivoDto;
using ProntoAtendimento.Domain.Enums;
using System;
using System.Collections.Generic;

namespace ProntoAtendimento.Domain.Entity
{
    public class Ativo : BaseEntity
    {
        public int IncAtivo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataHoraCadastro { get; set; }
        public DateTime DataHoraUltimaAtualizacao { get; set; }
        public StatusAtivoEnum? StatusAtivo { get; set; }
        public CriticidadeAtivoEnum? CriticidadeAtivo { get; set; }
        public Guid? SetorId { get; set; }
        public virtual Setor Setor { get; set; }
        public virtual ICollection<OcorrenciaAtivo> OcorrenciaAtivos { get; set; }

        public Ativo()
        {

        }

        public Ativo(AtivoPostDto ativoPostDto)
        {
            Id = ativoPostDto.Id;
            Nome = ativoPostDto.Nome;
            Descricao = ativoPostDto.Descricao;
            DataHoraCadastro = DateTime.Now;
            DataHoraUltimaAtualizacao = DateTime.Now;
            StatusAtivo = StatusAtivoEnum.ATIVO;
            CriticidadeAtivo = ativoPostDto.CriticidadeAtivo;
            SetorId = ativoPostDto.SetorId;
        }

        public Ativo(AtivoPutDto ativoPutDto, AtivoViewDto viewDto)
        {
            Id = viewDto.Id;
            IncAtivo = viewDto.IncAtivo;
            Nome = ativoPutDto.Nome;
            Descricao = ativoPutDto.Descricao;
            DataHoraCadastro = viewDto.DataHoraCadastro;
            DataHoraUltimaAtualizacao = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            StatusAtivo = ativoPutDto.StatusAtivo;
            CriticidadeAtivo = ativoPutDto.CriticidadeAtivo;
            SetorId = ativoPutDto.SetorId;
        }

    }
}
