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
        private IDocumentoRepositorio documentoRepositorio;

        public IndexacaoController()
        {
            this.sessaoDoUsuario = InversionControl.Current.Resolve<ISessaoDoUsuario>();
            this.indexacaoRepositorio = InversionControl.Current.Resolve<IIndexacaoRepositorio>();
            this.documentoRepositorio = InversionControl.Current.Resolve<IDocumentoRepositorio>();
        }
        
        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet]
        public IEnumerable<Indexacao> PorDocumento(int id)
        {
            var indexacao = this.indexacaoRepositorio.ObterPorDocumento(new Documento { Id = id });
            return indexacao;
        }

        [HttpPost]
        public void Salvar([FromBody]IEnumerable<IndexacaoViewModel> indexacaoModel)
        {
            int documentoId = 0;

            foreach (var index in indexacaoModel)
            {
                var indexacao = new Indexacao();
                indexacao.Chave = new Chave { Id = index.CampoId };
                indexacao.Documento = new Documento { Id = index.DocumentoId };
                indexacao.Valor = index.Valor;
                documentoId = index.DocumentoId;

                this.indexacaoRepositorio.Salvar(indexacao);
            }
            
            this.documentoRepositorio.AlterStatus(documentoId, SearchStatus.ToIndex);
        }
        
        [HttpPut]
        public void SalvarChave(int id, IndexacaoViewModel indexacaoModel)
        {
            this.indexacaoRepositorio.AtualizarValor(indexacaoModel.Id, indexacaoModel.Valor);

            this.documentoRepositorio.AlterStatus(indexacaoModel.DocumentoId, SearchStatus.ToIndex);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }

    public class IndexacaoViewModel
    {
        public int Id { get; set;  }
        public int CampoId { get; set; }
        public int DocumentoId { get; set; }
        public string Valor { get; set; }
    }
}