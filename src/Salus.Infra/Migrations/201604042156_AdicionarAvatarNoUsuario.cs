namespace Salus.Infra.Migrations
{
    using FluentMigrator;

    [Migration(201604042156)]
    public class AdicionarAvatarNoUsuario : Migration
    {
        public override void Up()
        {
            Alter.Table("usuario").AddColumn("avatar").AsAnsiString(512).Nullable();
        }

        public override void Down()
        {
        }
    }
}
