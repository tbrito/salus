namespace Salus.Infra.Repositorios
{
    using System;
    using System.Collections.Generic;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;

    public class AcessoFuncionalidadeRepositorio : Repositorio<AcessoFuncionalidade>, IAcessoFuncionalidadeRepositorio
    {
        public IList<AcessoFuncionalidade> ObterPorPapelComAtorId(int papelId, int atorId)
        {
            return this.Sessao.QueryOver<AcessoFuncionalidade>()
                .Where(x => x.Papel == Papel.FromInt32(papelId))
                .Where(x => x.AtorId == atorId)
                .List();
        }
    }
}