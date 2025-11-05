using Domain.DTOs;
using Domain.Entities;
using Infrastructure.Interfaces;
using Services.Interfaces;

namespace Services.Services
{
    public class BookServices(
        IBookRepositories _bookRepo) : IBookServices
    {
        public List<BookSummaryInfoDTO> GetNewBooksSummaryInfoList(int numberOfBook)
        {
            return _bookRepo.GetNewBooksSummaryInfoList(numberOfBook);
        }
    }
}
