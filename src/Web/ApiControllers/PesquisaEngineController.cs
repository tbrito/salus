namespace Web.ApiControllers
{
    using Salus.Infra.IoC;
    using Salus.Infra.Repositorios;
    using Salus.Model.Search;
    using Salus.Model.UI;
    using System.Web.Http;

    public class PesquisaEngineController : ApiController
    {
        private UsuarioRepositorio usuarioRepositorio;
        private SearchService searchService;
        
        public PesquisaEngineController()
        {
            this.usuarioRepositorio = InversionControl.Current.Resolve<UsuarioRepositorio>();
            this.searchService = InversionControl.Current.Resolve<SearchService>();
        }

        public IHttpActionResult Post([FromBody]PesquisaViewModel viewModel)
        {
            var resultado = this.searchService.SearchContent(viewModel.Texto, viewModel.PaginaId);
            return Ok(resultado);
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