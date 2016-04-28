namespace Salus.Infra.Migrations
{
    using FluentMigrator;

    [Migration(201602120910)]
    public class CriaPerfil : Migration
    {
        public override void Up()
        {
            Create.Table("perfil")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("nome").AsAnsiString();

            Create.Index("idx_perfil_1").OnTable("perfil").OnColumn("nome");
        }

        public override void Down()
        {
            Delete.Table("perfil");
        }
    }
}
