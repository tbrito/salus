namespace Salus.Infra.Repositorios
{
    using System.Collections.Generic;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;

    public class AcessoFuncionalidadeRepositorio : 
        Repositorio<AcessoFuncionalidade>, 
        IAcessoFuncionalidadeRepositorio
    {
        public void ApagarAcessosDoAtor(int papelId, int atorId)
        {
            this.Sessao.CreateSQLQuery(@"Delete from acessofuncionalidade where autor_id = :atorId and papel_id =:papelId")
                .SetParameter("atorId", atorId)
                .SetParameter("papelId", papelId)
                .ExecuteUpdate();
        }

        public IList<AcessoFuncionalidade> ObterPorPapelComAtorId(int papelId, int atorId)
        {
            return this.Sessao.QueryOver<AcessoFuncionalidade>()
                .Where(x => x.Papel == Papel.FromInt32(papelId))
                .Where(x => x.AtorId == atorId)
                .List();
        }
    }
}