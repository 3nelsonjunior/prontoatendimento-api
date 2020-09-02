using FluentValidation;
using ProntoAtendimento.Domain.Enums;
using System;

namespace ProntoAtendimento.Domain.Dto.UsuarioDto.Validation
{
    public class UsuarioPutDtoValidation : AbstractValidator<UsuarioPutDto>
    {
        public UsuarioPutDtoValidation()
        {
            RuleFor(udto => udto)
                .NotNull().WithMessage("Parâmetro {PropertyName} não pode ser nulo!");

            RuleFor(udto => udto.Id.ToString())
                .NotEmpty().WithMessage("O campo {PropertyName} não pode ser vazio!")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo!")
                .Length(36).WithMessage("O Campo {PropertyName} precisa ter 36 caracteres!");

            RuleFor(udto => udto.Matricula)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode ser vazio!")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo!")
                .Length(8).WithMessage("O Campo {PropertyName} precisa ter 8 caracteres!");

            RuleFor(udto => udto.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode ser vazio!")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo!")
                .Length(2, 120).WithMessage("O Campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres!");

            RuleFor(udto => udto.Email)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode ser vazio!")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo!")
                .EmailAddress().WithMessage("O Campo {PropertyName} precisa ser válido!")
                .Length(5, 120).WithMessage("O Campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres!");


            RuleFor(udto => udto.StatusUsuario)
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo!")
                .Must(EhStatusValido).WithMessage("O campo {PropertyName} não é válido!");


        }

        private bool EhStatusValido(StatusUsuarioEnum statusEnum)
        {
            if (statusEnum == StatusUsuarioEnum.ATIVO ||
                statusEnum == StatusUsuarioEnum.BLOQUEADO ||
                statusEnum == StatusUsuarioEnum.INATIVO
                ) return true;
            else
            {
                return false;
            }
        }

    }

}
