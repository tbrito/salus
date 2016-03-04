﻿namespace Salus.Infra.Repositorios
{
    using System;
    using Salus.Model.Entidades;

    public class UsuarioRepositorio : Repositorio<Usuario>
    {
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
    }
}
