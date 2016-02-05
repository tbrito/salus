using FluentMigrator;

namespace Salus.Infra.Migrations
{
    [Migration(201602051040)]
    public class CriarAspNetUserClaims : Migration
    {
        public override void Up()
        {
            Create.Table("AspNetUserClaims")
                .WithColumn("Id").AsInt32().NotNullable().Identity()
                .WithColumn("ClaimType").AsAnsiString()
                .WithColumn("ClaimValue").AsAnsiString()
                .WithColumn("UserId").AsAnsiString();

            Create.ForeignKey("FK_AspNetUserClaims_User")
                .FromTable("AspNetUserClaims")
                .ForeignColumn("UserId")
                .ToTable("AspNetUsers")
                .PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.Table("AspNetUserClaims");
        }
    }
}
