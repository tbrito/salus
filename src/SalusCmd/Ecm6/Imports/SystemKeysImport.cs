namespace Veros.Ecm.DataAccess.Tarefas.Ecm6.Imports
{
    using System.Collections.Generic;
    using Model.Entities.Import;
    using NHibernate;
    using Veros.Data.Hibernate;
    using Veros.Framework;
    using Veros.Ecm.Model.Entities;
    using Veros.Ecm.Model.Enums;

    public class SystemKeysImport : ImportBase<SystemKey>
    {
        private readonly Dictionary<int, KeyType> keyTypes;

        public SystemKeysImport(Dictionary<int, KeyType> keyTypes)
        {
            this.keyTypes = keyTypes;
        }

        public override string SqlEcm6
        {
            get
            {
                return "select ecm6_id Ecm6Id, ecm8_id Ecm8Id from ecm6_keydef where ecm6_id = :ecm6Id";
            }
        }

        public override IList<SystemKey> GetEntities(IStatelessSession session)
        {
            var sql = @"
select
    keydef_code Code,
    keydef_desc Descricao,
    keytype_code KeyType,
    keydef_index Indexing
from 
    keydef";

            var systemKeys = new List<SystemKey>();

            var dtos = session
                .CreateSQLQuery(sql)
                .SetResultTransformer(CustomResultTransformer<KeyDefDto>.Do())
                .List<KeyDefDto>();

            foreach (var dto in dtos)
            {
                //// TODO: length == 88?
                var systemKey = new SystemKey
                {
                    Id = dto.Code.ToInt(),
                    Name = dto.Descricao.ToPascalCase(),
                    KeyType = this.keyTypes[dto.KeyType.ToInt()],
                    Indexing = string.IsNullOrEmpty(dto.Indexing) == false && dto.Indexing.ToUpper() == "S",
                    Length = 88
                };

                systemKeys.Add(systemKey);
            }

            return systemKeys;            
        }

        public override bool ShouldImport(SystemKey entity)
        {
            return this.CheckIfExist<Ecm6KeyDef>(entity);
        }

        public override void AfterImport(int oldId, SystemKey entity, ISession session)
        {
            session.Save(new Ecm6KeyDef
            {
                Ecm6Id = oldId,
                Ecm8Id = entity.Id
            });
        }

        public class KeyDefDto
        {
            public string Descricao
            {
                get;
                set;
            }

            public string KeyType
            {
                get;
                set;
            }

            public string Code
            {
                get;
                set;
            }

            public string Indexing
            {
                get;
                set;
            }
        }
    }
}