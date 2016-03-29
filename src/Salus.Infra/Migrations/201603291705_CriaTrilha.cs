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
               .WithColumn("data").AsInt32()
               .WithColumn("descricao").AsAnsiString(512)
               .WithColumn("tipo").AsInt32()
               .WithColumn("recurso").AsAnsiString(1024)
               .WithColumn("user_id").AsInt32();
        }

        public override void Down()
        {
        }
    }
}
