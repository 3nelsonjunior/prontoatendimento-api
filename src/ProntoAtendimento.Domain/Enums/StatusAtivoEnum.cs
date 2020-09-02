using System.ComponentModel;

namespace ProntoAtendimento.Domain.Enums

{
    public enum StatusAtivoEnum
    {
        [Description("Inativo")]
        INATIVO = 0,

        [Description("Ativo")]
        ATIVO = 1,

        [Description("Bloquedo")]
        BLOQUEADO = 2,
    }
}
