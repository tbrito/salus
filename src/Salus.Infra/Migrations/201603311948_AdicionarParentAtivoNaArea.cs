namespace Salus.Infra.Migrations
{
    using FluentMigrator;

    [Migration(201603311948)]
    public class AdicionarParentAtivoNaArea : Migration
    {
        public override void Up()
        {
            Alter.Table("area").AddColumn("ativo").AsBoolean().Nullable();
            Alter.Table("area").AddColumn("parent_id").AsInt32().Nullable();
            Alter.Table("area").AddColumn("abreviacao").AsAnsiString(254).Nullable();
            Alter.Table("area").AddColumn("segura").AsInt32().Nullable();

            Create.Index("idx_area_2").OnTable("area").OnColumn("ativo");
            Create.Index("idx_area_3").OnTable("area").OnColumn("parent_id");
            Create.Index("idx_area_4").OnTable("area").OnColumn("segura");
        }

        public override void Down()
        {
        }
    }
}
