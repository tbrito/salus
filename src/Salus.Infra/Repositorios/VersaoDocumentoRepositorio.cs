﻿namespace Salus.Infra.Repositorios
{
    using NHibernate.Transform;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;
    using System.Collections.Generic;
    using System;

    public class VersaoDocumentoRepositorio : Repositorio<VersaoDocumento>, IVersaoDocumentoRepositorio
    {
        public IList<VersaoDocumento> ObterDoDocumento(int documentoId)
        {
            return this.Sessao.QueryOver<VersaoDocumento>()
                .Where(x => x.Documento.Id == documentoId)
                .Fetch(x => x.CriadoPor).Eager
                .OrderBy(x => x.CriadoEm).Desc
                .TransformUsing(Transformers.DistinctRootEntity)
                .List();
        }
    }
}