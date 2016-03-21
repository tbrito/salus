namespace Salus.Infra.Migrations
{
    using FluentMigrator;

    [Migration(201603211513)]
    public class CriaWorkflow : Migration
    {
        public override void Up()
        {
            Create.Table("workflow")
               .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
               .WithColumn("criadoem").AsDateTime()
               .WithColumn("finalizadoem").AsDateTime().Nullable()
               .WithColumn("de").AsInt32()
               .WithColumn("para").AsInt32()
               .WithColumn("documento_id").AsInt32()
               .WithColumn("mensagem").AsAnsiString(2000)
               .WithColumn("status").AsInt32()
               .WithColumn("lido").AsBoolean();
        }

        public override void Down()
        {
        }
    }
}
