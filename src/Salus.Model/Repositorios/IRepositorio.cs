using System.Collections.Generic;

namespace Salus.Model.Repositorios
{
    public interface IRepositorio<T>
    {
        void Salvar(T item);

        T ObterPorId(int id);

        IList<T> ObterTodos();

        void Apagar(T item);

        void ApagarTodos();
    }
}