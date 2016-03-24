namespace Salus.Infra.Migrations
{
    using FluentMigrator;

    [Migration(201603240211)]
    public class AdicionarFiletypeMongoStorage : Migration
    {
        public override void Up()
        {
            Alter.Table("gridstorage").AddColumn("filetype").AsAnsiString(5);
        }

        public override void Down()
        {
        }
    }
}
