namespace Salus.Infra.Repositorios
{
    using Salus.Infra.ConnectionInfra;
    using Salus.Model.Entidades;
    using System.Collections.Generic;

    public abstract class Repositorio<T> : BancoDeDados where T : Entidade
    {
        public void Salvar(T item)
        {
            this.Sessao.SaveOrUpdate(item);
        }

        public T ObterPorId(int id)
        {
            return this.Sessao.Get<T>(id);
        }

        public IList<T> ObterTodos()
        {
            return this.Sessao
                .CreateQuery("from " + typeof(T).Name)
                .List<T>();
        }

        public void Apagar(T item)
        {
            this.Sessao.Delete(item);
        }

        public void ApagarTodos()
        {
            this.Sessao.Delete("from " + typeof(T).Name);
        }
    }
}