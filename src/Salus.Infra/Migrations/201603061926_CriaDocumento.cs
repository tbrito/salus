namespace Salus.Infra.Migrations
{
    using FluentMigrator;

    [Migration(201603061926)]
    public class CriaDocumento : Migration
    {
        public override void Up()
        {
            Create.Table("documento")
               .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
               .WithColumn("criadoem").AsDateTime()
               .WithColumn("assunto").AsAnsiString(1024)
               .WithColumn("tamanho").AsInt64();
        }

        public override void Down()
        {
        }
    }
}
