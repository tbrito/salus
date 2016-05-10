using Salus.Model.Entidades;
using System.Collections.Generic;

namespace Salus.Model.Repositorios
{
    public interface IDocumentoRepositorio : IRepositorio<Documento>
    {
        IList<Documento> ObterPorIdsComTipoDocumentoEIndexacoes(int[] currentPageIds);

        void AlterStatus(int id, SearchStatus searchStatus);

        IList<Documento> ObterTodosParaIndexar(int quantidade);

        Documento ObterPorIdComTipoDocumento(int documentoId);
    }
}