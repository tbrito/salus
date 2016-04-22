namespace Salus.Infra.Repositorios
{
    using System;
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

        public IList<AcessoDocumento> ObterDoUsuario(Usuario usuario)
        {
            return this.Sessao.CreateQuery(
@"from 
    AcessoDocumento
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
                .List<AcessoDocumento>();
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