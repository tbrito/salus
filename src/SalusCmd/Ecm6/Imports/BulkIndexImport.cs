namespace Veros.Ecm.DataAccess.Tarefas.Ecm6.Imports
{
    using System.Collections.Generic;
    using System.Data;
    using Model.Entities;
    using Veros.Ecm.DataAccess.Tarefas.SystemKeys;
    using Dapper;
    using Veros.Ecm.Model.Enums;
    using Veros.Framework;

    public class BulkIndexImport : BulkImport<KeysDto, Index>
    {
        private Dictionary<int, int> keys;

        protected override string TableName
        {
            get
            {
                return "indexes";
            }
        }

        protected override Index ConvertDtoToEntity(KeysDto dto)
        {
            var value = dto.Descricao;
            var keyId = dto.KeyDefCode.ToInt();

            if (this.keys.ContainsKey(keyId))
            {
                var type = this.keys[keyId];
                if (type == KeyType.CpfCnpj.ToInt())
                {
                    value = value.RemoveCaracteresEspeciais();
                }
            }
            
            return new Index
            {
                Id = dto.Id,
                Content = new File { Id = dto.DocCode.ToInt() },
                Key = new Key { Id = dto.KeyDefCode.ToInt() },
                Value = value
            };
        }

        protected override IEnumerable<int> GetExists(IDbConnection conn)
        {
            return conn.Query<int>("select id from indexes");
        }

        protected override IEnumerable<KeysDto> GetDtos(IDbConnection connEcm6)
        {
            DapperHelper.UsingConnection(conn =>
            {
                this.keys = new Dictionary<int, int>();

                const string SelectKeys = @"
select 
    keys.id, 
    system_keys.type 
from 
    keys
inner join system_keys on (system_keys.id = keys.system_key_id)";

                var keysType = conn.Query<KeyWithSystemKey>(SelectKeys);

                foreach (var keyType in keysType)
                {
                    this.keys.Add(keyType.Id, keyType.Type);
                }
            });

            const string Sql = @"
select 
    CONVERT(INT, keys_code) Id,
    keydef_code KeyDefCode,
    doc_code DocCode,
    keys_desc Descricao
from 
    keys
order by
    keys_code";

            return connEcm6.Query<KeysDto>(Sql, buffered: false);
        }

        protected override DataTable CreateDataTable()
        {
            var dt = new DataTable("indexes");
            dt.Columns.Add(new DataColumn("id", typeof(int)));
            dt.Columns.Add(new DataColumn("content_id", typeof(int)));
            dt.Columns.Add(new DataColumn("key_id", typeof(int)));
            dt.Columns.Add(new DataColumn("value"));

            foreach (var entity in this.entities)
            {
                var row = dt.NewRow();

                row["id"] = entity.Id;
                row["content_id"] = this.GetId(entity.Content);
                row["key_id"] = this.GetId(entity.Key);
                row["value"] = entity.Value;

                dt.Rows.Add(row);
            }

            return dt;
        }

        public class KeyWithSystemKey
        {
            public int Id
            {
                get;
                set;
            }

            public byte Type
            {
                get;
                set;
            }
        }
    }
}