using Microsoft.Data.Entity;

namespace Web.Models
{
   public class UserAdminDbContext : DbContext
    {
        public UserAdminDbContext()
        {
        }

        public DbSet<UserAdmin> UserAdmin
        {
            get;
            set;
        }
    }
}
