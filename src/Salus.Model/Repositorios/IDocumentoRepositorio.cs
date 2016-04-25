using Salus.Model.Entidades;
using System.Collections.Generic;

namespace Salus.Model.Repositorios
{
    public interface IDocumentoRepositorio : IRepositorio<Documento>
    {
        IList<Documento> ObterPorIdsComTipoDocumentoEIndexacoes(int[] currentPageIds);
    }
}