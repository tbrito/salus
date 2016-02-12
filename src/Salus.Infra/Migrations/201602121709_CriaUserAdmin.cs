namespace Salus.Infra.Migrations
{
    using FluentMigrator;

    [Migration(201602121709)]
    public class CriaUserAdmin : Migration
    {
        public override void Up()
        {
            this.Insert.IntoTable("usuario")
                .Row(new
                {
                    nome = "admin",
                    senha = "pwd123",
                    email = "admin@admin.com"
                });
        }

        public override void Down()
        {
        }
    }
}
