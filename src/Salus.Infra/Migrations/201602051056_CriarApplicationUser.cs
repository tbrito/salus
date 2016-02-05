using FluentMigrator;

namespace Salus.Infra.Migrations
{
    [Migration(201602051056)]
    public class CriarApplicationUser : Migration
    {
        public override void Up()
        {
            Create.Table("ApplicationUser")
                .WithColumn("applicationuser_key").AsAnsiString().NotNullable().PrimaryKey();

            Create.ForeignKey("FK_ApplicationUser_User")
                .FromTable("ApplicationUser")
                .ForeignColumn("applicationuser_key")
                .ToTable("AspNetUsers")
                .PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.Table("ApplicationUser");
        }
    }
}
