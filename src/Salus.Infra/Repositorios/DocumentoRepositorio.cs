﻿using System;
using System.Collections.Generic;
using Salus.Model.Entidades;
using Salus.Model.Repositorios;
using NHibernate.Transform;
using System.Linq;

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

        public void Bloquear(int id)
        {
            this.Sessao
             .CreateQuery("update Documento set Bloqueado = true where Id = :id")
             .SetParameter("id", id)
             .ExecuteUpdate();
        }

        public void Desbloquear(int id)
        {
            this.Sessao
             .CreateQuery("update Documento set Bloqueado = false where Id = :id")
             .SetParameter("id", id)
             .ExecuteUpdate();
        }

        public Documento ObterPorIdComTipoDocumento(int documentoId)
        {
            return this.Sessao.QueryOver<Documento>()
                .Fetch(x => x.Usuario).Eager
                .Fetch(x => x.TipoDocumento).Eager
                .Fetch(x => x.TipoDocumento.Parent).Eager
                .Where(x => x.Id == documentoId)
                .SingleOrDefault();
        }

        public Documento ObterPorIdComTipoDocumentoEIndexacoes(int id)
        {
            return this.Sessao.QueryOver<Documento>()
               .Fetch(x => x.TipoDocumento).Eager
               .Fetch(x => x.TipoDocumento.Parent).Eager
               .Fetch(x => x.Indexacao).Eager
               .Fetch(x => x.Indexacao.First().Chave).Eager
               .Fetch(x => x.Usuario).Eager
               .Where(x => x.Id == id)
               .OrderBy(x => x.EhIndice).Desc
               .TransformUsing(Transformers.DistinctRootEntity)
               .SingleOrDefault();
        }

        public IList<Documento> ObterPorIdsComTipoDocumentoEIndexacoes(int[] currentPageIds)
        {
            return this.Sessao.QueryOver<Documento>()
                .Fetch(x => x.TipoDocumento).Eager
                .Fetch(x => x.TipoDocumento.Parent).Eager
                .Fetch(x => x.Indexacao).Eager
                .Fetch(x => x.Indexacao.First().Chave).Eager
                .Fetch(x => x.Usuario).Eager
                .OrderBy(x => x.EhIndice).Desc
                .WhereRestrictionOn(x => x.Id).IsIn(currentPageIds)
                .TransformUsing(Transformers.DistinctRootEntity)
                .List();
        }

        public IList<Documento> ObterPreIndexadosPorUsuario(Usuario usuario)
        {
            return this.Sessao.QueryOver<Documento>()
                .Where(x => x.EhPreIndexacao == true)
                .Fetch(x => x.TipoDocumento).Eager
                .Fetch(x => x.TipoDocumento.Parent).Eager
                .Fetch(x => x.Usuario).Eager
                .TransformUsing(Transformers.DistinctRootEntity)
                .List();
        }

        public IList<int> ObterIdsParaIndexar(int quantidade)
        {
            return this.Sessao.CreateQuery(
@"select 
    Id
from 
    Documento
where
    SearchStatus = :searchStatus")
                .SetParameter("searchStatus", SearchStatus.ToIndex)
                .SetFetchSize(quantidade)
                .List<int>();
        }

        public Documento ObterDocumentoParaIndexar(int documentoId)
        {
            return this.Sessao.QueryOver<Documento>()
                .Fetch(x => x.TipoDocumento).Eager
                .Fetch(x => x.Usuario).Eager
                .Fetch(x => x.Indexacao).Eager
                .Fetch(x => x.Indexacao.First().Chave).Eager
                .Where(x => x.Id == documentoId)
                .TransformUsing(Transformers.DistinctRootEntity)
                .SingleOrDefault();
        }
    }
}
