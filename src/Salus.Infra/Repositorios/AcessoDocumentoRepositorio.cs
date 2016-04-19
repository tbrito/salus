namespace Salus.Infra.Repositorios
{
    using System.Collections.Generic;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;

    public class AcessoDocumentoRepositorio : 
        Repositorio<AcessoDocumento>, 
        IAcessoDocumentoRepositorio
    {
        public void ApagarAcessosDoAtor(int papelId, int atorId)
        {
            this.Sessao.CreateSQLQuery(@"Delete from acessodocumento where autor_id = :atorId and papel_id =:papelId")
                .SetParameter("atorId", atorId)
                .SetParameter("papelId", papelId)
                .ExecuteUpdate();
        }

        public IList<AcessoDocumento> ObterPorPapelComAtorId(int papelId, int atorId)
        {
            return this.Sessao.QueryOver<AcessoDocumento>()
                .Where(x => x.Papel == Papel.FromInt32(papelId))
                .Where(x => x.AtorId == atorId)
                .List();
        }
    }
}