namespace Veros.Ecm.DataAccess.Tarefas.Ecm6.Imports
{
    using System.Collections.Generic;
    using Model.Entities.Import;
    using NHibernate;
    using Veros.Data.Hibernate;
    using Veros.Ecm.Model.Entities;
    using Veros.Framework;

    public class PreIndexIndexesImport : ImportBase<Index>
    {
        private readonly Dictionary<int, PreIndexed> preIndexeds;

        public PreIndexIndexesImport(Dictionary<int, PreIndexed> preIndexeds)
        {
            this.preIndexeds = preIndexeds;
        }

        public override string SqlEcm6
        {
            get
            {
                return "select ecm6_id Ecm6Id, ecm8_id Ecm8Id from ecm6_preindexes where ecm6_id = :ecm6Id";
            }
        }

        public override IList<Index> GetEntities(IStatelessSession session)
        {
            const string Sql = @"
select
    prekeys_code Code,
    prekeydef_code KeyDefCode,
    predoc_code DocCode,
    prekeys_desc Descricao
from 
	prekeys
where
	predoc_code in 
        (select predoc_code from predoc where predoc_processed = 'N')";

            var indexes = new List<Index>();

            var dtos = session
                .CreateSQLQuery(Sql)
                .SetResultTransformer(CustomResultTransformer<PreIndexIndexesDto>.Do())
                .List<PreIndexIndexesDto>();

            foreach (var dto in dtos)
            {
                var content = this.preIndexeds.GetById(dto.DocCode.ToInt());

                indexes.Add(new Index
                {
                    Id = dto.Code.ToInt(),
                    Content = content,
                    Key = this.Create<Key>(dto.KeyDefCode),
                    Value = dto.Descricao
                });
            }

            return indexes;
        }

        public override bool ShouldImport(Index entity)
        {
            return this.CheckIfExist<Ecm6PreIndexIndexes>(entity);
        }

        public override void AfterImport(int oldId, Index entity, ISession session)
        {
            session.Save(new Ecm6DocVersionado
            {
                Ecm6Id = oldId,
                Ecm8Id = entity.Id
            });
        }
        
        public class PreIndexIndexesDto
        {
            public string Code
            {
                get;
                set;
            }

            public string DocCode
            {
                get;
                set;
            }

            public string KeyDefCode
            {
                get;
                set;
            }

            public string Descricao
            {
                get;
                set;
            }
        }
    }
}