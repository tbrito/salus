namespace Salus.Infra.Migrations
{
    using FluentMigrator;

    [Migration(201603181112)]
    public class CriaTipoDocumento : Migration
    {
        public override void Up()
        {
            Create.Table("tipodocumento")
               .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
               .WithColumn("nome").AsAnsiString(254)
               .WithColumn("ativo").AsBoolean()
               .WithColumn("ehpasta").AsBoolean();
        }

        public override void Down()
        {
        }
    }
}
