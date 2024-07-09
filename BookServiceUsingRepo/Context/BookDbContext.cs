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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasData(new User
                {
                    Id = 1,
                    UserName = "user1",
                    Password = "pass@1"
                },
                new User
                {
                    Id = 2,
                    UserName = "user2",
                    Password = "pass@123"
                },
                new User
                {
                    Id = 3,
                    UserName = "user3",
                    Password = "pass@123"
                }
               );
        }


        }
    }
