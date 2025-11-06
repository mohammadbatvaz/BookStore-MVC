using Domain.DTOs;
using Domain.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CategoryRepositories(AppDbContext _db) : ICategoryRepositories
    {
        public List<CategoryInfoDTO> GetAllCategoryInfo()
        {
            return _db.Categories.Select(c => new CategoryInfoDTO()
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                EmojiIcon = c.EmojiIcon,
                BackgroundColorHEX = c.BackgroundColorHEX
            }).ToList();
        }

        public bool AddNewCategory(Category category)
        {
            _db.Categories.Add(category);
            return _db.SaveChanges() > 0;
        }

        public CategoryInfoDTO? GetCategoryById(int Id)
        {
            return _db.Categories
                .Where(u => u.Id == Id)
                .Select(u => new CategoryInfoDTO()
                {
                    Id = u.Id,
                    Title = u.Title,
                    Description = u.Description,
                    EmojiIcon = u.EmojiIcon,
                    BackgroundColorHEX = u.BackgroundColorHEX
                }).FirstOrDefault();
        }

        public void UpdateCategory(CategoryInfoDTO newCategoryData, int adminId)
        {
            _db.Categories.Where(c => c.Id == newCategoryData.Id)
                    .ExecuteUpdate(c => c
                        .SetProperty(cat => cat.Title, newCategoryData.Title)
                        .SetProperty(cat => cat.Description, newCategoryData.Description)
                        .SetProperty(cat => cat.EmojiIcon, newCategoryData.EmojiIcon)
                        .SetProperty(cat => cat.BackgroundColorHEX, newCategoryData.BackgroundColorHEX)
                        .SetProperty(cat => cat.UpdatedBy, adminId)
                        .SetProperty(cat => cat.UpdatedAt, DateTime.Now)
                    );
        }

        public void DeleteCategory(int categoryId, int adminId)
        {
            _db.Categories.Where(c => c.Id == categoryId)
                .ExecuteUpdate(c => c
                    .SetProperty(cat => cat.SoftDelete, true)
                    .SetProperty(cat => cat.UpdatedBy, adminId)
                    .SetProperty(cat => cat.UpdatedAt, DateTime.Now)
                );
        }

        public List<CategorySummaryInfoDTO> GetAllCategorySummaryInfo()
        {
            return _db.Categories
                .Where(c => !c.SoftDelete)
                .Select(c => new CategorySummaryInfoDTO()
                {
                    Id = c.Id,
                    Title = c.Title
                }).ToList();
        }
    }
}
