namespace Salus.Infra.Repositorios
{
    using System;
    using System.Collections.Generic;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;

    public class TipoDocumentoRepositorio : Repositorio<TipoDocumento>, ITipoDocumentoRepositorio
    {
        public void Ativar(int id)
        {
            this.Sessao
              .CreateQuery("update TipoDocumento set Ativo = true where Id = :id")
              .SetInt32("id", id)
              .ExecuteUpdate();
        }

        public void MarcarComoInativo(int id)
        {
            this.Sessao
              .CreateQuery("update TipoDocumento set Ativo = false where Id = :id")
              .SetInt32("id", id)
              .ExecuteUpdate();
        }

        public TipoDocumento ObterPorIdComParents(int id)
        {
            return this.Sessao.QueryOver<TipoDocumento>()
                .Fetch(x => x.Parent).Eager
                .Where(x => x.Id == id)
                .SingleOrDefault();
        }

        public IList<TipoDocumento> ObterTodosClassificaveis(Usuario usuario)
        {
            return this.Sessao.QueryOver<TipoDocumento>()
                .Fetch(x => x.Parent).Eager
                .List();
        }

        public IList<TipoDocumento> ObterTodosGrupos(Usuario usuarioAtual)
        {
            return this.Sessao.QueryOver<TipoDocumento>()
                .Fetch(x => x.Parent).Eager
                .Where(x => x.EhPasta == true)
                .List();
        }

        public IList<TipoDocumento> ObterTodosParaIndexar(Usuario usuarioAtual)
        {
            return this.Sessao.QueryOver<TipoDocumento>()
                .Fetch(x => x.Parent).Eager
                .Where(x => x.EhPasta == false)
                .List();
        }
    }
}