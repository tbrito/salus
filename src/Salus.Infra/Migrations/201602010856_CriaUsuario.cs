namespace Salus.Infra.Migrations
{
    using FluentMigrator;

    [Migration(201602010856)]
    public class CriaUsuario : Migration
    {
        public override void Up()
        {
            Create.Table("usuario")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("nome").AsAnsiString()
                .WithColumn("email").AsAnsiString()
                .WithColumn("senha").AsAnsiString()
                .WithColumn("area_id").AsInt32().Nullable()
                .WithColumn("perfil_id").AsInt32().Nullable()
                .WithColumn("expira").AsBoolean().Nullable()
                .WithColumn("ativo").AsBoolean().Nullable()
                .WithColumn("motivo_inatividade").AsAnsiString(1024).Nullable()
                .WithColumn("expira_em").AsDateTime().Nullable();
        }

        public override void Down()
        {
            Delete.Table("Usuario");
        }
    }
}
