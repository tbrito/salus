namespace Salus.Model.Search
{
    using Entidades;
    using System.Collections.Generic;

    public class ResultadoPesquisaDocumento
    {
        private readonly IList<Documento> documentos;

        public ResultadoPesquisaDocumento(
            ResultadosPesquisa searchResults, 
            IList<Documento> documentos,
            IList<int> allContentsIdsFound,
            ISearchEngine searchEngine)
        {
            this.documentos = documentos;
            this.SearchEngine = searchEngine;
            this.TodosOsDocumentosIdsEncontrados = allContentsIdsFound;
            this.ResultadoPesquisa = searchResults;
        }

        public ISearchEngine SearchEngine
        {
            get;
            private set;
        }

        public ResultadosPesquisa ResultadoPesquisa
        {
            get;
            private set;
        }

        public IList<Documento> Documentos
        {
            get { return this.documentos; }
        }

        public IList<int> TodosOsDocumentosIdsEncontrados
        {
            get;
            private set;
        }
    }
}
