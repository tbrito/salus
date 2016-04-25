namespace Salus.Infra.Seeds
{
    using FluentMigrator;
    using Model.Servicos;

    [Migration(201602121709)]
    public class CriaUserAdmin : Migration
    {
        public override void Up()
        {
            this.Insert.IntoTable("usuario")
                .Row(new
                {
                    nome = "admin",
                    senha = new HashString().Do("pwd123"),
                    email = "admin@admin.com",
                    expira = false,
                    ativo = false
                });
        }

        public override void Down()
        {
        }
    }
}
