namespace Web.ApiControllers
{
    using Salus.Infra.IoC;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;
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
        public IEnumerable<Workflow> CaixaEntrada(int id = 0)
        {
            var workflow = this.workflowRepositorio
                .ObterCaixaEntrada(this.sessaoDoUsuario.UsuarioAtual);
           
            return workflow as IEnumerable<Workflow>;
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