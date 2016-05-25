namespace Salus.Infra.Repositorios
{
    using System;
    using System.Collections.Generic;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;

    public class TrilhaRepositorio : Repositorio<Trilha>, ITrilhaRepositorio
    {
        public IList<Trilha> ObterTodosComUsuario()
        {
            return this.Sessao.QueryOver<Trilha>()
                .Fetch(x => x.Usuario).Eager
                .List();
        }
    }
}