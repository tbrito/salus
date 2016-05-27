namespace Salus.Infra.Repositorios
{
    using System;
    using System.Collections.Generic;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;
    using NHibernate.Transform;
    public class AreaRepositorio : Repositorio<Area>, IAreaRepositorio
    {
        public void MarcarComoInativo(int id)
        {
            this.Sessao
              .CreateQuery("update Area set Ativo = false where Id = :id")
              .SetInt32("id", id)
              .ExecuteUpdate();
        }

        public Area ObterPorIdComParents(int id)
        {
            return this.Sessao.QueryOver<Area>()
                .Fetch(x => x.Parent).Eager
                .Where(x => x.Id == id)
                .SingleOrDefault();
        }

        public IList<Area> ObterTodosAtivos(Usuario usuarioAtual)
        {
            return this.Sessao.QueryOver<Area>()
                .Fetch(x => x.Parent).Eager
                .Fetch(x => x.SubAreas).Eager
                .TransformUsing(Transformers.DistinctRootEntity)
                .Where(x => x.Parent == null)
                .List();
        }

        public void Reativar(int id)
        {
            this.Sessao
              .CreateQuery("update Area set Ativo = true where Id = :id")
              .SetInt32("id", id)
              .ExecuteUpdate();
        }
    }
}