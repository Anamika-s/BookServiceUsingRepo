using BookServiceUsingRepo.Models;

namespace BookServiceUsingRepo.IRepo
{
    public interface IBookRepo
    {
        // CRUD operations
        List<Book> GetBooks();
        Book GetBook(int id);
        Book AddBook(Book book);    
        void EditBook(int id, Book book);
        void DeleteBook(int id);
    }
}
