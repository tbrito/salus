namespace Salus.Infra.Migrations
{
    using FluentMigrator;

    [Migration(201603171759)]
    public class AdicionarUsuarioNoDocumento : Migration
    {
        public override void Up()
        {
            Alter.Table("documento").AddColumn("user_id").AsInt32();
            Alter.Table("documento").AddColumn("tipodocumento_id").AsInt32().Nullable();

            Create.Index("idx_documento_3").OnTable("documento").OnColumn("user_id");
            Create.Index("idx_documento_4").OnTable("documento").OnColumn("tipodocumento_id");
        }

        public override void Down()
        {
        }
    }
}
