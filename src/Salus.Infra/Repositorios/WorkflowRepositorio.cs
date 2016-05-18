namespace Salus.Infra.Repositorios
{
    using NHibernate.Transform;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;
    using System.Collections.Generic;
    using System;

    public class WorkflowRepositorio : Repositorio<Workflow>, IWorkflowRepositorio
    {
        public IList<Workflow> ObterCaixaEntrada(Usuario usuarioAtual)
        {
            return this.Sessao.QueryOver<Workflow>()
                .Fetch(x => x.Para).Eager
                .Fetch(x => x.Documento).Eager
                .Where(x => x.Para == usuarioAtual && x.Status != WorkflowStatus.Finalizado)
                .OrderBy(x => x.CriadoEm).Desc
                .TransformUsing(Transformers.DistinctRootEntity)
                .List();
        }

        public IList<Workflow> ObterDoDocumento(int id)
        {
            return this.Sessao.QueryOver<Workflow>()
                .Fetch(x => x.Para).Eager
                .Fetch(x => x.De).Eager
                .Fetch(x => x.Documento).Eager
                .Where(x => x.Documento.Id == id)
                .OrderBy(x => x.CriadoEm).Desc
                .TransformUsing(Transformers.DistinctRootEntity)
                .List();
        }
    }
}