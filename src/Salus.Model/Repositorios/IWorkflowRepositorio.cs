namespace Salus.Model.Repositorios
{
    using Salus.Model.Entidades;
    using System.Collections.Generic;

    public interface IWorkflowRepositorio : IRepositorio<Workflow>
    {
        IList<Workflow> ObterCaixaEntrada(Usuario usuarioAtual);

        IList<Workflow> ObterDoDocumento(int id);
    }
}