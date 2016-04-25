namespace Salus.Infra.Search.Indexing
{
    using Model.Repositorios;
    using Model.Search;
    using Model.Servicos;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class IndexQueueProcess : IIndexQueueProcess
    {
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IConfiguracoesDaAplicacao configuracoesDaAplicacao;
        private readonly LuceneIndexerSession indexerSession;
        private readonly IndexQueueProcessBatch indexQueueProcessBatch;
        private int contentsIndexed;

        public IndexQueueProcess(
            IDocumentoRepositorio documentoRepositorio,
            IConfiguracoesDaAplicacao configuracoesDaAplicacao, 
            LuceneIndexerSession indexerSession, 
            IndexQueueProcessBatch indexQueueProcessBatch)
        {
            this.documentoRepositorio = documentoRepositorio;
            this.configuracoesDaAplicacao = configuracoesDaAplicacao;
            this.indexerSession = indexerSession;
            this.indexQueueProcessBatch = indexQueueProcessBatch;
        }

        public void Execute()
        {
            SearchSettings searchSettings;
            IList<DossierKeySettings> dossierKeysSettings;

            searchSettings = this.configuracoesDaAplicacao.Obter();
            dossierKeysSettings = this.dossierKeySettingsRepository.FetchIndexingKeys();
            
            this.ProcessQueue(searchSettings, dossierKeysSettings);

            using (var session = this.indexerSession.Begin(searchSettings.Path))
            {
                session.Current.Optimize();
            }

            this.LogResume();
        }
        
        private void ProcessQueue(
            SearchSettings searchSettings, 
            IList<DossierKeySettings> dossierKeysSettings)
        {
            Log.Application.InfoFormat("Indexando conteudos na fila");
            IList<Content> contents;
            
            do
            {
                using (this.unitOfWork.Begin())
                {
                    contents = this.documentoRepositorio.FetchAllSearchToIndex(80);
                }

                if (contents.Count == 0)
                {
                    Log.Application.InfoFormat("Não há conteúdos pendentes para indexação");
                    return;
                }

                Log.Application.InfoFormat(
                    "Indexando conteudos de {0} a {1}", 
                    contents[0].Id, 
                    contents.Last().Id);

                var contentsBatch = contents.DividirEmLotes(8);

                using (var session = this.indexerSession.Begin(searchSettings.Path))
                {
                    Parallel.ForEach(contentsBatch, Paralelizar.Em(8), batch =>
                    {
                        using (this.unitOfWork.Begin())
                        {
                            this.Increment(this.indexQueueProcessBatch
                                .Execute(batch, searchSettings, dossierKeysSettings));
                        }
                    });

                    session.Current.Commit();
                }
            }
            while (contents.Count > 0);
        }

        private void Increment(int indexed)
        {
            Interlocked.Add(ref this.contentsIndexed, indexed);
        }
    }
}
