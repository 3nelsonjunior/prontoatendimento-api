﻿using FluentValidation;

namespace ProntoAtendimento.Domain.Dto.UsuarioDto.Validation
{
    public class UsuarioPostDtoValidation : AbstractValidator<UsuarioPostDto>
    {
        public UsuarioPostDtoValidation()
        {
            RuleFor(udto => udto)
                .NotNull().WithMessage("Parâmetro {PropertyName} não pode ser nulo!");

            RuleFor(udto => udto.Matricula)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode ser vazio!")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo!")
                .Length(8, 8).WithMessage("O Campo {PropertyName} precisa ter 8 caracteres!");

            RuleFor(udto => udto.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode ser vazio!")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo!")
                .Length(2, 120).WithMessage("O Campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres!");

            RuleFor(udto => udto.Email)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode ser vazio!")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo!")
                .EmailAddress().WithMessage("O Campo {PropertyName} precisa ser válido!")
                .Length(5, 120).WithMessage("O Campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres!");
            
            RuleFor(udto => udto.Senha)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode ser vazio!")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo!")
                .Length(8, 15).WithMessage("O Campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres!");
        }
    }
}
