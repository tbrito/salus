namespace Salus.Infra.Migrations
{
    using FluentMigrator;

    [Migration(201605041545)]
    public class CriaEcm6PreDoc : Migration
    {
        public override void Up()
        {
            Create.Table("ecm6_predoc")
               .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
               .WithColumn("ecm6_id").AsInt32()
               .WithColumn("ecm8_id").AsInt32();
        }

        public override void Down()
        {
        }
    }
}
