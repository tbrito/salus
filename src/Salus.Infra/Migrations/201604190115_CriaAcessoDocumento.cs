namespace Salus.Infra.Migrations
{
    using FluentMigrator;

    [Migration(201604190115)]
    public class CriaAcessoDocumento : Migration
    {
        public override void Up()
        {
            Create.Table("acessodocumento")
               .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
               .WithColumn("papel_id").AsInt32()
               .WithColumn("autor_id").AsInt32()
               .WithColumn("tipodocumento_id").AsInt32();
        }

        public override void Down()
        {
        }
    }
}
