using System.Collections.Generic;

namespace Salus.Data
{
    public interface IRepositorio<T>
    {
        void Salvar(T entidade);
        
        IList<T> ObterTodos();

        void Excluir(T entidade);

        T ObterPorId(int id);
    }
}