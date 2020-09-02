using System.ComponentModel;

namespace ProntoAtendimento.Domain.Enums

{
    public enum StatusUsuarioEnum
    {
        [Description("Inativo")]
        INATIVO = 0,

        [Description("Ativo")]
        ATIVO = 1,

        [Description("Bloqueado")]
        BLOQUEADO = 2,
    }
}
