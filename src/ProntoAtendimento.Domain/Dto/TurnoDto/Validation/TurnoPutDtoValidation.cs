using FluentValidation;

namespace ProntoAtendimento.Domain.Dto.TurnoDto.Validation
{
    public class TurnoPutDtoValidation : AbstractValidator<TurnoPutDto>
    {
        public TurnoPutDtoValidation()
        {
            RuleFor(tdto => tdto)
                .NotNull().WithMessage("Parâmetro {PropertyName} não pode ser nulo!");

            RuleFor(tdto => tdto.IncTurno)
                .NotNull().WithMessage("Parâmetro {PropertyName} não pode ser nulo!")
                .NotEmpty().WithMessage("O campo {PropertyName} não pode ser vazio!");

            RuleFor(tdto => tdto.DataHoraInicio)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode ser vazio!")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo!")
                .LessThan(tdto => tdto.DataHoraFim).WithMessage("O campo {PropertyName} não pode ser maior que a Data/Hora fim!");

            RuleFor(tdto => tdto.DataHoraFim)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode ser vazio!")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo!")
                .GreaterThan(tdto => tdto.DataHoraInicio).WithMessage("O campo {PropertyName} não pode ser menor que a Data/Hora inicío!");

            RuleFor(tdto => tdto.UsuarioId)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode ser vazio!")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo!");

        }
    }
}
