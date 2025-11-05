namespace Domain.Entities
{
    public class Category : _BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string EmojiIcon { get; set; }
        public string BackgroundColorHEX { get; set; }

        public List<Book> Books { get; set; }
    }
}
