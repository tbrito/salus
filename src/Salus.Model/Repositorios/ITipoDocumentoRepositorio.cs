using Salus.Model.Entidades;
using System.Collections.Generic;

namespace Salus.Model.Repositorios
{
    public interface ITipoDocumentoRepositorio : IRepositorio<TipoDocumento>
    {
        IList<TipoDocumento> ObterTodosClassificaveis(Usuario usuario);
    }
}