namespace Web.Migrations
{
    using Microsoft.Data.Entity;
    using Microsoft.Data.Entity.Infrastructure;
    using Microsoft.Data.Entity.Migrations;
    using Web.Models;

    [DbContext(typeof(ApplicationDbContext))]
    [Migration("00000000000001_CreateUserAdmin")]
    public partial class CreateUserAdmin
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
        }
    }
}
