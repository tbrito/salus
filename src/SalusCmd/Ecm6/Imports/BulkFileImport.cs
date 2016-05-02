namespace Veros.Ecm.DataAccess.Tarefas.Ecm6.Imports
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Dapper;
    using Model.Entities;
    using Veros.Framework;
    using Veros.Ecm.Model.Enums;

    public class BulkFileImport : BulkImport<FileDto, File>
    {
        protected override string TableName
        {
            get
            {
                return "contents";
            }
        }

        protected override File ConvertDtoToEntity(FileDto dto)
        {
            var file = new File();

            file.Id = dto.Id.ToInt();
            file.CreatedAt = dto.Date;

            if (string.IsNullOrEmpty(dto.Usr) == false)
            {
                file.CreatedBy = this.Create<User>(dto.Usr);
            }

            if (string.IsNullOrEmpty(dto.IndexUsr) == false)
            {
                file.IndexedBy = this.Create<User>(dto.IndexUsr);
            }

            if (dto.IndexDate != null)
            {
                file.IndexedAt = dto.IndexDate;
            }

            if (string.IsNullOrEmpty(dto.Extension) == false)
            {
                file.OriginalFileExtension = dto.Extension.ToLower();
            }

            file.SearchStatus = SearchStatus.DontIndex;
            file.TransformStatus = TransformStatus.DontNeed;

            if (this.GrupoDocumentoValido(dto))
            {
                file.Category = this.Create<Category>(dto.GroupDocCode);

                if (file.Category != null)
                {
                    file.Subject = this.Create<Category>(dto.GroupDocCode).Name;
                }
            }

            return file;
        }

        protected override IEnumerable<int> GetExists(IDbConnection conn)
        {
            return conn.Query<int>("select id from contents", conn);
        }

        protected override IEnumerable<FileDto> GetDtos(IDbConnection connEcm6)
        {
            const string Sql = @"
select
    CONVERT(INT, doc_code) Id,
	groupdoc_code GroupDocCode, 
	doc_usr Usr, 
	doc_date Date, 
	doc_index_usr IndexUsr, 
	doc_index_date IndexDate,
	doc_filetype Extension
from 
	doc
where
	doc_filetype <> '-IDX-'
order by
    doc_code";

            return connEcm6.Query<FileDto>(Sql, buffered: false);
        }

        protected override DataTable CreateDataTable()
        {
            var dt = new DataTable("contents");
            dt.Columns.Add(new DataColumn("id", typeof(int)));
            dt.Columns.Add(new DataColumn("subject"));
            dt.Columns.Add(new DataColumn("category_id", typeof(int)));
            dt.Columns.Add(new DataColumn("created_by", typeof(int)));
            dt.Columns.Add(new DataColumn("created_at", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("indexed_by", typeof(int)));
            dt.Columns.Add(new DataColumn("indexed_at", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("file_extension"));
            dt.Columns.Add(new DataColumn("type", typeof(int)));
            dt.Columns.Add(new DataColumn("search_status", typeof(int)));
            dt.Columns.Add(new DataColumn("transform_status", typeof(int)));

            foreach (var entity in this.entities)
            {
                var row = dt.NewRow();

                row["id"] = entity.Id;
                row["subject"] = entity.Subject;
                row["category_id"] = this.GetId(entity.Category);
                row["created_by"] = this.GetId(entity.CreatedBy);
                row["created_at"] = this.GetDate(entity.CreatedAt);
                row["indexed_by"] = this.GetId(entity.IndexedBy);
                row["indexed_at"] = this.GetDate(entity.IndexedAt);
                row["file_extension"] = entity.OriginalFileExtension;
                row["type"] = ContentType.File.ToInt();
                row["search_status"] = 0;
                row["transform_status"] = 0;

                dt.Rows.Add(row);
            }

            return dt;
        }

        private bool GrupoDocumentoValido(FileDto fileDto)
        {
            return string.IsNullOrEmpty(fileDto.GroupDocCode) == false &&
                fileDto.GroupDocCode.ToLower() != "null";
        }
    }
}