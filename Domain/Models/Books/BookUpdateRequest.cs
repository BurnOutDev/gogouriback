namespace Domain.Models.Books
{
    public class BookUpdateRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Isbn { get; set; }
        public int AuthorId { get; set; }
        public string Description { get; set; }
    }
}