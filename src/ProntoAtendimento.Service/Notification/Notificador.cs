using ProntoAtendimento.Service.Interface;
using ProntoAtendimento.Service.Notification;
using System.Collections.Generic;
using System.Linq;

namespace ProntoAtendimento.Domain.Notification
{
    public class Notificador: INotificador
    {
        private List<Notificacao> _listaNotificacoes;

        public Notificador()
        {
            _listaNotificacoes = new List<Notificacao>();
        }

        public void AdicionarNotificacao(Notificacao notificacao)
        {
            _listaNotificacoes.Add(notificacao);
        }

        public List<Notificacao> ObterNotificacoes()
        {
            return _listaNotificacoes;
        }

        public bool ExisteNotificacao()
        {
            return _listaNotificacoes.Any();
        }
    }
}
