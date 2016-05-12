namespace Salus.Infra.Repositorios
{
    using System;
    using System.Collections.Generic;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;

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