namespace Veros.Ecm.DataAccess.Tarefas.Ecm6.Imports
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Dapper;
    using Veros.Ecm.Model.Entities;
    using Veros.Framework;

    internal class BulkAreaImport : BulkImport<AreaDto, Area>
    {
        private DataTable dtgroups;

        protected override string TableName
        {
            get
            {
                return "areas";
            }
        }

        protected override IEnumerable<int> GetExists(IDbConnection conn)
        {
            return conn.Query<int>("select id from areas");
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
            var dt = new DataTable("areas");
            dt.Columns.Add(new DataColumn("id", typeof(int)));
            dt.Columns.Add(new DataColumn("group_id", typeof(int)));
            dt.Columns.Add(new DataColumn("parent_id", typeof(int)));
            dt.Columns.Add(new DataColumn("is_restricted_view", typeof(bool)));
            dt.Columns.Add(new DataColumn("abbreviation"));

            this.dtgroups = new DataTable("groups");
            this.dtgroups.Columns.Add(new DataColumn("id", typeof(int)));
            this.dtgroups.Columns.Add(new DataColumn("name"));

            foreach (var entity in this.entities)
            {
                var row = dt.NewRow();

                row["id"] = entity.Id;
                row["group_id"] = entity.Id;
                row["parent_id"] = this.GetId(entity.Parent);
                row["is_restricted_view"] = entity.IsRestrictedView;
                row["abbreviation"] = entity.Abbreviation;

                dt.Rows.Add(row);

                var rowgroups = this.dtgroups.NewRow();
                rowgroups["id"] = entity.Id;
                rowgroups["name"] = entity.Nome;

                this.dtgroups.Rows.Add(rowgroups);
            }

            return dt;
        }

        protected override void AfterExecute()
        {
            this.BulkCopy(this.dtgroups, "groups");
        }

        protected override Area ConvertDtoToEntity(AreaDto dto)
        {
            return new Area
            {
                Id = dto.Id,
                Parent = this.entities.FirstOrDefault(x => x.Id == dto.Parent.ToInt()),
                Nome = dto.Descricao.ToPascalCase(),
                IsRestrictedView = dto.Restricted == "S",
                Abbreviation = dto.Abrev.ToPascalCase()
            };
        }
    }
}