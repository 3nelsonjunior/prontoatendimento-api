using FluentValidation;

namespace ProntoAtendimento.Domain.Dto.SetorDto.Validation
{
    public class SetorPutDtoValidation : AbstractValidator<SetorPutDto>
    {
        public SetorPutDtoValidation()
        {
            RuleFor(sdto => sdto)
                .NotNull().WithMessage("Parâmetro {PropertyName} não pode ser nulo!");

            RuleFor(sdto => sdto.Nome)
                .NotNull().WithMessage("Parâmetro {PropertyName} não pode ser nulo!")
                .NotEmpty().WithMessage("O campo {PropertyName} não pode ser nulo!")
                .Length(2, 480).WithMessage("O Campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres!");

            RuleFor(sdto => sdto.Coordenacao)
                .NotNull().WithMessage("Parâmetro {PropertyName} não pode ser nulo!")
                .NotEmpty().WithMessage("O campo {PropertyName} não pode ser nulo!")
                .Length(2, 480).WithMessage("O Campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres!");
        }
    }
}
