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
        }

        public override void Down()
        {
        }
    }
}
