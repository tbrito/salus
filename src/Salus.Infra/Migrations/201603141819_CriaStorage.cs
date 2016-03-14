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
               .WithColumn("documentoId").AsInt32()
               .WithColumn("mongoId").AsInt16();
        }

        public override void Down()
        {
        }
    }
}
