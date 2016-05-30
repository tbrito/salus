namespace Web.ApiControllers
{
    using Salus.Infra.IoC;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;
    using Salus.Model.Servicos;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    public class PreindexacaoController : ApiController
    {
        private IDocumentoRepositorio documentoRepositorio;
        private ISessaoDoUsuario sessaoDoUsuario;
        private ITipoDocumentoRepositorio tipoDocumentoRepositorio;
        private IIndexacaoRepositorio indexacaoRepositorio;
        private LogarAcaoDoSistema logarAcaoSistema;

        public PreindexacaoController()
        {
            this.documentoRepositorio = InversionControl.Current.Resolve<IDocumentoRepositorio>();
            this.sessaoDoUsuario = InversionControl.Current.Resolve<ISessaoDoUsuario>();
            this.tipoDocumentoRepositorio = InversionControl.Current.Resolve<ITipoDocumentoRepositorio>();
            this.indexacaoRepositorio = InversionControl.Current.Resolve<IIndexacaoRepositorio>();
            this.logarAcaoSistema = InversionControl.Current.Resolve<LogarAcaoDoSistema>();
        }

        [HttpGet]
        public IEnumerable<Documento> Get()
        {
            var documentos = this.documentoRepositorio
                .ObterPreIndexadosPorUsuario(this.sessaoDoUsuario.UsuarioAtual);
            
            return documentos as IEnumerable<Documento>;
        }

        [HttpGet]
        public Documento ObterPorId(int id)
        {
            var documento = this.documentoRepositorio
                .ObterPorIdComTipoDocumentoEIndexacoes(id);

            return documento;
        }

        [HttpPost]
        public void Salvar([FromBody]IEnumerable<IndexacaoViewModel> indexacaoModel)
        {
            var tipodocumentoId = indexacaoModel.First().TipoDocumentoId;
            var tipoDocumento = this.tipoDocumentoRepositorio.ObterPorId(tipodocumentoId);
            var documento = new Documento
            {
                Assunto = tipoDocumento.Nome,
                Bloqueado = false,
                DataCriacao = DateTime.Now,
                EhIndice = false,
                EhPreIndexacao = true,
                SearchStatus = SearchStatus.ToIndex,
                TipoDocumento = tipoDocumento,
                Usuario = this.sessaoDoUsuario.UsuarioAtual
            };

            this.documentoRepositorio.Salvar(documento);

            foreach (var index in indexacaoModel)
            {
                var indexacao = new Indexacao();
                indexacao.Chave = new Chave { Id = index.CampoId };
                indexacao.Documento = documento;
                indexacao.Valor = index.Valor;
                
                this.indexacaoRepositorio.Salvar(indexacao);
            }
            
            this.logarAcaoSistema.Execute(
              TipoTrilha.Criacao,
              "Preindexacao de documento",
              "Documento foi criado #" + documento.Id);
        }
        
        [HttpDelete]
        public void Excluir(int id)
        {
        }
    }
}