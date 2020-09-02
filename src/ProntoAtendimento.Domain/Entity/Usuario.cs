using Microsoft.AspNetCore.Identity;
using ProntoAtendimento.Domain.Dto.UsuarioDto;
using ProntoAtendimento.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProntoAtendimento.Domain.Entity
{
    public class Usuario :  IdentityUser<Guid>
    {
        [Column(TypeName = "nvarchar(120)")]
        public string NomeCompleto { get; set; }
        [Column(TypeName = "int(1)")]
        public StatusUsuarioEnum StatusUsuario { get; set; }
        [Column(TypeName = "DATETIME")]
        public DateTime DataHoraCadastro { get; set; }
        public List<UsuarioPerfil> ListaUsuarioPerfil { get; set; }

        public virtual ICollection<Turno> Turnos { get; set; }
        public virtual ICollection<Ocorrencia> Ocorrencias { get; set; }
        public virtual ICollection<Tramite> Tramites { get; set; }

        public Usuario()
        {
        }
        

        public Usuario(UsuarioPostDto postDto)
        {
            Id = postDto.Id;
            UserName = postDto.Matricula;
            NomeCompleto = postDto.Nome;
            Email = postDto.Email;
            StatusUsuario = StatusUsuarioEnum.ATIVO;
            EmailConfirmed = true;
            DataHoraCadastro = DateTime.Now;
        }

        public Usuario(UsuarioPutDto putDto, UsuarioViewDto viewDto)
        {
            Id = viewDto.Id;
            UserName = putDto.Matricula;
            NomeCompleto = putDto.Nome;
            Email = putDto.Email;
            StatusUsuario = putDto.StatusUsuario;
            EmailConfirmed = true;
            DataHoraCadastro = viewDto.DataHoraCadastro;

            if (viewDto.StatusUsuario == StatusUsuarioEnum.BLOQUEADO && (putDto.StatusUsuario == StatusUsuarioEnum.ATIVO || putDto.StatusUsuario == StatusUsuarioEnum.INATIVO))
            {
                LockoutEnd = null;
            }
        }



    }
}
