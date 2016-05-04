namespace SalusCmd.Ecm6.Imports
{
    using System.Collections.Generic;
    using System.Data;
    using Dapper;
    using Salus.Model.Entidades;
    using Salus.Model.Servicos;

    public class BulkUsuarioImport : BulkImport<UserDto, Usuario>
    {
        protected override string TableName
        {
            get
            {
                return "usuario";
            }
        }

        protected override Usuario ConvertDtoToEntity(UserDto dto)
        {
            return new Usuario
            {
                Id = dto.Id,
                Login = ConvertString.ToPascalCase(dto.Login),
                Nome = dto.Name,
                Ativo = dto.Active == "S" && dto.Deleted != "*",
                Perfil = this.Create<Perfil>(dto.Profile),
                Area = this.Create<Area>(dto.Area),
                Senha = new HashString().Do("pwd123")
            };
        }

        protected override IEnumerable<int> GetExists(IDbConnection conn)
        {
            return conn.Query<int>("select id from usuario");
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
    usr_group Profile,
    area_code Area
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
            var dt = new DataTable("usuario");
            dt.Columns.Add(new DataColumn("id", typeof(int)));
            dt.Columns.Add(new DataColumn("nome"));
            dt.Columns.Add(new DataColumn("nome_completo"));
            dt.Columns.Add(new DataColumn("email"));
            dt.Columns.Add(new DataColumn("ativo", typeof(bool)));
            dt.Columns.Add(new DataColumn("perfil_id", typeof(int)));
            dt.Columns.Add(new DataColumn("area_id", typeof(int)));
            dt.Columns.Add(new DataColumn("senha"));

            foreach (var entity in this.entities)
            {
                var row = dt.NewRow();

                row["id"] = entity.Id;
                row["nome"] = entity.Login;
                row["nome_completo"] = entity.Nome;
                row["email"] = entity.Login + "@bbc.br";
                row["ativo"] = entity.Ativo;
                row["perfil_id"] = this.GetId(entity.Perfil);
                row["area_id"] = this.GetId(entity.Area);
                row["senha"] = entity.Senha;

                dt.Rows.Add(row);
            }

            return dt;
        }
    }
}