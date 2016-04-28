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
               .WithColumn("parent_id").AsInt32().Nullable()
               .WithColumn("ativo").AsBoolean()
               .WithColumn("ehpasta").AsBoolean();

            Create.Index("idx_tipodoc_1").OnTable("tipodocumento").OnColumn("nome");
            Create.Index("idx_tipodoc_2").OnTable("tipodocumento").OnColumn("parent_id");
            Create.Index("idx_tipodoc_3").OnTable("tipodocumento").OnColumn("ehpasta");
        }

        public override void Down()
        {
        }
    }
}
