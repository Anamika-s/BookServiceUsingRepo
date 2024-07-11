namespace BookServiceUsingRepo.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string BookName { get; set; }
        public string PublisherName { get; set; }
        public string AuthorName { get; set; }
        public DateTime PublishedDate { get; set; }
        public int EditionNo { get; set; }
    }
}
