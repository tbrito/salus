namespace SalusCmd.Ecm6.Imports
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Dapper;
    using Salus.Model.Entidades;
    using Salus.Infra.Extensions;

    internal class BulkAreaImport : BulkImport<AreaDto, Area>
    {
        private DataTable dtgroups;

        protected override string TableName
        {
            get
            {
                return "area";
            }
        }

        protected override IEnumerable<int> GetExists(IDbConnection conn)
        {
            return conn.Query<int>("select id from area");
        }

        protected override IEnumerable<AreaDto> GetDtos(IDbConnection connEcm6)
        {
            const string Sql = @"
select 
    CONVERT(INT, area_code) Id,
    area_desc Descricao, 
    area_abrev Abrev,
    area_parent Parent, 
    area_security Restricted
from 
    area
order by
    area_code";

            return connEcm6.Query<AreaDto>(Sql, buffered: false);
        }

        protected override DataTable CreateDataTable()
        {
            var dt = new DataTable("area");
            dt.Columns.Add(new DataColumn("id", typeof(int)));
            dt.Columns.Add(new DataColumn("nome"));
            dt.Columns.Add(new DataColumn("parent_id", typeof(int)));
            dt.Columns.Add(new DataColumn("segura", typeof(bool)));
            dt.Columns.Add(new DataColumn("abreviacao"));
            dt.Columns.Add(new DataColumn("ativo", typeof(bool)));

            foreach (var entity in this.entities)
            {
                var row = dt.NewRow();

                row["id"] = entity.Id;
                row["nome"] = entity.Nome;
                row["parent_id"] = this.GetId(entity.Parent);
                row["segura"] = entity.Segura;
                row["abreviacao"] = entity.Abreviacao;
                row["ativo"] = true;

                dt.Rows.Add(row);
            }

            return dt;
        }

        protected override Area ConvertDtoToEntity(AreaDto dto)
        {
            return new Area
            {
                Id = dto.Id,
                Parent = this.entities.FirstOrDefault(x => x.Id == dto.Parent.ToInt()),
                Nome = ConvertString.ToPascalCase(dto.Descricao),
                Segura = dto.Restricted == "S",
                Abreviacao = ConvertString.ToPascalCase(dto.Abrev)
            };
        }
    }
}