namespace Domain.DTOs
{
    public class BookSummaryInfoDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int PagesNumber { get; set; }
        public int Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
