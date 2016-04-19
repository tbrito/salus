using Salus.Model.Entidades;
using System.Collections.Generic;

namespace Salus.Model.Repositorios
{
    public interface IAcessoDocumentoRepositorio : IRepositorio<AcessoDocumento>
    {
        IList<AcessoDocumento> ObterPorPapelComAtorId(int papelId, int atorId);

        void ApagarAcessosDoAtor(int papelId, int atorId);
    }
}