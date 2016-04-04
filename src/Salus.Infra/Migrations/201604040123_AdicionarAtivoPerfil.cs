namespace Salus.Infra.Migrations
{
    using FluentMigrator;

    [Migration(201604040123)]
    public class AdicionarAtivoPerfil : Migration
    {
        public override void Up()
        {
            Alter.Table("perfil").AddColumn("ativo").AsBoolean().Nullable();
        }

        public override void Down()
        {
        }
    }
}
