namespace Salus.Infra.Migrations
{
    using FluentMigrator;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using System.Text;

    [Migration(201602010856)]
    public class CriaUsuario : Migration
    {
        public override void Up()
        {
            Create.Table("usuario")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("nome").AsAnsiString().NotNullable()
                .WithColumn("login").AsAnsiString().NotNullable()
                .WithColumn("senha").AsAnsiString().NotNullable();

            var md5 = new MD5CryptoServiceProvider();
            var password = Encoding.ASCII.GetBytes("pwd");
            var hash = md5.ComputeHash(password);
            
            IDictionary<string, object> valores = new Dictionary<string, object>();
            valores.Add("nome", "Administrador de Sistema");
            valores.Add("login", "admin");
            valores.Add("senha", password.ToString());

            Insert.IntoTable("usuario").Row(valores);
        }

        public override void Down()
        {
            Delete.Table("usuario");
        }
    }
}
