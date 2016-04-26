namespace Salus.Model.Search
{
    using Salus.Model.Repositorios;
    using Salus.Model.Servicos;
    using System.Linq;

    public class SearchService
    {
        private readonly ISearchEngine searchEngine;
        private readonly ISessaoDoUsuario sessaoDoUsuario;
        private readonly AutorizaVisualizacaoDocumento autorizaVisualizacaoDocumento;
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IConfiguracoesDaAplicacao configuracoesDaAplicacao;

        public SearchService(
            ISearchEngine searchEngine, 
            ISessaoDoUsuario userSession,
            AutorizaVisualizacaoDocumento contentAuthorizator,
            IDocumentoRepositorio contentRepository,
            IConfiguracoesDaAplicacao configuracoesDaAplicacao)
        {
            this.searchEngine = searchEngine;
            this.sessaoDoUsuario = userSession;
            this.autorizaVisualizacaoDocumento = contentAuthorizator;
            this.configuracoesDaAplicacao = configuracoesDaAplicacao;
            this.documentoRepositorio = contentRepository;
        }

        public ResultadoPesquisaDocumento SearchContent(
            string text, 
            int page, 
            string startDate = null, 
            string endDate = null, 
            int tipodocumentoId = 0)
        {
            if (text == null)
            {
                text = string.Empty;
            }
            
            var contentsWithTextId = this.searchEngine
                .SearchDocumentosIds(text, tipodocumentoId, startDate, endDate)
                .ToArray();
            
            var documentosIds = this.autorizaVisualizacaoDocumento
                .ObterConteudosAutorizados(contentsWithTextId);
            
            //// obtem apenas os contents do paginamento atual
            var result = new ResultadosPesquisa(
                documentosIds,
                this.configuracoesDaAplicacao.MaximoResultadoPorPagina, 
                page,
                documentosIds.Length);

            var currentPageIds = result.ObterDocumentosDaPagina(page).ToArray();

            var fetchedContents = this.documentoRepositorio
                .ObterPorIdsComTipoDocumentoEIndexacoes(currentPageIds);
            
            return new ResultadoPesquisaDocumento(result, fetchedContents, documentosIds, this.searchEngine);
        }
    }
}
