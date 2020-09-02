using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace ProntoAtendimento.Domain.Enums
{
    // baseado em.: https://github.com/DesenvolvedorNinja/enum-com-descricao
    public static class UtilEnum
    {
        public static string ObterDescricaoEnum(this Enum valorEnum)
        {
            Type tipo = valorEnum.GetType();
            MemberInfo membro = tipo.GetMembers().Where(item => item.Name == Enum.GetName(tipo, valorEnum)).FirstOrDefault();
            var atributo = membro?.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
            return atributo?.Description != null ? atributo.Description : valorEnum.ToString();
        }

        public static T ObterValorEnum<T>(this string descricao)
        {
            var tipo = typeof(T);
            
            if (!tipo.GetTypeInfo().IsEnum) throw new ArgumentException();

            var campo = tipo.GetFields()
                .SelectMany(cam => cam.GetCustomAttributes(typeof(DescriptionAttribute), false), (cam, atri) => new { Field = cam, Att = atri })
                .Where(atri => ((DescriptionAttribute)atri.Att).Description == descricao).SingleOrDefault();

            return campo == null ? default(T) : (T)campo.Field.GetRawConstantValue();
        }
    }
}
