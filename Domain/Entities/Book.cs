namespace Domain.Entities
{
    public class Book : BaseEntity
    {
        public string Name { get; set; }
        public string Isbn { get; set; }
        public Author Author { get; set; }
        public string Description { get; set; }
    }
}
