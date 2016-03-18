namespace Salus.Infra.Repositorios
{
    using System.Collections.Generic;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;

    public class TipoDocumentoRepositorio : Repositorio<TipoDocumento>, ITipoDocumentoRepositorio
    {
        public IList<TipoDocumento> ObterTodosClassificaveis(Usuario usuario)
        {
            return this.Sessao.QueryOver<TipoDocumento>()
                .List();
        }
    }
}