namespace Salus.Infra.Repositorios
{
    using System;
    using Salus.Model.Entidades;
    using System.Collections.Generic;

    public class AtividadeRepositorio : Repositorio<Atividade>
    {
        public IList<Atividade> ObterTodosDoUsuario(string usuario)
        {
            throw new NotImplementedException();
        }
    }
}