namespace Salus.Infra.Migrations
{
    using FluentMigrator;

    [Migration(201602120907)]
    public class CriaArea : Migration
    {
        public override void Up()
        {
            Create.Table("area")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("nome").AsAnsiString();

            Create.Index("idx_area_1").OnTable("area").OnColumn("nome");
        }

        public override void Down()
        {
            Delete.Table("area");
        }
    }
}
