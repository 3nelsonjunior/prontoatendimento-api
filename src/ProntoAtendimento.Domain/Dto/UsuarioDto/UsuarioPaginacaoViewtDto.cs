using System;
using System.Collections.Generic;
using System.Text;

namespace ProntoAtendimento.Domain.Dto.UsuarioDto
{
    public class UsuarioPaginacaoViewtDto : BasePaginacaoDto
    {
        public List<UsuarioTabelaViewDto> ListaUsuarioTabelaViewDto { get; set; }

        public UsuarioPaginacaoViewtDto()
        {
            ListaUsuarioTabelaViewDto = new List<UsuarioTabelaViewDto>();
        }
    }
}
