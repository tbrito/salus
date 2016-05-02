namespace SalusCmd.Ecm6.Imports
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Dapper;
    using Salus.Model.Entidades;
    using Salus.Infra.Extensions;
    using SalusCmd.Ecm6;

    public class BulkTipoDocumentoImport : BulkImport<GroupDocDto, TipoDocumento>
    {
        protected override string TableName
        {
            get
            {
                return "tipodocumento";
            }
        }

        protected override IEnumerable<GroupDocDto> GetDtos(IDbConnection connEcm6)
        {
            const string Sql = @"
select 
    CONVERT(INT, groupdoc_code) Id,
    groupdoc_parent Parent, 
    groupdoc_active Active, 
    groupdoc_desc Descricao, 
    groupdoc_type Folder
from 
    groupdoc
order by
    groupdoc_code";

            return connEcm6.Query<GroupDocDto>(Sql, buffered: false);
        }

        protected override DataTable CreateDataTable()
        {
            var dt = new DataTable("tipodocumento");

            dt.Columns.Add(new DataColumn("id", typeof(int)));
            dt.Columns.Add(new DataColumn("nome"));
            dt.Columns.Add(new DataColumn("parent_id", typeof(int)));
            dt.Columns.Add(new DataColumn("ativo", typeof(bool)));
            dt.Columns.Add(new DataColumn("ehpasta", typeof(bool)));

            foreach (var tipoDocumento in this.entities)
            {
                var row = dt.NewRow();
                row["id"] = tipoDocumento.Id;
                row["nome"] = tipoDocumento.Nome;
                row["parent_id"] = this.GetId(tipoDocumento.Parent);
                row["ativo"] = tipoDocumento.Ativo;
                row["ehpasta"] = tipoDocumento.EhPasta;

                dt.Rows.Add(row);
            }

            return dt;
        }

        protected override TipoDocumento ConvertDtoToEntity(GroupDocDto dto)
        {
            return new TipoDocumento
            {
                Id = dto.Id,
                Nome = string.IsNullOrEmpty(dto.Descricao) ? "(vazio)" : ConvertString.ToPascalCase(dto.Descricao),
                Ativo = dto.Active == "S",
                Parent = this.entities.FirstOrDefault(x => x.Id == dto.Parent.ToInt()),
                EhPasta = dto.Folder == "G",
            };
        }

        protected override IEnumerable<int> GetExists(IDbConnection conn)
        {
            return conn.Query<int>("select id from tipodocumento", conn);
        }
    }
}