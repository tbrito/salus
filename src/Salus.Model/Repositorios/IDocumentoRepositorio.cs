using Salus.Model.Entidades;
using System.Collections.Generic;

namespace Salus.Model.Repositorios
{
    public interface IDocumentoRepositorio : IRepositorio<Documento>
    {
        IList<Documento> ObterPorIdsComTipoDocumentoEIndexacoes(int[] currentPageIds);

        void AlterStatus(int id, SearchStatus searchStatus);

        void Bloquear(int id);

        void Desbloquear(int id);

        Documento ObterDocumentoParaIndexar(int quantidade);

        Documento ObterPorIdComTipoDocumento(int documentoId);

        IList<Documento> ObterPreIndexadosPorUsuario(Usuario usuario);

        Documento ObterPorIdComTipoDocumentoEIndexacoes(int id);

        IList<int> ObterIdsParaIndexar(int quantidade);
    }
}