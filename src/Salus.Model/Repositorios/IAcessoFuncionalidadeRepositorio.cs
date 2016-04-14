using Salus.Model.Entidades;
using System.Collections.Generic;

namespace Salus.Model.Repositorios
{
    public interface IAcessoFuncionalidadeRepositorio : IRepositorio<AcessoFuncionalidade>
    {
        IList<AcessoFuncionalidade> ObterPorPapelComAtorId(int papelId, int atorId);
    }
}