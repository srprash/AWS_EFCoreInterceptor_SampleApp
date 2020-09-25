using Microsoft.EntityFrameworkCore;

namespace InterceptorExample
{
    public class User
    {
        public int UserId { get; set; }
    }

    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
            this.Database.Migrate();
        }

        public DbSet<User> Users { get; set; }
    }
}