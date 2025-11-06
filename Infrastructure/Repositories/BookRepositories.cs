using Domain.DTOs;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

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

        public List<BookInfoDTO> GetAllBookList()
        {
            return _db.Books
                .Include(b => b.Category)
                .Select(b => new BookInfoDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    Category = b.Category.Title,
                    PagesNumber = b.PagesNumber,
                    Price = b.Price,
                    ImageUrl = b.ImageUrl,
                    Description = b.Description,
                }).ToList();
        }

        public bool AddNewBook(Book book, int adminId)
        {
            _db.Books.Add(book);
            return _db.SaveChanges() > 0;
        }
    }
}
