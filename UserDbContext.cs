using Microsoft.EntityFrameworkCore;

namespace WeatherApi
{
    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=testdb;User Id=sa;Password=MyPass@word;");
        }
    }

    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
