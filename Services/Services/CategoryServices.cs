using Domain.DTOs;
using Domain.Entities;
using Domain.ViewModel;
using Infrastructure;
using Infrastructure.Interfaces;
using Services.Interfaces;

namespace Services.Services
{
    public class CategoryServices(ICategoryRepositories _categoryRepo) : ICategoryServices
    {
        public List<CategoryInfoDTO> GetAllCategoryInfo()
        {
            return _categoryRepo.GetAllCategoryInfo();
        }

        public CategoryInfoDTO GetCategory(int categoryId)
        {
            return _categoryRepo.GetCategoryById(categoryId);
        }

        public Result<bool> AddNewCategory(NewCategoryViewModel newCategoryData, int adminId)
        {
            if(newCategoryData.Title.Length > 100)
                return Result<bool>.Failure("عنوان دسته‌بندی نمی‌تواند بیشتر از 100 کاراکتر باشد.");
            if(newCategoryData.Description.Length > 300)
                return Result<bool>.Failure("توضیحات دسته‌بندی نمی‌تواند بیشتر از 300 کاراکتر باشد.");
            if(newCategoryData.EmojiIcon.Length > 10)
                return Result<bool>.Failure("طول کاراکتر ایموجی نباید بیشتر از 10 کاراکتر باشد");
            if(newCategoryData.BackgroundColorHEX.Length != 7)
                return Result<bool>.Failure("رنگ پس‌زمینه باید به صورت کد HEX و شامل 7 کاراکتر باشد.");

            var category = new Category()
            {
                Title = newCategoryData.Title,
                Description = newCategoryData.Description,
                EmojiIcon = newCategoryData.EmojiIcon,
                BackgroundColorHEX = newCategoryData.BackgroundColorHEX,
                CreatedBy = adminId
            };

            try
            {
                var result = _categoryRepo.AddNewCategory(category);
                return result == true
                    ? Result<bool>.Success("دسته بندی با موفقیت افزوده شد")
                    : Result<bool>.Failure("دسته بندی افزوده نشد");
            }
            catch (Exception e)
            {
                return Result<bool>.Failure("دسته بندی افزوده نشد، لطفا بعدا تلاش کنید.");
            }
        }

        public Result<bool> EditCategory(CategoryInfoDTO newCategoryData, int adminId)
        {
            if (newCategoryData.Title.Length > 100)
                return Result<bool>.Failure("عنوان دسته‌بندی نمی‌تواند بیشتر از 100 کاراکتر باشد.");
            if (newCategoryData.Description.Length > 300)
                return Result<bool>.Failure("توضیحات دسته‌بندی نمی‌تواند بیشتر از 300 کاراکتر باشد.");
            if (newCategoryData.EmojiIcon.Length > 10)
                return Result<bool>.Failure("طول کاراکتر ایموجی نباید بیشتر از 10 کاراکتر باشد");
            if (newCategoryData.BackgroundColorHEX.Length != 7)
                return Result<bool>.Failure("رنگ پس‌زمینه باید به صورت کد HEX و شامل 7 کاراکتر باشد.");

            _categoryRepo.UpdateCategory(newCategoryData, adminId);
            return Result<bool>.Success("اطلاعات با موفقیت بروز شد");
        }

        public Result<bool> DeleteCategory(int categoryId, int adminId)
        {
            var category = _categoryRepo.GetCategoryById(categoryId);
            if (category == null) return Result<bool>.Failure("دسته‌بندی یافت نشد.");

            _categoryRepo.DeleteCategory(categoryId, adminId);
            return Result<bool>.Success("دسته‌بندی با موفقیت حذف شد.");
        }

        public List<CategorySummaryInfoDTO> GetAllCategorySummaryInfo()
        {
            return _categoryRepo.GetAllCategorySummaryInfo();
        }
    }
}
