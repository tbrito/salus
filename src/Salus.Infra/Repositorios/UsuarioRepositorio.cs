namespace Salus.Infra.Repositorios
{
    using System;
    using Salus.Model.Entidades;
    using Model.Repositorios;
    using System.Collections.Generic;

    public class UsuarioRepositorio : Repositorio<Usuario>, IUsuarioRepositorio
    {
        public void MarcarComoInativo(int id)
        {
            this.Sessao
              .CreateQuery("update Usuario set Ativo = false where Id = :id")
              .SetInt32("id", id)
              .ExecuteUpdate();
        }

        public Usuario ObterPorIdComParents(int id)
        {
            return this.Sessao.QueryOver<Usuario>()
                .Fetch(x => x.Area).Eager
                .Fetch(x => x.Perfil).Eager
                .Where(x => x.Id == id)
                .SingleOrDefault();
        }

        public IList<Usuario> ObterTodosComAreaEPerfil()
        {
            return this.Sessao.QueryOver<Usuario>()
                .Fetch(x => x.Area).Eager
                .Fetch(x => x.Perfil).Eager
                .OrderBy(x => x.Nome).Asc
                .List();
        }

        public Usuario Procurar(string userName, string senha)
        {
            return this.Sessao.QueryOver<Usuario>()
                .Where(x => x.Nome == userName && x.Senha == senha)
                .SingleOrDefault();
        }

        public Usuario ProcurarPorNome(string nomeUsuario)
        {
            return this.Sessao.QueryOver<Usuario>()
                .Where(x => x.Nome == nomeUsuario)
                .SingleOrDefault();
        }

        public void SalvarSenha(int id, string novaSenha)
        {
            this.Sessao
              .CreateQuery("update Usuario set senha = :novaSenha where Id = :id")
              .SetParameter("id", id)
              .SetParameter("novaSenha", novaSenha)
              .ExecuteUpdate();
        }
    }
}
