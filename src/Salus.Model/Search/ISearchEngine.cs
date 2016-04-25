namespace Salus.Model.Search
{
    using System.Collections.Generic;

    public interface ISearchEngine
    {
        ////SearchResults SearchDossiers(
        ////    string text,
        ////    User user,
        ////    IList<Group> userGroups,
        ////    int actualPage,
        ////    int resultsPerPage,
        ////    string dataCriacaoInicial = null,
        ////    string dataCriacaoFinal = null,
        ////    int categoriaId = 0);

        IList<int> SearchDocumentosIds(string text, int tipodocumentoId = 0, string startDate = "", string endDate = "");
    }
}