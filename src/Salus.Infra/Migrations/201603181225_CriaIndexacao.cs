namespace Salus.Infra.Migrations
{
    using FluentMigrator;

    [Migration(201603181225)]
    public class CriaIndexacao : Migration
    {
        public override void Up()
        {
            Create.Table("indexacao")
               .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
               .WithColumn("chave_id").AsInt32()
               .WithColumn("documento_id").AsInt32()
               .WithColumn("valor").AsAnsiString(2000);

            Create.Index("idx_indexacao_1").OnTable("indexacao").OnColumn("chave_id");
            Create.Index("idx_indexacao_2").OnTable("indexacao").OnColumn("documento_id");
            Create.Index("idx_indexacao_3").OnTable("indexacao").OnColumn("valor");
        }

        public override void Down()
        {
        }
    }
}
