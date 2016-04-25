namespace Salus.Infra.Migrations
{
    using FluentMigrator;

    [Migration(201604231602)]
    public class CriaConfiguracao : Migration
    {
        public override void Up()
        {
            Create.Table("configuracao")
               .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
               .WithColumn("chave").AsAnsiString(1024)
               .WithColumn("valor").AsAnsiString(2048);
        }

        public override void Down()
        {
        }
    }
}
