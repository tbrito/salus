namespace Web.ApiControllers
{
    using Salus.Infra.IoC;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;
    using System.Collections.Generic;
    using System.Web.Http;

    public class AcessoFuncionalidadeController : ApiController
    {
        private ISessaoDoUsuario sessaoDoUsuario;

        public AcessoFuncionalidadeController()
        {
            this.sessaoDoUsuario = InversionControl.Current.Resolve<ISessaoDoUsuario>();
        }
        
        [HttpGet]
        public IList<AcessoFuncionalidade> ObterPor(int papelId, int atorId)
        {
            return null;
        }

        [HttpPost]
        public void Salvar([FromBody]IList<AcessoFuncionalidade> acessoFuncionalidades)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }
    }
}