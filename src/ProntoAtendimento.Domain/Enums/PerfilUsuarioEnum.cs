using System.ComponentModel;

namespace ProntoAtendimento.Domain.Enums

{
    public enum PerfilUsuarioEnum
    {
        [Description("")]
        ADMIN = 0,

        [Description("PA")]
        PA = 1,

        [Description("TI")]
        TI = 2,

        [Description("CONSULTA")]
        CONSULTA = 3,

        [Description("ADMIN_TI")]
        ADMIN_TI = 4,

        [Description("DEV")]
        DEV = 5,
    }
}
