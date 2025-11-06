using Domain.DTOs;
using Domain.Entities;
using Domain.ViewModel;

namespace Services.Interfaces
{
    public interface ICategoryServices
    {
        List<CategoryInfoDTO> GetAllCategoryInfo();

        public CategoryInfoDTO GetCategory(int categoryId);

        Result<bool> AddNewCategory(NewCategoryViewModel newCategoryData, int adminId);

        Result<bool> EditCategory(CategoryInfoDTO userNewInfo, int adminId);

        Result<bool> DeleteCategory(int categoryId, int adminId);

        List<CategorySummaryInfoDTO> GetAllCategorySummaryInfo();
    }
}
