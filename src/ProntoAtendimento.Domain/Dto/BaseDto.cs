using ProntoAtendimento.Domain.Enums;
using System;

namespace ProntoAtendimento.Domain.Dto
{
    public abstract class BaseDto
    {
        public Guid Id { get; set; }

        public BaseDto()
        {
            Id = Guid.NewGuid();
        }

        public BaseDto(Guid valorGuid)
        {
            Id = valorGuid;
        }

        public string descricaoCriticidadeAtivoEnum(string valor)
        {
            var enumConvertido = (CriticidadeAtivoEnum)Enum.Parse(typeof(CriticidadeAtivoEnum), valor);

            return enumConvertido.ObterDescricaoEnum();
        }

        public string descricaoStatusAtivoEnum(string valor)
        {
            var enumConvertido = (StatusAtivoEnum)Enum.Parse(typeof(StatusAtivoEnum), valor);

            return enumConvertido.ObterDescricaoEnum();
        }

        public string descricaoPerfilUsuarioEnum(string valor)
        {
            var enumConvertido = (PerfilUsuarioEnum)Enum.Parse(typeof(PerfilUsuarioEnum), valor);

            return enumConvertido.ObterDescricaoEnum();
        }

        public string descricaoStatusTurnoEnum(string valor)
        {
            var enumConvertido = (StatusTurnoEnum)Enum.Parse(typeof(StatusTurnoEnum), valor);

            return enumConvertido.ObterDescricaoEnum();
        }

        public string descricaoStatusUsuarioEnum(string valor)
        {
            var enumConvertido = (StatusUsuarioEnum)Enum.Parse(typeof(StatusUsuarioEnum), valor);

            return enumConvertido.ObterDescricaoEnum();
        }

    }
}
