using FluentValidation;
using ProntoAtendimento.Domain.Enums;

namespace ProntoAtendimento.Domain.Dto.AtivoDto.Validation
{
    public class AtivoPostDtoValidation : AbstractValidator<AtivoPostDto>
    {
        public AtivoPostDtoValidation()
        {
            RuleFor(adto => adto)
                .NotNull().WithMessage("Parâmetro {PropertyName} não pode ser nulo!");

            RuleFor(adto => adto.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode ser vazio!")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo!")
                .Length(2, 480).WithMessage("O Campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres!");

            RuleFor(adto => adto.Descricao)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode ser vazio!")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo!")
                .Length(2, 6000).WithMessage("O Campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres!");

            RuleFor(adto => adto.CriticidadeAtivo)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode ser vazio!")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo!")
                .Must(EhCriticidadelValido).WithMessage("O campo {PropertyName} não é válido!");

            RuleFor(adto => adto.SetorId)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode ser vazio!")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo!");

        }

        private bool EhCriticidadelValido(CriticidadeAtivoEnum criticidadeEnum)
        {
            if (criticidadeEnum == CriticidadeAtivoEnum.ALTO ||
                criticidadeEnum == CriticidadeAtivoEnum.BAIXO ||
                criticidadeEnum == CriticidadeAtivoEnum.CRITICO ||
                criticidadeEnum == CriticidadeAtivoEnum.MEDIO ||
                criticidadeEnum == CriticidadeAtivoEnum.NAO_SE_APLICA ||
                criticidadeEnum == CriticidadeAtivoEnum.NENHUM
                ) return true;

            return false;
        }
    }
}
