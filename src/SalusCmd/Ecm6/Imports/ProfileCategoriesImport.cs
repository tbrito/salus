namespace Veros.Ecm.DataAccess.Tarefas.Ecm6.Imports
{
    using System.Collections.Generic;
    using Model.Entities.Import;
    using NHibernate;
    using Veros.Data.Hibernate;
    using Veros.Ecm.Model.Entities;
    using Veros.Framework;

    internal class ProfileCategoriesImport : ImportBase<ProfileCategory>
    {
        public override string SqlEcm6
        {
            get
            {
                return "select ecm6_id Ecm6Id, ecm8_id Ecm8Id from ecm6_accessdoc where ecm6_id = :ecm6Id";
            }
        }

        public override IList<ProfileCategory> GetEntities(IStatelessSession session)
        {
            const string Sql = @"
select
    profiles.usr_code UsrCode, access_doc.groupdoc_code Category
from 
    access_doc access_doc,
    (select usr_code from usr where usr_type = 'G') profiles
where
    access_doc.usr_code = profiles.usr_code";

            var profileCategories = new List<ProfileCategory>();

            var dtos = session
                .CreateSQLQuery(Sql)
                .SetResultTransformer(CustomResultTransformer<AccessDocDto>.Do())
                .List<AccessDocDto>();

            var i = 1;

            foreach (var dto in dtos)
            {
                profileCategories.Add(new ProfileCategory
                {
                    Id = i++,
                    Category = this.Create<Category>(dto.Category),
                    Profile = this.Create<Profile>(dto.UsrCode)
                });
            }

            return profileCategories; 
        }

        public override bool ShouldImport(ProfileCategory entity)
        {
            return this.CheckIfExist<Ecm6AccessDoc>(entity);
        }

        public override void AfterImport(int oldId, ProfileCategory entity, ISession session)
        {
            session.Save(new Ecm6AccessDoc()
            {
                Ecm6Id = oldId,
                Ecm8Id = entity.Id
            });
        }
    }
}