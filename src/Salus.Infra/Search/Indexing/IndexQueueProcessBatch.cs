namespace Salus.Infra.Search.Indexing
{
    using Logs;
    using Model.Entidades;
    using Model.Repositorios;
    using Model.Search;
    using System;
    using System.Collections.Generic;

    public class IndexQueueProcessBatch
    {
        private readonly IIndexEngine indexContentSearchEngineService;
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IIndexacaoRepositorio indexacaoRepositorio;

        public IndexQueueProcessBatch(
            IIndexEngine indexEngine, 
            IDocumentoRepositorio documentoRepositorio,
            IIndexacaoRepositorio indexacaoRepositorio)
        {
            this.indexContentSearchEngineService = indexEngine;
            this.indexacaoRepositorio = indexacaoRepositorio;
            this.documentoRepositorio = documentoRepositorio;
        }

        public int Execute(int documentoId)
        {
            var indexedItems = 0;
            var newStatuses = new Dictionary<int, SearchStatus>();

            var documento = this.documentoRepositorio.ObterDocumentoParaIndexar(documentoId);

            var newStatus = this.IndexContent(documento);
         
            return indexedItems;
        }

        private SearchStatus IndexContent(Documento content)
        {
            try
            {
                this.indexContentSearchEngineService.Index(content, content.Indexacao);
                this.documentoRepositorio.AlterStatus(content.Id, SearchStatus.Indexed);
                Log.App.Info("Documento indexado com sucesso #" + content.Id);

                return SearchStatus.Indexed;
            }
            catch (Exception exception)
            {
                Log.App.Error("Erro ao indexar documento " + content.Id, exception);

                return SearchStatus.TryIndexAgain;
            }
        }
    }
}
