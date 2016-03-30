namespace Salus.Infra.Migrations
{
    using FluentMigrator;

    [Migration(201603301829)]
    public class AdicionarAtivoNaChave : Migration
    {
        public override void Up()
        {
            Alter.Table("chaves").AddColumn("ativo").AsBoolean().Nullable();
        }

        public override void Down()
        {
        }
    }
}
