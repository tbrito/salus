namespace Salus.Infra.Migrations
{
    using FluentMigrator;

    [Migration(201604251711)]
    public class AdicionarCpfStatusSearchNoDocumento : Migration
    {
        public override void Up()
        {
            Alter.Table("documento").AddColumn("cpfcnpj").AsAnsiString(30).Nullable();
            Alter.Table("documento").AddColumn("search_status").AsInt32().Nullable();
        }

        public override void Down()
        {
        }
    }
}
