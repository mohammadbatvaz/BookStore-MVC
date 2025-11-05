using Domain.DTOs;

namespace Services.Interfaces
{
    public interface ICategoryServices
    {
        List<CategorySummaryInfoDTO> GetAllCategorySummaryInfo();
    }
}
