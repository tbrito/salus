namespace Web.Migrations
{
    using Microsoft.Data.Entity.Infrastructure;
    using Microsoft.Data.Entity.Migrations;
    using Web.Models;
    using Microsoft.Data.Entity;
    using Microsoft.Data.Entity.Metadata;

    [DbContext(typeof(UserAdminDbContext))]
    [Migration("201602011533_CreateUserAdmin")]
    public class CreateUserAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var user = new ApplicationUser { UserName = "admin", Email = "admin@admin.com" };
            var result = this._userManager.CreateAsync(user, "pwd");
        }

        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
               .HasAnnotation("ProductVersion", "7.0.0-beta8")
               .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
        }
    }
}