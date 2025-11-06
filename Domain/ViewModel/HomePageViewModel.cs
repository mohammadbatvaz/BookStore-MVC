using Domain.DTOs;

namespace Domain.ViewModel
{
    public class HomePageViewModel
    {
        public List<CategoryInfoDTO> Categories { get; set; }
        public List<BookSummaryInfoDTO> Books { get; set; }
    }
}
