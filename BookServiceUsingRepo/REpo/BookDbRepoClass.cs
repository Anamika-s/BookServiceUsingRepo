using BookServiceUsingRepo.Context;
using BookServiceUsingRepo.IRepo;
using BookServiceUsingRepo.Models;
using Microsoft.EntityFrameworkCore;

namespace BookServiceUsingRepo.REpo
{
    public class BookDbRepoClass : IBookRepo
    {
        BookDbContext _context;
        public BookDbRepoClass(BookDbContext context) {
            _context = context;
        }

        public Book AddBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
            return book;
        }

        public void DeleteBook(int id)
        {
            Book temp = GetBook(id);
            _context.Books.Remove(temp);
            _context.SaveChanges();

        }

        public void EditBook(int id, Book book)
        {
            Book temp = GetBook(id);
            temp.PublishedDate = DateTime.Now;
            _context.SaveChanges();
        }

        public Book GetBook(int id)
        {
            return _context.Books.FirstOrDefault(x => x.Id == id);
        }

        public List<Book> GetBooks()
        {
            return _context.Books.ToList();

        }
    }
}
