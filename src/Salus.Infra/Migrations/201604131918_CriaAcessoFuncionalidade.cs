namespace Salus.Infra.Migrations
{
    using FluentMigrator;

    [Migration(201604131918)]
    public class CriaAcessoFuncionalidade : Migration
    {
        public override void Up()
        {
            Create.Table("acessofuncionalidade")
               .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
               .WithColumn("papel_id").AsInt32()
               .WithColumn("autor_id").AsInt32()
               .WithColumn("funcionalidade_id").AsInt32();

            Create.Index("idx_accfunc_1").OnTable("acessofuncionalidade").OnColumn("papel_id");
            Create.Index("idx_accfunc_2").OnTable("acessofuncionalidade").OnColumn("autor_id");
            Create.Index("idx_accfunc_3").OnTable("acessofuncionalidade").OnColumn("funcionalidade_id");
        }

        public override void Down()
        {
        }
    }
}
