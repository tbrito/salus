namespace Salus.Infra.Migrations
{
    using FluentMigrator;

    [Migration(201603181119)]
    public class CriaChaves : Migration
    {
        public override void Up()
        {
            Create.Table("chaves")
               .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
               .WithColumn("tipodocumento_id").AsInt32()
               .WithColumn("nome").AsAnsiString(254)
               .WithColumn("obrigatorio").AsBoolean()
               .WithColumn("tipodado").AsInt32()
               .WithColumn("mascara").AsAnsiString(2000).Nullable()
               .WithColumn("itens").AsAnsiString(2000).Nullable();

            Create.Index("idx_chaves_1").OnTable("chaves").OnColumn("nome");
            Create.Index("idx_chaves_2").OnTable("chaves").OnColumn("tipodocumento_id");
        }

        public override void Down()
        {
        }
    }
}
