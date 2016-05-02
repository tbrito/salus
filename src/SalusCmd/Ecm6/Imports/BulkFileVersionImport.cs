namespace Veros.Ecm.DataAccess.Tarefas.Ecm6.Imports
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Dapper;
    using Model.Entities;
    using Veros.Framework;

    public class BulkFileVersionImport : BulkImport<ContentVersionDto, ContentVersion>
    {
        protected override string TableName
        {
            get
            {
                return "content_versions";
            }
        }

        protected override ContentVersion ConvertDtoToEntity(ContentVersionDto dto)
        {
            return new ContentVersion
            {
                Id = dto.Id,
                At = dto.Data,
                By = this.Create<User>(dto.UsrCode),
                Comments = dto.Obs,
                Content = this.Create<File>(dto.DocCode),
                Number = dto.Revisao.Split(new[] { '.' }, 1)[0].ToInt()
            };
        }

        protected override IEnumerable<int> GetExists(IDbConnection conn)
        {
            return conn.Query<int>("select id from content_versions");
        }

        protected override IEnumerable<ContentVersionDto> GetDtos(IDbConnection connEcm6)
        {
            const string Sql = @"
select 
    CONVERT(INT, vsdoc_code) Id,
    doc_code DocCode, 
    usr_code UsrCode, 
    vsdoc_data Data, 
    vsdoc_checkinobs Obs, 
    vsdoc_revisao Revisao
from 
    vsdoc
order by
    vsdoc_code";

            return connEcm6.Query<ContentVersionDto>(Sql, buffered: false);
        }

        protected override DataTable CreateDataTable()
        {
            var dt = new DataTable("content_versions");
            dt.Columns.Add(new DataColumn("id", typeof(int)));
            dt.Columns.Add(new DataColumn("created_at", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("created_by", typeof(int)));
            dt.Columns.Add(new DataColumn("content_id", typeof(int)));
            dt.Columns.Add(new DataColumn("comments"));
            dt.Columns.Add(new DataColumn("number", typeof(int)));

            foreach (var entity in this.entities)
            {
                var row = dt.NewRow();

                row["id"] = entity.Id;
                row["created_at"] = entity.At;
                row["created_by"] = this.GetId(entity.By);
                row["content_id"] = this.GetId(entity.Content);
                row["comments"] = entity.Comments;
                row["number"] = entity.Number;

                dt.Rows.Add(row);
            }

            return dt;
        }
    }
}