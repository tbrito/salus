namespace Salus.Infra.Migrations
{
    using FluentMigrator;

    [Migration(201602010856)]
    public class CriaUsuario : Migration
    {
        public override void Up()
        {
            Create.Table("usuario")
                .WithColumn("id").AsAnsiString().NotNullable().PrimaryKey().Identity()
                .WithColumn("nome").AsAnsiString()
                .WithColumn("email").AsAnsiString()
                .WithColumn("senha").AsAnsiString();
        }

        public override void Down()
        {
            Delete.Table("Usuario");
        }
    }
}
