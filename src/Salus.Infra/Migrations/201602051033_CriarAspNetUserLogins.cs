using FluentMigrator;

namespace Salus.Infra.Migrations
{
    [Migration(201602051033)]
    public class CriarAspNetUserLogins : Migration
    {
        public override void Up()
        {
            Create.Table("AspNetUserLogins")
                .WithColumn("UserId").AsAnsiString().NotNullable()
                .WithColumn("LoginProvider").AsAnsiString()
                .WithColumn("ProviderKey").AsAnsiString();

            Create.ForeignKey("FK_AspNetUserLogins_User")
                .FromTable("AspNetUserLogins")
                .ForeignColumn("UserId")
                .ToTable("AspNetUsers")
                .PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.Table("AspNetUserLogins");
        }
    }
}
