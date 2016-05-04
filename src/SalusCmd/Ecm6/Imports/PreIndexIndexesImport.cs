namespace SalusCmd.Ecm6.Imports
{
    using System.Collections.Generic;
    using NHibernate;
    using Salus.Model.Entidades;
    using Salus.Infra.Repositorios;
    using Salus.Infra.Extensions;
    using Salus.Model.Entidades.Import;

    public class PreIndexIndexesImport : ImportBase<Indexacao>
    {
        private readonly Dictionary<int, Documento> preIndexeds;

        public PreIndexIndexesImport(Dictionary<int, Documento> preIndexeds)
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

        public override IList<Indexacao> GetEntities(IStatelessSession session)
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

            var indexes = new List<Indexacao>();

            var dtos = session
                .CreateSQLQuery(Sql)
                .SetResultTransformer(CustomResultTransformer<PreIndexIndexesDto>.Do())
                .List<PreIndexIndexesDto>();

            foreach (var dto in dtos)
            {
                var content = this.preIndexeds.GetById(dto.DocCode.ToInt());

                indexes.Add(new Indexacao
                {
                    Id = dto.Code.ToInt(),
                    Documento = content,
                    Chave = this.Create<Chave>(dto.KeyDefCode),
                    Valor = dto.Descricao
                });
            }

            return indexes;
        }

        public override bool ShouldImport(Indexacao entity)
        {
            return this.CheckIfExist<Ecm6PreIndexIndexes>(entity);
        }

        public override void AfterImport(int oldId, Indexacao entity, ISession session)
        {
            session.Save(new Ecm6PreIndexIndexes
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