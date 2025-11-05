using Domain.DTOs;

namespace Services.Interfaces
{
    public interface IBookServices
    {
        List<BookSummaryInfoDTO> GetNewBooksSummaryInfoList(int numberOfBook);
    }
}
