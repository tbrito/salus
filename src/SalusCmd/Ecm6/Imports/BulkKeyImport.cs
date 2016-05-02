namespace Veros.Ecm.DataAccess.Tarefas.Ecm6.Imports
{
    using System.Collections.Generic;
    using System.Data;
    using Dapper;
    using Model.Entities;

    public class BulkKeyImport : BulkImport<KeyDefDto, Key>
    {
        private readonly Dictionary<int, SystemKey> systemKeys;

        public BulkKeyImport(Dictionary<int, SystemKey> systemKeys)
        {
            this.systemKeys = systemKeys;
        }

        protected override string TableName
        {
            get
            {
                return "keys";
            }
        }

        protected override Key ConvertDtoToEntity(KeyDefDto dto)
        {
            return new Key
            {
                Id = dto.Id,
                Category = this.Create<Category>(dto.GroupDocCode),
                IsRequired = dto.Obrig == "S",
                SystemKey = this.systemKeys.GetById(dto.Id)
            };
        }

        protected override IEnumerable<int> GetExists(IDbConnection conn)
        {
            return conn.Query<int>("select id from keys");
        }

        protected override IEnumerable<KeyDefDto> GetDtos(IDbConnection connEcm6)
        {
            const string Sql = @"
select
    CONVERT(INT, keydef_code) Id,
    groupdoc_code GroupDocCode, 
    keydef_obrig Obrig,
    keydef_desc Descricao
from 
    keydef
order by
    keydef_code";

            return connEcm6.Query<KeyDefDto>(Sql, buffered: false);
        }

        protected override DataTable CreateDataTable()
        {
            var dt = new DataTable("keys");
            dt.Columns.Add(new DataColumn("id", typeof(int)));
            dt.Columns.Add(new DataColumn("category_id", typeof(int)));
            dt.Columns.Add(new DataColumn("system_key_id", typeof(int)));
            dt.Columns.Add(new DataColumn("is_required", typeof(bool)));

            foreach (var entity in this.entities)
            {
                var row = dt.NewRow();

                row["id"] = entity.Id;
                row["category_id"] = this.GetId(entity.Category);
                row["system_key_id"] = this.GetId(entity.SystemKey);
                row["is_required"] = entity.IsRequired;

                dt.Rows.Add(row);
            }

            return dt;
        }
    }
}