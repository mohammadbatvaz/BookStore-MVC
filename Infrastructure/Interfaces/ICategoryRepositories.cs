using Domain.DTOs;
using Domain.Entities;

namespace Infrastructure.Interfaces
{
    public interface ICategoryRepositories
    {
        List<CategoryInfoDTO> GetAllCategoryInfo();

        bool AddNewCategory(Category category);

        CategoryInfoDTO? GetCategoryById(int Id);

        void UpdateCategory(CategoryInfoDTO newCategoryData, int adminId);

        void DeleteCategory(int categoryId, int adminId);

        List<CategorySummaryInfoDTO> GetAllCategorySummaryInfo();
    }
}
