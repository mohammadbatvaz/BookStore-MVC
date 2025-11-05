using Domain.DTOs;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories
{
    public class CategoryRepositories(AppDbContext _db) : ICategoryRepositories
    {
        public List<CategorySummaryInfoDTO> GetAllCategorySummaryInfo()
        {
            return _db.Categories.Select(c => new CategorySummaryInfoDTO()
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                EmojiIcon = c.EmojiIcon,
                BackgroundColorHEX = c.BackgroundColorHEX
            }).ToList();
        }
    }
}
