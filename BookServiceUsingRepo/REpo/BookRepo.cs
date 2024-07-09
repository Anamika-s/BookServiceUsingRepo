using BookServiceUsingRepo.IRepo;
using BookServiceUsingRepo.Models;

namespace BookServiceUsingRepo.REpo
{
    public class BookRepo : IBookRepo
    {
        public static List<Book> books = null;

        public BookRepo()
        {

            if (books == null)
            {
                // List initializer
                books = new List<Book>()
                {
                    new Book(){ Id=100, BookName="C++", AuthorName="Author1", EditionNo=1, PublisherName="publisher1", PublishedDate=Convert.ToDateTime("12/12/2022")},

                    new Book(){ Id=101, BookName="Java", AuthorName="Author2", EditionNo=3, PublisherName="publisher2", PublishedDate=Convert.ToDateTime("12/12/2022")},

                    new Book(){ Id=102, BookName="C#", AuthorName="Author3", EditionNo=4, PublisherName="publisher3", PublishedDate=Convert.ToDateTime("12/12/2022")},


                    new Book(){ Id=103, BookName="Dotnet", AuthorName="Author4", EditionNo=1, PublisherName="publisher1", PublishedDate=Convert.ToDateTime("12/12/2022")}
                };
            }
        }
        public Book AddBook(Book book)
        {
            books.Add(book);
            return book;
        }

        public void DeleteBook(int id)
        {
            Book temp = GetBook(id);
            books.Remove(temp);
        }

        public void EditBook(int id, Book book)
        {
            Book temp = GetBook(id);
            temp.PublishedDate = book.PublishedDate;
        }

        public Book GetBook(int id)
        {
            return books.FirstOrDefault(x => x.Id == id);
        }

        public List<Book> GetBooks()
        {
            return books.ToList();
        }
    }
}
