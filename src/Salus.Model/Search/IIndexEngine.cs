using Salus.Model.Entidades;
using System.Collections.Generic;

namespace Salus.Model.Search
{
    public interface IIndexEngine
    {
        SearchStatus Index(Documento Documento, IList<Indexacao> indexacao);

        void DeleteContentIfExist(int contentId);
    }
}