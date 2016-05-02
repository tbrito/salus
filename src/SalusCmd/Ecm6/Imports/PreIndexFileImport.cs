namespace Veros.Ecm.DataAccess.Tarefas.Ecm6.Imports
{
    using System.Collections.Generic;
    using Model.Entities.Import;
    using NHibernate;
    using Veros.Data.Hibernate;
    using Veros.Ecm.Model.Entities;
    using Veros.Ecm.Model.Enums;
    using Veros.Framework;

    public class PreIndexFileImport : PaginatedImportBase<PreIndexed>
    {
        public override string SqlEcm6
        {
            get
            {
                return "select ecm6_id Ecm6Id, ecm8_id Ecm8Id from ecm6_predoc";
            }
        }

        public override List<PreIndexed> GetEntities(IStatelessSession session, int startRow, int endRow)
        {
            const string Sql = @"
select
    ROW_NUMBER() over (order by predoc_code) as RowNum, 
    predoc_code Code, 
    groupdoc_code GroupDocCode, 
    predoc_usr Usr, 
    predoc_date Date
from 
    predoc
where 
	predoc_processed = 'N'";

            var preIndexeds = new List<PreIndexed>();

            var dtos = session
                .CreateSQLQuery(this.GetPaginatedSql(Sql, startRow, endRow))
                .SetResultTransformer(CustomResultTransformer<PreIndexedFileDto>.Do())
                .List<PreIndexedFileDto>();

            foreach (var preIndexedFileDto in dtos)
            {
                Log.Application.DebugFormat("Importando PreIndexedFileDto #{0}", preIndexedFileDto.Code);

                var preIndexed = new PreIndexed();

                preIndexed.Id = preIndexedFileDto.Code.ToInt();
                preIndexed.CreatedAt = preIndexedFileDto.Date;

                if (string.IsNullOrEmpty(preIndexedFileDto.Usr) == false)
                {
                    preIndexed.CreatedBy = this.Create<User>(preIndexedFileDto.Usr);
                }

                preIndexed.SearchStatus = SearchStatus.DontIndex;

                if (string.IsNullOrEmpty(preIndexedFileDto.GroupDocCode) == false)
                {
                    preIndexed.Category = this.Create<Category>(preIndexedFileDto.GroupDocCode);

                    if (preIndexed.Category != null)
                    {
                        preIndexed.Subject = this.Create<Category>(preIndexedFileDto.GroupDocCode).Name;
                    }
                }

                preIndexeds.Add(preIndexed);
            }

            return preIndexeds;
        }

        public override void AfterImport(int oldId, PreIndexed entity, ISession session)
        {
            session.Save(new Ecm6PreDoc
            {
                Ecm6Id = oldId,
                Ecm8Id = entity.Id
            });
        }

        public override bool ShouldImport(PreIndexed entity)
        {
            return this.CheckIfExist<Ecm6PreDoc>(entity);
        }
    }
}