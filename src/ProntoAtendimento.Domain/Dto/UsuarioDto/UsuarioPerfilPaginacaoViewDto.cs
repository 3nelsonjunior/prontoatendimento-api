
using System.Collections.Generic;

namespace ProntoAtendimento.Domain.Dto.UsuarioDto
{
    public class UsuarioPerfilPaginacaoViewDto : BasePaginacaoDto
    {
        public List<UsuarioPerfilTabelaViewDto> ListaUsuarioPerfilTabelaViewDto { get; set; }

        public UsuarioPerfilPaginacaoViewDto()
        {
            ListaUsuarioPerfilTabelaViewDto = new List<UsuarioPerfilTabelaViewDto>();
        }
    }
}
