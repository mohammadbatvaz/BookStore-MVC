using Domain.DTOs;
using Domain.Entities;
using Domain.ViewModel;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Services.Interfaces;

namespace Services.Services
{
    public class BookServices(
        IBookRepositories _bookRepo,
        IFileService fileService) : IBookServices
    {
        public List<BookSummaryInfoDTO> GetNewBooksSummaryInfoList(int numberOfBook)
        {
            return _bookRepo.GetNewBooksSummaryInfoList(numberOfBook);
        }

        public List<BookInfoDTO> AllBookList()
        {
            return _bookRepo.GetAllBookList();
        }

        public Result<bool> AddNewBook(NewBookViewModel newBook, int adminId)
        {
            long maxFileSize = 300 * 1024;

            if (newBook.Title.Length > 200)
                return Result<bool>.Failure("تعداد کارکتر عنوان نباید بیشتر از 200 کاراکتر باشد");
            if(newBook.Description.Length > 500)
                return Result<bool>.Failure("تعداد کارکتر توضیحات نباید بیشتر از 500 کاراکتر باشد");
            if(newBook.Author.Length > 100)
                return Result<bool>.Failure("تعداد کارکتر نویسنده نباید بیشتر از 100 کاراکتر باشد");
            if(newBook.PagesNumber <= 0)
                return Result<bool>.Failure("تعداد صفحات باید بیشتر از 0 باشد");
            if(newBook.Price <= 0)
                return Result<bool>.Failure("قیمت باید بیشتر از 0 باشد");
            if (newBook.Image is not null && newBook.Image.Length > maxFileSize)
                return Result<bool>.Failure("حجم تصویر پروفایل شما نباید بیشتر 300 کیلوبایت باشد");

            var book = new Book()
            {
                Title = newBook.Title,
                CategoryId = newBook.CategoryId,
                Author = newBook.Author,
                Description = newBook.Description,
                PagesNumber = newBook.PagesNumber,
                Price = newBook.Price,
                CreatedBy = adminId,
                ImageUrl = newBook.Image is not null
                    ? fileService.Upload(newBook.Image, "Book")
                    : null
            };


            try
            {
                var result = _bookRepo.AddNewBook(book, adminId);
                return result
                    ? Result<bool>.Success("کتاب با موفقیت افزوده شد")
                    : Result<bool>.Failure("کتاب افزوده نشد");
            }
            catch (Exception e)
            {
                fileService.Delete(book.ImageUrl);
                return Result<bool>.Failure("کتاب افزوده نشد، لطفا بعدا تلاش کنید.");
            }
        }
    }
}
