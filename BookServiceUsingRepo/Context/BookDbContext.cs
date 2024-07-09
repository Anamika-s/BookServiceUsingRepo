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
    }
}
