using Domain.DTOs;

namespace Infrastructure.Interfaces
{
    public interface ICategoryRepositories
    {
        List<CategorySummaryInfoDTO> GetAllCategorySummaryInfo();
    }
}
