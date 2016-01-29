namespace Web.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.Data.Entity.Migrations;
    using Models;

    public partial class CreateUserAdmin : Migration
    {
        private UserManager<ApplicationUser> userManager;

        public CreateUserAdmin(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        protected override async void Up(MigrationBuilder migrationBuilder)
        {
            var userAdmin = new ApplicationUser();

            userAdmin.UserName = "admin";
            var result = await this.userManager.CreateAsync(userAdmin, "pwd");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("AspNetRoleClaims");
        }
    }
}
