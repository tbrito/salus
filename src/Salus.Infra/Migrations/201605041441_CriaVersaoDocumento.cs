namespace Salus.Infra.Migrations
{
    using FluentMigrator;

    [Migration(201605041441)]
    public class CriaVersaoDocumento : Migration
    {
        public override void Up()
        {
            Create.Table("versaodocumento")
               .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
               .WithColumn("documento_id").AsInt32()
               .WithColumn("usuario_id").AsInt32()
               .WithColumn("criadoem").AsDateTime()
               .WithColumn("comentario").AsAnsiString(2048)
               .WithColumn("versao").AsInt32();
        }

        public override void Down()
        {
        }
    }
}
