using System.Collections.Generic;

namespace Salus.Data
{
    public class Repositorio<T> : SessaoDoBanco, IRepositorio<T>
    {
        public void Salvar(T entidade)
        {
            this.CurrentSession.SaveOrUpdate(entidade);
            this.CurrentSession.Flush();
        }

        public IList<T> ObterTodos()
        {
            return this.CurrentSession
                .CreateQuery("from " + typeof(T).Name)
                .List<T>();
        }

        public void Excluir(T entidade)
        {
            this.CurrentSession.Delete(typeof(T));
            this.CurrentSession.Flush();
        }

        public T ObterPorId(int id)
        {
            return this.CurrentSession.Get<T>(id);
        }
    }
}