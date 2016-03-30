using Salus.Model.Entidades;
using System.Collections.Generic;

namespace Salus.Model.Repositorios
{
    public interface IChaveRepositorio : IRepositorio<Chave>
    {
        IList<Chave> ObterPorTipoDocumentoId(int id);

        Chave ObterPorIdComTipoDocumento(int id);

        void MarcarComoInativo(int id);
    }
}