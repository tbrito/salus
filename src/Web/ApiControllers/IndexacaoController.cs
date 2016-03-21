namespace Web.ApiControllers
{
    using Salus.Infra.IoC;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;
    using System.Collections.Generic;
    using System.Web.Http;

    public class IndexacaoController : ApiController
    {
        private ISessaoDoUsuario sessaoDoUsuario;
        private IIndexacaoRepositorio indexacaoRepositorio;

        public IndexacaoController()
        {
            this.sessaoDoUsuario = InversionControl.Current.Resolve<ISessaoDoUsuario>();
            this.indexacaoRepositorio = InversionControl.Current.Resolve<IIndexacaoRepositorio>();
        }
        
        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Salvar([FromBody]IEnumerable<IndexacaoViewModel> indexacaoModel)
        {
            foreach (var index in indexacaoModel)
            {
                var indexacao = new Indexacao();
                indexacao.Chave = new Chave { Id = index.CampoId };
                indexacao.Documento = new Documento { Id = index.DocumentoId };
                indexacao.Valor = index.Valor;

                this.indexacaoRepositorio.Salvar(indexacao);
            }
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

    public class IndexacaoViewModel
    {
        public int CampoId { get; set; }
        public int DocumentoId { get; set; }
        public string Valor { get; set; }
    }
}