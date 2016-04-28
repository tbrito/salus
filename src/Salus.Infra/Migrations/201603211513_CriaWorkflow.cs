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

            Create.Index("idx_workflow_1").OnTable("workflow").OnColumn("criadoem");
            Create.Index("idx_workflow_2").OnTable("workflow").OnColumn("de");
            Create.Index("idx_workflow_3").OnTable("workflow").OnColumn("para");
            Create.Index("idx_workflow_4").OnTable("workflow").OnColumn("documento_id");
        }

        public override void Down()
        {
        }
    }
}
