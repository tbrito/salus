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
        }

        public override void Down()
        {
        }
    }
}
