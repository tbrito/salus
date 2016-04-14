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
        }

        public override void Down()
        {
        }
    }
}
