using Domain.DTOs;
using Domain.Entities;

namespace Infrastructure.Interfaces
{
    public interface IBookRepositories
    {
        List<BookSummaryInfoDTO> GetNewBooksSummaryInfoList(int numberOfBook);

        List<BookInfoDTO> GetAllBookList();

        bool AddNewBook(Book book, int adminId);
    }
}
