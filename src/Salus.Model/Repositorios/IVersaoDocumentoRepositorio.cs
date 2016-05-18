namespace Salus.Model.Repositorios
{
    using Salus.Model.Entidades;
    using System.Collections.Generic;

    public interface IVersaoDocumentoRepositorio : IRepositorio<VersaoDocumento>
    {
        IList<VersaoDocumento> ObterDoDocumento(int documentoId);
    }
}