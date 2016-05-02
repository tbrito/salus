namespace Veros.Ecm.DataAccess.Tarefas.Ecm6.Imports
{
    using System.Collections.Generic;
    using System.Data;
    using Dapper;
    using Model.Entities;
    using Veros.Framework;

    public class BulkUserImport : BulkImport<UserDto, User>
    {
        protected override string TableName
        {
            get
            {
                return "users";
            }
        }

        protected override User ConvertDtoToEntity(UserDto dto)
        {
            return new User
            {
                Id = dto.Id,
                Nome = dto.Name.ToPascalCase(),
                Login = dto.Login,
                IsActive = dto.Active == "S" && dto.Deleted != "*",
                Profile = this.Create<Profile>(dto.Profile),
                Senha = "123456".Hash()
            };
        }

        protected override IEnumerable<int> GetExists(IDbConnection conn)
        {
            return conn.Query<int>("select id from users");
        }

        protected override IEnumerable<UserDto> GetDtos(IDbConnection connEcm6)
        {
            const string Sql = @"
select
    CONVERT(INT, usr_code) Id,
    usr_name Name, 
    usr_active Active, 
    USR_NETWORKLOGIN Login, 
    usr_deleted Deleted,
    usr_group Profile
from 
    usr
where 
    usr_type = 'U'
order by
    usr_code";

            return connEcm6.Query<UserDto>(Sql, buffered: false);
        }

        protected override DataTable CreateDataTable()
        {
            var dt = new DataTable("users");
            dt.Columns.Add(new DataColumn("id", typeof(int)));
            dt.Columns.Add(new DataColumn("name"));
            dt.Columns.Add(new DataColumn("login"));
            dt.Columns.Add(new DataColumn("is_active", typeof(bool)));
            dt.Columns.Add(new DataColumn("profile_id", typeof(int)));
            dt.Columns.Add(new DataColumn("password"));

            foreach (var entity in this.entities)
            {
                var row = dt.NewRow();

                row["id"] = entity.Id;
                row["name"] = entity.Nome;
                row["login"] = entity.Login;
                row["is_active"] = entity.IsActive;
                row["profile_id"] = this.GetId(entity.Profile);
                row["password"] = entity.Senha;

                dt.Rows.Add(row);
            }

            return dt;
        }
    }
}