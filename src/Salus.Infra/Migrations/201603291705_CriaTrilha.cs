namespace Salus.Infra.Migrations
{
    using FluentMigrator;

    [Migration(201603291705)]
    public class CriaTrilha : Migration
    {
        public override void Up()
        {
            Create.Table("trilha")
               .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
               .WithColumn("data").AsDateTime()
               .WithColumn("descricao").AsAnsiString(512)
               .WithColumn("tipo").AsInt32()
               .WithColumn("recurso").AsAnsiString(1024)
               .WithColumn("user_id").AsInt32();

            Create.Index("idx_trilha_1").OnTable("trilha").OnColumn("data");
            Create.Index("idx_trilha_2").OnTable("trilha").OnColumn("tipo");
            Create.Index("idx_trilha_3").OnTable("trilha").OnColumn("user_id");
            Create.Index("idx_trilha_4").OnTable("trilha").OnColumn("recurso");
        }

        public override void Down()
        {
        }
    }
}
