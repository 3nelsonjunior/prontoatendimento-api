using FluentValidation;
using ProntoAtendimento.Domain.Enums;

namespace ProntoAtendimento.Domain.Dto.AtivoDto.Validation
{
    public class AtivoPutDtoValidation : AbstractValidator<AtivoPutDto>
    {
        public AtivoPutDtoValidation()
        {
            RuleFor(adto => adto)
                .NotNull().WithMessage("Parâmetro {PropertyName} não pode ser nulo!");

            RuleFor(adto => adto.IncAtivo)
                .NotNull().WithMessage("Parâmetro {PropertyName} não pode ser nulo!")
                .NotEmpty().WithMessage("O campo {PropertyName} não pode ser vazio!");

            RuleFor(adto => adto.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode ser vazio!")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo!")
                .Length(2, 480).WithMessage("O Campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres!");

            RuleFor(adto => adto.Descricao)
                .NotEmpty().WithMessage("O campo {PropertyName} não pode ser vazio!")
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo!")
                .Length(2, 6000).WithMessage("O Campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres!");

            RuleFor(adto => adto.StatusAtivo)
                .NotNull().WithMessage("O campo {PropertyName} não pode ser nulo!")
                .Must(EhStatusValido).WithMessage("O campo {PropertyName} não é válido!");

            RuleFor(adto => adto.CriticidadeAtivo)
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

        private bool EhStatusValido(StatusAtivoEnum statusEnum)
        {
            if (statusEnum == StatusAtivoEnum.ATIVO ||
                statusEnum == StatusAtivoEnum.BLOQUEADO ||
                statusEnum == StatusAtivoEnum.INATIVO
                ) return true;

            return false;
        }
    }
}
