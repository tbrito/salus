namespace Salus.Infra.Repositorios
{
    using System.Collections.Generic;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;

    public class IndexacaoRepositorio : Repositorio<Indexacao>, IIndexacaoRepositorio
    {
        public IList<Indexacao> ObterPorDocumento(Documento documento)
        {
            return this.Sessao.QueryOver<Indexacao>()
                .Fetch(x => x.Chave).Eager
                .Where(x => x.Documento.Id == documento.Id)
                .List();
        }
    }
}