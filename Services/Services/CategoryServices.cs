using Domain.DTOs;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Interfaces;
using Services.Interfaces;

namespace Services.Services
{
    public class CategoryServices(ICategoryRepositories _categoryRepo) : ICategoryServices
    {
        public List<CategorySummaryInfoDTO> GetAllCategorySummaryInfo()
        {
            return _categoryRepo.GetAllCategorySummaryInfo();
        }
    }
}
