using ProntoAtendimento.Service.Notification;
using System.Collections.Generic;

namespace ProntoAtendimento.Service.Interface
{
    public interface INotificador
    {
        List<Notificacao> ObterNotificacoes();
        void AdicionarNotificacao(Notificacao notificacao);
        bool ExisteNotificacao();
    }
}
