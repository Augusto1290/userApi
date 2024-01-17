using Microsoft.EntityFrameworkCore;
using UsersApi.Models.Post;
using UsersApi.Models.User;

namespace UsersApi.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User() { Id = 1, Name = "John", UserName = "John1", Password = "123456" },
                new User() { Id = 2, Name = "Jane", UserName = "Jane34", Password = "123456" },
                new User() { Id = 3, Name = "Joe", UserName = "JoeJoe", Password = "123456", Email = "Joe@mail.com" }
            );
        }
    }
}
