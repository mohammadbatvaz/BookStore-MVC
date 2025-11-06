using Domain.DTOs;
using Domain.Entities;
using Domain.ViewModel;

namespace Services.Interfaces
{
    public interface IBookServices
    {
        List<BookSummaryInfoDTO> GetNewBooksSummaryInfoList(int numberOfBook);

        List<BookInfoDTO> AllBookList();

        Result<bool> AddNewBook(NewBookViewModel newBook, int adminId);
    }
}
