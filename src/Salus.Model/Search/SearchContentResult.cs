namespace Salus.Model.Search
{
    using Entidades;
    using System.Collections.Generic;

    public class SearchContentResult
    {
        private readonly IList<Documento> documentos;

        public SearchContentResult(
            SearchResults searchResults, 
            IList<Documento> documentos,
            IList<int> allContentsIdsFound,
            ISearchEngine searchEngine)
        {
            this.documentos = documentos;
            this.SearchEngine = searchEngine;
            this.AllContentsIdsFound = allContentsIdsFound;
            this.SearchResults = searchResults;
        }

        public ISearchEngine SearchEngine
        {
            get;
            private set;
        }

        public SearchResults SearchResults
        {
            get;
            private set;
        }

        public IList<Documento> Documento
        {
            get { return this.documentos; }
        }

        public IList<int> AllContentsIdsFound
        {
            get;
            private set;
        }
    }
}
