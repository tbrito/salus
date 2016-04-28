namespace Salus.Model.Servicos
{
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;

    public class WorkflowServico
    {
        private readonly ISessaoDoUsuario sessaoDoUsuario;
        private readonly IWorkflowRepositorio workflowRepositorio;

        public WorkflowServico(
            ISessaoDoUsuario sessaoDoUsuario,
            IWorkflowRepositorio workflowRepositorio)
        {
            this.sessaoDoUsuario = sessaoDoUsuario;
            this.workflowRepositorio = workflowRepositorio;
        }

        public void Iniciar(Documento documento)
        {
            var workflow = Workflow.Novo(documento, sessaoDoUsuario.UsuarioAtual);
            this.workflowRepositorio.Salvar(workflow);
        }
    }
}