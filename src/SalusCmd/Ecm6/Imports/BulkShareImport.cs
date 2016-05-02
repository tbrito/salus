namespace Veros.Ecm.DataAccess.Tarefas.Ecm6.Imports
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Dapper;
    using Model.Entities;
    using Veros.Framework;

    public class BulkShareImport : BulkImport<InflowDto, Model.Entities.Share>
    {        
        protected override string TableName
        {
            get
            {
                return "shares";
            }
        }

        protected override Model.Entities.Share ConvertDtoToEntity(InflowDto dto)
        {
            return new Model.Entities.Share
            {
                Id = dto.Id,
                Content = new File { Id = dto.DocCode.ToInt() },
                At = dto.Date1,
                By = this.Create<User>(dto.FromUsr),
                ToGroup = this.Create<Area>(dto.Area)
            };
        }

        protected override IEnumerable<int> GetExists(IDbConnection conn)
        {
            return conn.Query<int>("select id from shares");
        }

        protected override IEnumerable<InflowDto> GetDtos(IDbConnection connEcm6)
        {
            const string Sql = @"
select
    CONVERT(INT, inflow_code) Id,
    doc_code DocCode,
    inflow_area Area,
    inflow_fromusr FromUsr,
    inflow_date1 Date1,
    inflow_usr Usr
from 
    inflow
order by
    inflow_code";

            return connEcm6.Query<InflowDto>(Sql, buffered: false);
        }

        protected override DataTable CreateDataTable()
        {
            var dt = new DataTable("shares");
            dt.Columns.Add(new DataColumn("id", typeof(int)));
            dt.Columns.Add(new DataColumn("content_id", typeof(int)));
            dt.Columns.Add(new DataColumn("group_id", typeof(int)));
            dt.Columns.Add(new DataColumn("created_by", typeof(int)));
            dt.Columns.Add(new DataColumn("created_at", typeof(DateTime)));

            foreach (var entity in this.entities)
            {
                var row = dt.NewRow();

                row["id"] = entity.Id;
                row["content_id"] = this.GetId(entity.Content);
                row["group_id"] = this.GetId(entity.ToGroup);
                row["created_by"] = this.GetId(entity.By);
                row["created_at"] = entity.At;

                dt.Rows.Add(row);
            }

            return dt;
        }
    }
}