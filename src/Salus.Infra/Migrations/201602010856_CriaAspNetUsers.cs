namespace Salus.Infra.Migrations
{
    using FluentMigrator;

    [Migration(201602010856)]
    public class CriaAspNetUsers : Migration
    {
        public override void Up()
        {
            Create.Table("AspNetUsers")
                .WithColumn("Id").AsAnsiString().NotNullable().PrimaryKey()
                .WithColumn("AccessFailedCount").AsInt32()
                .WithColumn("Email").AsAnsiString()
                .WithColumn("EmailConfirmed").AsBoolean()
                .WithColumn("LockoutEnabled").AsBoolean()
                .WithColumn("LockoutEndDateUtc").AsDateTime()
                .WithColumn("PasswordHash").AsAnsiString()
                .WithColumn("PhoneNumber").AsAnsiString()
                .WithColumn("PhoneNumberConfirmed").AsBoolean()
                .WithColumn("TwoFactorEnabled ").AsBoolean()
                .WithColumn("UserName").AsAnsiString()
                .WithColumn("SecurityStamp").AsAnsiString();
        }

        public override void Down()
        {
            Delete.Table("AspNetUsers");
        }
    }
}
