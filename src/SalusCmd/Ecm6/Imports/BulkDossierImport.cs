namespace Veros.Ecm.DataAccess.Tarefas.Ecm6.Imports
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Dapper;
    using Veros.Framework;
    using Veros.Ecm.Model.Enums;

    public class BulkDossierImport : BulkImport<DossierDto, DossierInsertDto>
    {
        protected override string TableName
        {
            get
            {
                return "contents";
            }
        }

        protected override DossierInsertDto ConvertDtoToEntity(DossierDto dto)
        {
            return new DossierInsertDto
            {
                Id = dto.Id,
                CreatedAt = dto.Date,
                Subject = "(Sem Assunto)",
                ContentType = this.GetContentType(dto.GroupDoc)
            };
        }

        protected override IEnumerable<int> GetExists(IDbConnection conn)
        {
            return conn.Query<int>(
                "select id from contents where type in (@EmployeeDossier, @CustomerDossier, 0)",
                new
                {
                    EmployeeDossier = ContentType.EmployeeDossier.ToInt(),
                    CustomerDossier = ContentType.CustomerDossier.ToInt()
                });
        }

        protected override IEnumerable<DossierDto> GetDtos(IDbConnection connEcm6)
        {
            const string Sql = @"
select 
    CONVERT(INT, doc_code) Id,
    doc_date Date,
    CONVERT(INT, groupdoc_code) GroupDoc
from 
    doc
where
    doc_filetype = '-IDX-'
order by
    doc_code";

            return connEcm6.Query<DossierDto>(Sql, buffered: false);
        }

        protected override DataTable CreateDataTable()
        {
            var dt = new DataTable("contents");
            dt.Columns.Add(new DataColumn("id", typeof(int)));
            dt.Columns.Add(new DataColumn("subject"));
            dt.Columns.Add(new DataColumn("created_at", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("type", typeof(int)));
            dt.Columns.Add(new DataColumn("search_status", typeof(int)));
            dt.Columns.Add(new DataColumn("transform_status", typeof(int)));

            foreach (var entity in this.entities)
            {
                var row = dt.NewRow();

                row["id"] = entity.Id;
                row["subject"] = entity.Subject;
                row["created_at"] = this.GetDate(entity.CreatedAt);
                row["type"] = entity.ContentType.ToInt();
                row["search_status"] = 0;
                row["transform_status"] = 0;

                dt.Rows.Add(row);
            }

            return dt;
        }

        private ContentType GetContentType(int groupDoc)
        {
            if (groupDoc == 91)
            {
                return ContentType.EmployeeDossier;
            }

            if (groupDoc == 38)
            {
                return ContentType.CustomerDossier;
            }

            return ContentType.Unknow;
        }
    }
}