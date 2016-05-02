namespace Veros.Ecm.DataAccess.Tarefas.Ecm6.Imports
{
    using System.Collections.Generic;
    using System.Data;
    using Dapper;
    using Model.Entities;

    public class BulkPurgesImport : BulkImport<PurgeGroupDocDto, Purge>
    {
        protected override string TableName
        {
            get
            {
                return "purges";
            }
        }

        protected override IEnumerable<PurgeGroupDocDto> GetDtos(IDbConnection connEcm6)
        {
            const string Sql = @"
select 
    CONVERT(INT, groupdoc_code) Id,
    groupdoc_purge Months
from 
    groupdoc
where 
	groupdoc_purge > 0
order by
    groupdoc_code";

            return connEcm6.Query<PurgeGroupDocDto>(Sql, buffered: false);
        }

        protected override DataTable CreateDataTable()
        {
            var dt = new DataTable("purges");

            dt.Columns.Add(new DataColumn("id", typeof(int)));
            dt.Columns.Add(new DataColumn("category_id", typeof(int)));
            dt.Columns.Add(new DataColumn("use_system_key", typeof(bool)));
            dt.Columns.Add(new DataColumn("months", typeof(int)));

            var count = 1;

            foreach (var purge in this.entities)
            {
                var row = dt.NewRow();
                row["id"] = count++;
                row["category_id"] = purge.Id;
                row["use_system_key"] = purge.UseSystemKey;
                row["months"] = purge.Months;

                dt.Rows.Add(row);
            }

            return dt;
        }

        protected override Purge ConvertDtoToEntity(PurgeGroupDocDto dto)
        {
            return new Purge
            {
                Id = dto.Id,
                UseSystemKey = false,
                Months = dto.Months,
            };
        }

        protected override IEnumerable<int> GetExists(IDbConnection conn)
        {
            return conn.Query<int>("select id from purges", conn);
        }
    }
}