using System.ComponentModel;

namespace ProntoAtendimento.Domain.Enums

{
    public enum StatusTurnoEnum
    {
        [Description("Aberto")]
        ABERTO = 0,

        [Description("Fechado")]
        FECHADO = 1,

        [Description("Bloqueado")]
        BLOQUEADO = 2,
    }
}
