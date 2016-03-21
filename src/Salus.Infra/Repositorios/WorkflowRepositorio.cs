namespace Salus.Infra.Repositorios
{
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;
    using System.Collections.Generic;

    public class WorkflowRepositorio : Repositorio<Workflow>, IWorkflowRepositorio
    {
        public IList<Workflow> ObterCaixaEntrada(Usuario usuarioAtual)
        {
            return this.Sessao.QueryOver<Workflow>()
                .Fetch(x => x.Para).Eager
                .Fetch(x => x.Documento).Eager
                .Where(x => x.Para == usuarioAtual)
                .OrderBy(x => x.CriadoEm).Desc
                .List();
        }
    }
}