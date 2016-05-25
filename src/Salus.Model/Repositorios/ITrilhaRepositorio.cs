using Salus.Model.Entidades;
using System.Collections.Generic;

namespace Salus.Model.Repositorios
{
    public interface ITrilhaRepositorio : IRepositorio<Trilha>
    {
        IList<Trilha> ObterTodosComUsuario();
    }
}