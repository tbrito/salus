namespace SalusCmd.Ecm6.Imports
{
    using System.Collections.Generic;
    using System.Data;
    using Dapper;
    using Salus.Model.Entidades;

    public class BulkPerfilImport : BulkImport<UserGroupDto, Perfil>
    {
        protected override string TableName
        {
            get
            {
                return "perfil";
            }
        }

        protected override Perfil ConvertDtoToEntity(UserGroupDto dto)
        {
            return new Perfil
            {
                Id = dto.Id,
                Nome = ConvertString.ToPascalCase(dto.Name)
            };
        }

        protected override IEnumerable<int> GetExists(IDbConnection conn)
        {
            return conn.Query<int>("select id from perfil");
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
            var dt = new DataTable("perfil");
            dt.Columns.Add(new DataColumn("id", typeof(int)));
            dt.Columns.Add(new DataColumn("nome"));
            dt.Columns.Add(new DataColumn("ativo", typeof(bool)));

            foreach (var entity in this.entities)
            {
                var row = dt.NewRow();

                row["id"] = entity.Id;
                row["nome"] = entity.Nome;
                row["ativo"] = false;

                dt.Rows.Add(row);
            }

            return dt;
        }
    }
}