using Domain.DTOs;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BookRepositories(AppDbContext _db) : IBookRepositories
    {
        public List<BookSummaryInfoDTO> GetNewBooksSummaryInfoList(int numberOfBook)
        {
            return _db.Books
                .OrderByDescending(b => b.CreatedAt)
                .Take(numberOfBook)
                .Select(b => new BookSummaryInfoDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    PagesNumber = b.PagesNumber,
                    Price = b.Price,
                    ImageUrl = b.ImageUrl
                })
                .ToList();
        }
    }
}
