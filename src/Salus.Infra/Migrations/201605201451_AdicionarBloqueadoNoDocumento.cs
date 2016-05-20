namespace Salus.Infra.Migrations
{
    using FluentMigrator;

    [Migration(201605201451)]
    public class AdicionarBloqueadoNoDocumento : Migration
    {
        public override void Up()
        {
            Alter.Table("documento").AddColumn("bloqueado").AsBoolean().Nullable();
            
            Create.Index("idx_documento_9").OnTable("documento").OnColumn("bloqueado");
        }

        public override void Down()
        {
        }
    }
}
