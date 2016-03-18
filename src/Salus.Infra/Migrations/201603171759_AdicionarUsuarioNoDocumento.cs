namespace Salus.Infra.Migrations
{
    using FluentMigrator;

    [Migration(201603171759)]
    public class AdicionarUsuarioNoDocumento : Migration
    {
        public override void Up()
        {
            Alter.Table("documento").AddColumn("user_id").AsInt32();
            Alter.Table("documento").AddColumn("tipodocumento_id").AsInt32();
        }

        public override void Down()
        {
        }
    }
}
