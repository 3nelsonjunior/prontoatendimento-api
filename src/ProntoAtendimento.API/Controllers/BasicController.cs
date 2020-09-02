using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ProntoAtendimento.Service.Interface;
using ProntoAtendimento.Service.Notification;
using System.Linq;

namespace ProntoAtendimento.API.Controllers
{
    [ApiController]
    public class BasicController : ControllerBase
    {
        private readonly INotificador _notificador;

        protected BasicController(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected bool OperacaoValida()
        {
            return !_notificador.ExisteNotificacao();
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if(OperacaoValida())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }
            else
            {
                return BadRequest(new
                {
                    success = false,
                    errors = _notificador.ObterNotificacoes().Select(n => n.Mensagem)
                });
            }
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotificarErroEntidadeInvalida(modelState);
            return CustomResponse();
        }

        protected void NotificarErroEntidadeInvalida(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach(var erro in erros)
            {
                var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotificarErro(errorMsg);
            }
        }

        protected void NotificarErro(string mensagem)
        {
            _notificador.AdicionarNotificacao(new Notificacao(mensagem));
        }
    }
}
