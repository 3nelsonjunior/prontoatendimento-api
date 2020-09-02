using FluentValidation;
using FluentValidation.Results;
using ProntoAtendimento.Domain.Dto;
using ProntoAtendimento.Domain.Dto.UsuarioDto;
using ProntoAtendimento.Service.Interface;
using ProntoAtendimento.Service.Notification;

namespace ProntoAtendimento.Service.Implementation
{
    public class BaseService
    {
        private readonly INotificador _notificador;

        protected BaseService(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notificar(error.ErrorMessage);
            }
        }

        protected void Notificar(string mensagem)
        {
            _notificador.AdicionarNotificacao(new Notificacao(mensagem));
        }

        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : BaseDto
        {
            var validator = validacao.Validate(entidade);

            if (validator.IsValid) return true;

            Notificar(validator);

            return false;
        }
    }
}
