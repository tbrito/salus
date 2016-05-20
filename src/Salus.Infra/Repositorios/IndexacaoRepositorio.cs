namespace Salus.Infra.Repositorios
{
    using System.Collections.Generic;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;

    public class IndexacaoRepositorio : Repositorio<Indexacao>, IIndexacaoRepositorio
    {
        public void AtualizarValor(int id, string valor)
        {
            this.Sessao
                .CreateQuery("update Indexacao set Valor = :valor where Id = :id")
                .SetParameter("valor", valor)
                .SetParameter("id", id)
                .ExecuteUpdate();
        }

        public IList<Indexacao> ObterPorDocumento(Documento documento)
        {
            return this.Sessao.QueryOver<Indexacao>()
                .Fetch(x => x.Chave).Eager
                .Where(x => x.Documento.Id == documento.Id)
                .List();
        }
    }
}