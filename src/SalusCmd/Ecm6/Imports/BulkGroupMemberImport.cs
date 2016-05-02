namespace Veros.Ecm.DataAccess.Tarefas.Ecm6.Imports
{
    using System.Collections.Generic;
    using System.Data;
    using Dapper;
    using Model.Entities;

    public class BulkGroupMemberImport : BulkImport<UsersAreaDto, GroupMember>
    {        
        protected override string TableName
        {
            get
            {
                return "group_members";
            }
        }

        protected override GroupMember ConvertDtoToEntity(UsersAreaDto dto)
        {
            return new GroupMember()
            {
                Id = dto.Id,
                Group = this.Create<Area>(dto.Area),
                User = this.Create<User>(dto.Id.ToString())
            };
        }

        protected override IEnumerable<int> GetExists(IDbConnection conn)
        {
            return conn.Query<int>("select id from group_members");
        }

        protected override IEnumerable<UsersAreaDto> GetDtos(IDbConnection connEcm6)
        {
            const string Sql = @"
select
    CONVERT(INT, usr_code) Id,
    area_code Area
from 
    usr
where 
    usr_type = 'U'
order by
    usr_code";

            return connEcm6.Query<UsersAreaDto>(Sql, buffered: false);
        }

        protected override DataTable CreateDataTable()
        {
            var dt = new DataTable("group_members");
            dt.Columns.Add(new DataColumn("id", typeof(int)));
            dt.Columns.Add(new DataColumn("group_id", typeof(int)));
            dt.Columns.Add(new DataColumn("user_id", typeof(int)));

            foreach (var entity in this.entities)
            {
                var row = dt.NewRow();

                row["id"] = entity.Id;
                row["group_id"] = this.GetId(entity.Group);
                row["user_id"] = this.GetId(entity.User);

                dt.Rows.Add(row);
            }

            return dt;
        }
    }
}