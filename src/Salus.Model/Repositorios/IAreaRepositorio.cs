using Salus.Model.Entidades;
using System.Collections.Generic;

namespace Salus.Model.Repositorios
{
    public interface IAreaRepositorio : IRepositorio<Area>
    {
        IList<Area> ObterTodosAtivos();

        Area ObterPorIdComParents(int id);

        void MarcarComoInativo(int id);

        void Reativar(int id);
    }
}