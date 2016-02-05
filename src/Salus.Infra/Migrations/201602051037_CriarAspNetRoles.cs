using FluentMigrator;

namespace Salus.Infra.Migrations
{
    [Migration(201602051037)]
    public class CriarAspNetRoles : Migration
    {
        public override void Up()
        {
            Create.Table("AspNetRoles")
                .WithColumn("UserId").AsString().NotNullable()
                .WithColumn("LoginProvider").AsAnsiString()
                .WithColumn("ProviderKey").AsAnsiString();

            Create.ForeignKey("FK_UserRoles_User")
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
