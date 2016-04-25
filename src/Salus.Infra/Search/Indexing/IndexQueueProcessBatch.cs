namespace Salus.Infra.Search.Indexing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Framework;
    using Model.Entities;
    using Model.Enums;
    using Model.Repository;
    using Model.Services.Search;

    public class IndexQueueProcessBatch
    {
        private readonly IIndexContentSearchEngineService indexContentSearchEngineService;
        private readonly IContentRepository contentRepository;

        public IndexQueueProcessBatch(
            IIndexContentSearchEngineService indexContentSearchEngineService, 
            IContentRepository contentRepository)
        {
            this.indexContentSearchEngineService = indexContentSearchEngineService;
            this.contentRepository = contentRepository;
        }

        public int Execute(
            IEnumerable<Content> contents,
            SearchSettings searchSettings,
            IList<DossierKeySettings> dossierKeySettings)
        {
            var indexedItems = 0;
            var newStatuses = new Dictionary<int, SearchStatus>();

            foreach (var content in contents)
            {
                var newStatus = this.IndexContent(content, searchSettings, dossierKeySettings);
                newStatuses.Add(content.Id, newStatus);
                indexedItems++;
            }

            this.UpdateContentStatus(SearchStatus.Indexed, newStatuses);
            this.UpdateContentStatus(SearchStatus.TryIndexAgain, newStatuses);
            this.UpdateContentStatus(SearchStatus.CantIndex, newStatuses);

            return indexedItems;
        }

        private SearchStatus IndexContent(
            Content content,
            SearchSettings searchSettings,
            IList<DossierKeySettings> dossierKeysSettings)
        {
            try
            {
                this.indexContentSearchEngineService
                    .Execute(content, searchSettings, dossierKeysSettings);

                return SearchStatus.Indexed;
            }
            catch (Exception exception)
            {
                Log.Application.Error("Erro ao indexar conteudo " + content.Id, exception);

                if (content.SearchStatus == SearchStatus.TryIndexAgain)
                {
                    return SearchStatus.CantIndex;
                }

                return SearchStatus.TryIndexAgain;
            }
        }

        private void UpdateContentStatus(
            SearchStatus searchStatus,
            Dictionary<int, SearchStatus> newStatuses)
        {
            var ids = newStatuses
                .Where(x => x.Value == searchStatus)
                .Select(x => x.Key).ToArray();

            if (ids.Length > 0)
            {
                this.contentRepository.UpdateSearchStatus(searchStatus, ids);
            }
        }
    }
}
