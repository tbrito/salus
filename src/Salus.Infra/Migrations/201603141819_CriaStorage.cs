namespace Salus.Infra.Migrations
{
    using FluentMigrator;

    [Migration(201603141819)]
    public class CriaStorage : Migration
    {
        public override void Up()
        {
            Create.Table("gridstorage")
               .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
               .WithColumn("mongoid").AsAnsiString(1024)
               .WithColumn("salusid").AsAnsiString(1024);

            Create.Index("idx_gridstorage_1").OnTable("gridstorage").OnColumn("mongoid");
            Create.Index("idx_gridstorage_2").OnTable("gridstorage").OnColumn("salusid");
        }

        public override void Down()
        {
        }
    }
}
