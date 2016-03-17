namespace Web.ApiControllers
{
    using Salus.Infra.IoC;
    using Salus.Model;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;
    using System.Collections.Generic;
    using System.Web.Http;

    public class TipoDocumentoController : ApiController
    {
        private ITipoDocumentoRepositorio tipoDocumentoRepositorio;
        private ISessaoDoUsuario sessaoDoUsuario;

        public TipoDocumentoController()
        {
            this.tipoDocumentoRepositorio = InversionControl.Current.Resolve<ITipoDocumentoRepositorio>();
            this.sessaoDoUsuario = InversionControl.Current.Resolve<ISessaoDoUsuario>();
        }

        // GET api/<controller>
        public IEnumerable<TipoDocumento> Get()
        {
            var tiposDocumentos = this.tipoDocumentoRepositorio
                .ObterTodosClassificaveis(this.sessaoDoUsuario.UsuarioAtual.Id);
           
            return tiposDocumentos as IEnumerable<TipoDocumento>;
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