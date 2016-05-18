namespace Web.ApiControllers
{
    using Salus.Infra.IoC;
    using Salus.Infra.Logs;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;
    using System.Collections.Generic;
    using System.Web.Http;

    public class VersaoDocumentoController : ApiController
    {
        private IVersaoDocumentoRepositorio versaoDocumentoRepositorio;
        private ISessaoDoUsuario sessaoDoUsuario;

        public VersaoDocumentoController()
        {
            this.versaoDocumentoRepositorio = InversionControl.Current.Resolve<IVersaoDocumentoRepositorio>();
            this.sessaoDoUsuario = InversionControl.Current.Resolve<ISessaoDoUsuario>();
        }

        // GET api/<controller>
        [HttpGet]
        public IEnumerable<VersaoDocumento> ObterPorDocumento(int id)
        {
            var versoes = this.versaoDocumentoRepositorio
                .ObterDoDocumento(id);

            Log.App.Info("Versões do documento:: " + versoes.Count);
            
            return versoes as IEnumerable<VersaoDocumento>;
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