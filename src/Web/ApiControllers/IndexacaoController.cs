namespace Web.ApiControllers
{
    using Salus.Infra.IoC;
    using Salus.Infra.Logs;
    using Salus.Infra.Repositorios;
    using Salus.Model;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    public class IndexacaoController : ApiController
    {
        private UsuarioRepositorio usuarioRepositorio;
        
        public IndexacaoController()
        {
            this.usuarioRepositorio = InversionControl.Current.Resolve<UsuarioRepositorio>();
        }
        
        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Salvar([FromBody]IEnumerable<Indexacao> value)
        {
            Log.App.Info(value.ElementAt(0).Valor);
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