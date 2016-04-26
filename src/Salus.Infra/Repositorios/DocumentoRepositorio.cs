using System;
using System.Collections.Generic;
using Salus.Model.Entidades;
using Salus.Model.Repositorios;
using NHibernate.Transform;

namespace Salus.Infra.Repositorios
{
    public class DocumentoRepositorio : Repositorio<Documento>, IDocumentoRepositorio
    {
        public void AlterStatus(int id, SearchStatus searchStatus)
        {
            this.Sessao
             .CreateQuery("update Documento set SearchStatus = :searchStatus where Id = :id")
             .SetParameter("searchStatus", searchStatus)
             .SetParameter("id", id)
             .ExecuteUpdate();
        }

        public IList<Documento> ObterPorIdsComTipoDocumentoEIndexacoes(int[] currentPageIds)
        {
            return this.Sessao.QueryOver<Documento>()
                .Fetch(x => x.TipoDocumento).Eager
                .Fetch(x => x.TipoDocumento.Parent).Eager
                .Fetch(x => x.Usuario).Eager
                .Fetch(x => x.Indexacao).Eager
                .WhereRestrictionOn(x => x.Id).IsIn(currentPageIds)
                .TransformUsing(Transformers.DistinctRootEntity)
                .List();
        }

        public IList<Documento> ObterTodosParaIndexar(int quantidade)
        {
            return this.Sessao.QueryOver<Documento>()
                .Fetch(x => x.TipoDocumento).Eager
                .Fetch(x => x.Usuario).Eager
                .Fetch(x => x.Indexacao).Eager
                .Where(x => x.SearchStatus == SearchStatus.ToIndex)
                .TransformUsing(Transformers.DistinctRootEntity)
                .List();
        }
    }
}
