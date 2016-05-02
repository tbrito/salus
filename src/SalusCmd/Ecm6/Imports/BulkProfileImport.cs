namespace Veros.Ecm.DataAccess.Tarefas.Ecm6.Imports
{
    using System.Collections.Generic;
    using System.Data;
    using Dapper;
    using Model.Entities;
    using Veros.Framework;

    public class BulkProfileImport : BulkImport<UserGroupDto, Profile>
    {
        protected override string TableName
        {
            get
            {
                return "profiles";
            }
        }

        protected override Profile ConvertDtoToEntity(UserGroupDto dto)
        {
            return new Profile
            {
                Id = dto.Id,
                Name = dto.Name.ToPascalCase()
            };
        }

        protected override IEnumerable<int> GetExists(IDbConnection conn)
        {
            return conn.Query<int>("select id from profiles");
        }

        protected override IEnumerable<UserGroupDto> GetDtos(IDbConnection connEcm6)
        {
            const string Sql = @"
select
    CONVERT(INT, usr_code) Id,
    usr_name Name
from 
    usr
where 
    usr_type = 'G'";

            return connEcm6.Query<UserGroupDto>(Sql, buffered: false);
        }

        protected override DataTable CreateDataTable()
        {
            var dt = new DataTable("profiles");
            dt.Columns.Add(new DataColumn("id", typeof(int)));
            dt.Columns.Add(new DataColumn("name"));

            foreach (var entity in this.entities)
            {
                var row = dt.NewRow();

                row["id"] = entity.Id;
                row["name"] = entity.Name;

                dt.Rows.Add(row);
            }

            return dt;
        }
    }
}