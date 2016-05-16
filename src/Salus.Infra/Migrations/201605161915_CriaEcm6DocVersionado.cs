namespace Salus.Infra.Migrations
{
    using FluentMigrator;

    [Migration(201605161915)]
    public class CriaEcm6DocVersionado : Migration
    {
        public override void Up()
        {
            
            Create.Table("ecm6_docversionado")
               .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
               .WithColumn("ecm6_id").AsInt32()
               .WithColumn("ecm8_id").AsInt32()
               .WithColumn("import_status").AsInt32();
        }

        public override void Down()
        {
        }
    }
}
