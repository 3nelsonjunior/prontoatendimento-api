using System.ComponentModel;

namespace ProntoAtendimento.Domain.Enums
{
    public enum CriticidadeAtivoEnum
    {
        [Description("Não se aplica")]
        NAO_SE_APLICA = 0,

        [Description("Nenhum")]
        NENHUM = 1,

        [Description("Baixo")]
        BAIXO = 2,

        [Description("Médio")]
        MEDIO = 3,

        [Description("Alto")]
        ALTO = 4,

        [Description("Crítico")]
        CRITICO = 5,
    }
}
