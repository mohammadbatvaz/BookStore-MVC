namespace Domain.Entities
{
    public class Book : _BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string Author { get; set; }
        public int PagesNumber { get; set; }
        public int Price { get; set; }
        public string ImageUrl { get; set; }

        public Category Category { get; set; }
    }
}
