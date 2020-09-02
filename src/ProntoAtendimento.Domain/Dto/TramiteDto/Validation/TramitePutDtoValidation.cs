using FluentValidation;

namespace ProntoAtendimento.Domain.Dto.TramiteDto.Validation
{
    public class TramitePutDtoValidation : AbstractValidator<TramitePutDto>
    {
        public TramitePutDtoValidation()
        {
            RuleFor(tradto => tradto)
                .NotNull().WithMessage("Parâmetro {PropertyName} não pode ser nulo!");

            RuleFor(tradto => tradto.IncTramite)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode ser vazio!")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo!");

            RuleFor(tradto => tradto.Descricao)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode ser vazio!")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo!")
                .Length(2, 6000).WithMessage("O Campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres!");

            RuleFor(tradto => tradto.DataHora)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode ser vazio!")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo!");

            RuleFor(tradto => tradto.Solucao)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode ser vazio!")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo!")
                .Must(x => !x.Equals(true) || !x.Equals(false)).WithMessage("O campo {PropertyName} é inválido!");

            RuleFor(tradto => tradto.OcorrenciaId)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode ser vazio!")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo!");

            RuleFor(tradto => tradto.UsuarioId)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode ser vazio!")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo!");

        }
    }
}
