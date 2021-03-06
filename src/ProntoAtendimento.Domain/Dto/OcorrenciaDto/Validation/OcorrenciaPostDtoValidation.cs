﻿using FluentValidation;
using ProntoAtendimento.Domain.Dto.AtivoDto;
using System.Collections.Generic;

namespace ProntoAtendimento.Domain.Dto.OcorrenciaDto.Validation
{
    public class OcorrenciaPostDtoValidation : AbstractValidator<OcorrenciaPostDto>
    {
        public OcorrenciaPostDtoValidation()
        {
            RuleFor(odto => odto)
                .NotNull().WithMessage("Parâmetro {PropertyName} não pode ser nulo!");

            RuleFor(odto => odto.Titulo)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode ser vazio!")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo!")
                .Length(2, 300).WithMessage("O Campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres!");

            RuleFor(odto => odto.DataHoraInicio)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode ser vazio!")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo!");

            RuleFor(odto => odto.Acionamento)
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo!")
                .Must(x => !x.Equals(true) || !x.Equals(false)).WithMessage("O campo {PropertyName} é inválido!");

            RuleFor(odto => odto.Impacto)
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo!")
                .Must(x => !x.Equals(true) || !x.Equals(false)).WithMessage("O campo {PropertyName} é inválido!");

            RuleFor(odto => odto)
                .Must(odto => EhDescricaoImpactoValido(odto.Impacto, odto.DescricaoImpacto)).WithMessage("O campo {PropertyName} não é válido!");

            RuleFor(odto => odto.TurnoId)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode ser vazio!")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo!");

            RuleFor(odto => odto.UsuarioId)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode ser vazio!")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo!");

            RuleFor(odto => odto.Ativos)
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo!")
                .Must(EhListaAtivosValida).WithMessage("O campo {PropertyName} não é válido!");

        }

        private bool EhListaAtivosValida(ICollection<AtivoOcorrenciaPostDto> listaAtivoOcorrenciaPostDto)
        {
            if (listaAtivoOcorrenciaPostDto.Equals(null) ||
                listaAtivoOcorrenciaPostDto.Count == 0 ||
                listaAtivoOcorrenciaPostDto.Contains(null)
                ) return false;

            return true;
        }

        private bool EhDescricaoImpactoValido(bool impacto, string descricaoImpacto)
        {
            if (!impacto) return true;

            if (string.IsNullOrEmpty(descricaoImpacto))
            {
                return false;
            }

            return true;
        }
    }
}
