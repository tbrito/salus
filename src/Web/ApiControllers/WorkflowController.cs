namespace Web.ApiControllers
{
    using Salus.Infra.IoC;
    using Salus.Infra.Logs;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;
    using Salus.Model.UI;
    using SharpArch.NHibernate;
    using System.Collections.Generic;
    using System.Web.Http;

    public class WorkflowController : ApiController
    {
        private IWorkflowRepositorio workflowRepositorio;
        private ISessaoDoUsuario sessaoDoUsuario;

        public WorkflowController()
        {
            this.workflowRepositorio = InversionControl.Current.Resolve<IWorkflowRepositorio>();
            this.sessaoDoUsuario = InversionControl.Current.Resolve<ISessaoDoUsuario>();
        }

        // GET api/<controller>
        [HttpGet]
        public IEnumerable<WorkflowViewModel> CaixaEntrada(int id = 0)
        {
            var workflow = this.workflowRepositorio
                .ObterCaixaEntrada(this.sessaoDoUsuario.UsuarioAtual);

            Log.App.Info("Documentos da Caixa de entrada:: " + workflow.Count);

            var fluxos = new List<WorkflowViewModel>();

            foreach (var fluxo in workflow)
            {
                var viewModel = new WorkflowViewModel
                {
                    Id = fluxo.Id,
                    CriadoEm = fluxo.CriadoEm,
                    De = fluxo.De,
                    Documento = fluxo.Documento,
                    FinalizadoEm = fluxo.FinalizadoEm,
                    Lido = fluxo.Lido,
                    Mensagem = fluxo.Mensagem,
                    Para = fluxo.Para,
                    Status = fluxo.Status
                };

                fluxos.Add(viewModel);
            }

            return fluxos as IEnumerable<WorkflowViewModel>;
        }

        [HttpGet]
        public IEnumerable<WorkflowViewModel> PorDocumento(int id)
        {
            var workflow = this.workflowRepositorio
                .ObterDoDocumento(id);

            Log.App.Info("Fluxos do documento:: " + workflow.Count);

            var fluxos = new List<WorkflowViewModel>();

            foreach (var fluxo in workflow)
            {
                var viewModel = new WorkflowViewModel
                {
                    Id = fluxo.Id,
                    CriadoEm = fluxo.CriadoEm,
                    De = fluxo.De,
                    Documento = fluxo.Documento,
                    FinalizadoEm = fluxo.FinalizadoEm,
                    Lido = fluxo.Lido,
                    Mensagem = fluxo.Mensagem,
                    Para = fluxo.Para,
                    Status = fluxo.Status
                };

                fluxos.Add(viewModel);
            }

            return fluxos as IEnumerable<WorkflowViewModel>;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}