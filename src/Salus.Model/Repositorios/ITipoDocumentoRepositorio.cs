using System.Collections.Generic;

namespace Salus.Model.Repositorios
{
    public interface ITipoDocumentoRepositorio
    {
        IList<TipoDocumento> ObterTodosClassificaveis(int userId);
    }
}