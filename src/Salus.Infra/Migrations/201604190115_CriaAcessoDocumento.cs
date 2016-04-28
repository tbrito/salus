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

            Create.Index("idx_accdoc_1").OnTable("acessodocumento").OnColumn("papel_id");
            Create.Index("idx_accdoc_2").OnTable("acessodocumento").OnColumn("autor_id");
            Create.Index("idx_accdoc_3").OnTable("acessodocumento").OnColumn("tipodocumento_id");
        }

        public override void Down()
        {
        }
    }
}
