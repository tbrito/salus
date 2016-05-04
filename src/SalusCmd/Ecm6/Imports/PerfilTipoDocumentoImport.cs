namespace SalusCmd.Ecm6.Imports
{
    using System.Collections.Generic;
    using NHibernate;
    using Salus.Model.Entidades;
    using Salus.Model.Entidades.Import;
    using Salus.Infra.Repositorios;
    using Salus.Infra.Extensions;

    internal class PerfilTipoDocumentoImport : ImportBase<AcessoDocumento>
    {
        public override string SqlEcm6
        {
            get
            {
                return "select ecm6_id Ecm6Id, ecm8_id Ecm8Id from ecm6_accessdoc where ecm6_id = :ecm6Id";
            }
        }

        public override IList<AcessoDocumento> GetEntities(IStatelessSession session)
        {
            const string Sql = @"
select
    profiles.usr_code UsrCode, access_doc.groupdoc_code Category
from 
    access_doc access_doc,
    (select usr_code from usr where usr_type = 'G') profiles
where
    access_doc.usr_code = profiles.usr_code";

            var profileCategories = new List<AcessoDocumento>();

            var dtos = session
                .CreateSQLQuery(Sql)
                .SetResultTransformer(CustomResultTransformer<AccessDocDto>.Do())
                .List<AccessDocDto>();

            var i = 1;

            foreach (var dto in dtos)
            {
                profileCategories.Add(new AcessoDocumento
                {
                    Id = i++,
                    TipoDocumento = this.Create<TipoDocumento>(dto.Category),
                    AtorId = dto.UsrCode.ToInt(),
                    Papel = Papel.Pefil
                });
            }

            return profileCategories; 
        }

        public override bool ShouldImport(AcessoDocumento entity)
        {
            return this.CheckIfExist<Ecm6AccessDoc>(entity);
        }

        public override void AfterImport(int oldId, AcessoDocumento entity, ISession session)
        {
            session.Save(new Ecm6AccessDoc()
            {
                Ecm6Id = oldId,
                Ecm8Id = entity.Id
            });
        }
    }
}