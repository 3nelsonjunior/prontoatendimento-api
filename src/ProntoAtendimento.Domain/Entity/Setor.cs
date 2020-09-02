using ProntoAtendimento.Domain.Dto.SetorDto;
using System.Collections.Generic;

namespace ProntoAtendimento.Domain.Entity
{
    public class Setor : BaseEntity
    {
        public int IncSetor { get; set; }
        public string Nome { get; set; }
        public string Coordenacao { get; set; }
        public virtual ICollection<Ativo> Ativos { get; set; }

        public Setor()
        {

        }

        public Setor(SetorPostDto setorPostDto)
        {
            Id = setorPostDto.Id;
            Nome = setorPostDto.Nome;
            Coordenacao = setorPostDto.Coordenacao;
        }

        public Setor(SetorPutDto setorPutDto, SetorViewDto viewDto)
        {
            Id = viewDto.Id;
            IncSetor = viewDto.IncSetor;
            Nome = setorPutDto.Nome;
            Coordenacao = setorPutDto.Coordenacao;
        }
    }
}
