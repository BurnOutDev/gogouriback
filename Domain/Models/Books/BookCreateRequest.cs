namespace Domain.Models.Books
{
    public class BookCreateRequest
    {
        public string Name { get; set; }
        public string Isbn { get; set; }
        public int AuthorId { get; set; }
        public string Description { get; set; }
    }
}