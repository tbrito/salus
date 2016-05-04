namespace SalusCmd.Ecm6.Imports
{
    using System.Collections.Generic;
    using System.Data;
    using Dapper;
    using Salus.Model.Entidades;
    using Salus.Infra.Extensions;
    using Salus.Model;

    public class BulkIndexImport : BulkImport<KeysDto, Indexacao>
    {
        private Dictionary<int, int> keys;

        protected override string TableName
        {
            get
            {
                return "indexacao";
            }
        }

        protected override Indexacao ConvertDtoToEntity(KeysDto dto)
        {
            var value = dto.Descricao;
            var keyId = dto.KeyDefCode.ToInt();

            if (this.keys.ContainsKey(keyId))
            {
                var type = this.keys[keyId];
                if (type == TipoDado.CpfCnpj.Value)
                {
                    value = value.RemoveCaracteresEspeciais();
                }
            }
            
            return new Indexacao
            {
                Id = dto.Id,
                Documento = new Documento { Id = dto.DocCode.ToInt() },
                Chave = new Chave { Id = dto.KeyDefCode.ToInt() },
                Valor = value,
            };
        }

        protected override IEnumerable<int> GetExists(IDbConnection conn)
        {
            return conn.Query<int>("select id from indexacao");
        }

        protected override IEnumerable<KeysDto> GetDtos(IDbConnection connEcm6)
        {
            DapperHelper.UsingConnection(conn =>
            {
                this.keys = new Dictionary<int, int>();

                const string SelectKeys = @"
select 
    Id, 
    TipoDado
from 
    chaves";

                var keysType = conn.Query<KeyWithSystemKey>(SelectKeys);

                foreach (var keyType in keysType)
                {
                    this.keys.Add(keyType.Id, keyType.TipoDado);
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
            var dt = new DataTable("indexacao");
            dt.Columns.Add(new DataColumn("id", typeof(int)));
            dt.Columns.Add(new DataColumn("documento_id", typeof(int)));
            dt.Columns.Add(new DataColumn("chave_id", typeof(int)));
            dt.Columns.Add(new DataColumn("valor"));

            foreach (var entity in this.entities)
            {
                var row = dt.NewRow();

                row["id"] = entity.Id;
                row["documento_id"] = this.GetId(entity.Documento);
                row["chave_id"] = this.GetId(entity.Chave);
                row["valor"] = entity.Valor;

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

            public int TipoDado
            {
                get;
                set;
            }
        }
    }
}