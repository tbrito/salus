namespace Salus.Infra.Repositorios
{
    using System;
    using System.Collections.Generic;
    using Salus.Model.Entidades;
    using Salus.Model.Repositorios;
    using NHibernate.Criterion;
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

        public IList<AcessoFuncionalidade> ObterDoUsuario(Usuario usuario)
        {
            return this.Sessao.CreateQuery(
@"from 
    AcessoFuncionalidade
where 
    (Papel = :usuario and AtorId = :usuarioId) or
    (Papel = :perfil and AtorId = :perfilId) or
    (Papel = :area and AtorId = :areaId)")
                .SetParameter("usuario", Papel.Usuario)
                .SetParameter("perfil", Papel.Pefil)
                .SetParameter("area", Papel.Area)
                .SetParameter("usuarioId", usuario.Id)
                .SetParameter("perfilId", usuario.Perfil.Id)
                .SetParameter("areaId", usuario.Area.Id)
                .List<AcessoFuncionalidade>();
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