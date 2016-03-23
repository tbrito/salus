namespace Salus.Infra.Repositorios
{
    using System.Collections.Generic;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;

    public class ChaveRepositorio : Repositorio<Chave>, IChaveRepositorio
    {
        public IList<Chave> ObterPorTipoDocumentoId(int id)
        {
            return this.Sessao.QueryOver<Chave>()
                .Fetch(x => x.TipoDocumento).Eager
                .Fetch(x => x.TipoDocumento.Parent).Eager
                .Where(x => x.TipoDocumento.Id == id)
                .List();
        }
    }
}