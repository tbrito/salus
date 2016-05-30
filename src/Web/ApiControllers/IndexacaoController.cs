namespace Web.ApiControllers
{
    using Salus.Infra.IoC;
    using Salus.Model;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;
    using Salus.Model.Servicos;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    public class IndexacaoController : ApiController
    {
        private ISessaoDoUsuario sessaoDoUsuario;
        private IIndexacaoRepositorio indexacaoRepositorio;
        private IDocumentoRepositorio documentoRepositorio;
        private LogarAcaoDoSistema logarAcaoSistema;

        public IndexacaoController()
        {
            this.sessaoDoUsuario = InversionControl.Current.Resolve<ISessaoDoUsuario>();
            this.indexacaoRepositorio = InversionControl.Current.Resolve<IIndexacaoRepositorio>();
            this.documentoRepositorio = InversionControl.Current.Resolve<IDocumentoRepositorio>();
            this.logarAcaoSistema = InversionControl.Current.Resolve<LogarAcaoDoSistema>();
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

        [HttpGet]
        public IHttpActionResult ObterSuc(int id)
        {
            var indexacao = this.indexacaoRepositorio.ObterPorDocumento(new Documento { Id = id });
            var index = indexacao.FirstOrDefault(x => x.Chave.Nome.ToLower().Contains("cpf"));

            if (index == null)
            {
                index = indexacao.FirstOrDefault(x => x.Chave.Nome.ToLower().Contains("cnpj"));
            }

            var suc = id.ToString();

            if (index != null)
            {
                suc = index.Valor;
            }

            return Ok(new { Suc = suc });
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

            this.logarAcaoSistema.Execute(
              TipoTrilha.Criacao,
              "Indexação de Documento",
              "Documento foi indexado #" + documentoId);
        }
        
        [HttpPut]
        public void SalvarChave(int id, IndexacaoViewModel indexacaoModel)
        {
            this.indexacaoRepositorio.AtualizarValor(indexacaoModel.Id, indexacaoModel.Valor);

            this.documentoRepositorio.AlterStatus(indexacaoModel.DocumentoId, SearchStatus.ToIndex);

            this.logarAcaoSistema.Execute(
              TipoTrilha.Alteracao,
              "Indexação de Documento",
              "Documento teve indexação alterada Documento: #" + indexacaoModel.DocumentoId);
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
        public int TipoDocumentoId { get; set; }
    }
}