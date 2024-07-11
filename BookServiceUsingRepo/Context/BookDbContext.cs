using BookServiceUsingRepo.Models;
using BookServiceUsingRepo.REpo;
using Microsoft.EntityFrameworkCore;

namespace BookServiceUsingRepo.Context
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options)
            : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .HasData(new Role()
                {
                    RoleId = 1,
                    RoleName = "Admin"
                },
                new Role()
                {
                    RoleId = 2,
                    RoleName = "Manager"
                },
                new Role()
                {
                    RoleId = 3,
                    RoleName = "Employee"
                });
            modelBuilder.Entity<User>()
                .HasData(new User
                {
                    Id = 1,
                    FirstName = "Vijay Sood",
                    Password = "pass@1",
                    Email = "user2@gmial.com",
                    RoleId = 2
                },
                new User
                {
                    Id = 3,
                    FirstName = "Deepak Sood",
                    Password = "pass@1",
                    Email = "user3@gmial.com",
                    RoleId = 3
                }
               );


        }
    }
}

