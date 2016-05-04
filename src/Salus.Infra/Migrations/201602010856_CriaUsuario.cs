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
                .WithColumn("nome_completo").AsAnsiString()
                .WithColumn("email").AsAnsiString()
                .WithColumn("senha").AsAnsiString()
                .WithColumn("area_id").AsInt32().Nullable()
                .WithColumn("perfil_id").AsInt32().Nullable()
                .WithColumn("expira").AsBoolean().Nullable()
                .WithColumn("ativo").AsBoolean().Nullable()
                .WithColumn("motivo_inatividade").AsAnsiString(1024).Nullable()
                .WithColumn("expira_em").AsDateTime().Nullable();

            Create.Index("idx_usuario_1").OnTable("usuario").OnColumn("nome");
            Create.Index("idx_usuario_2").OnTable("usuario").OnColumn("area_id");
            Create.Index("idx_usuario_3").OnTable("usuario").OnColumn("perfil_id");
            Create.Index("idx_usuario_4").OnTable("usuario").OnColumn("ativo");
        }

        public override void Down()
        {
            Delete.Table("Usuario");
        }
    }
}
