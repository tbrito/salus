namespace Web.ApiControllers
{
    using Salus.Infra.IoC;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;
    using System.Collections.Generic;
    using System.Web.Http;

    public class AcessoFuncionalidadeController : ApiController
    {
        private IAcessoFuncionalidadeRepositorio acessoFuncionalidadeRepositorio;

        public AcessoFuncionalidadeController()
        {
            this.acessoFuncionalidadeRepositorio = InversionControl.Current.Resolve<IAcessoFuncionalidadeRepositorio>();
        }
        
        [HttpGet]
        public IEnumerable<AcessoFuncionalidade> ObterPor(int papelId, int atorId)
        {
            var acessos = this.acessoFuncionalidadeRepositorio
                .ObterPorPapelComAtorId(papelId, atorId);

            return acessos as IEnumerable<AcessoFuncionalidade>;
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