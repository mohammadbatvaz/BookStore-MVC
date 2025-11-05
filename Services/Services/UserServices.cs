using Domain.DTOs;
using Domain.Entities;
using Domain.Enums;
using Domain.ViewModel;
using Infrastructure.Interfaces;
using Services.Interfaces;

namespace Services.Services
{
    public class UserServices(
        IAuthenticationServices authServices,
        IFileService fileService,
        IUserRepositories _userRepo) : IUserServices
    {
        public Result<bool> AddNewUser(RegistrationDataViewModel newUser)
        {
            long maxFileSize = 300 * 1024;

            if (newUser.FirstName.Length > 100)
                return Result<bool>.Failure("نام شما نباید بیشتر از 100 کاراکتر باشد");
            if (newUser.LastName.Length > 100)
                return Result<bool>.Failure("نام خانوادگی شما نباید بیشتر از 100 کاراکتر باشد");
            if (newUser.Email.Length > 255)
                return Result<bool>.Failure("ایمیل شما نباید بیشتر از 255 کاراکتر باشد");
            if (newUser.UserName.Length > 255)
                return Result<bool>.Failure("نام کاربری شما نباید بیشتر از 255 کارکتر باشد");
            if (newUser.ProfileImageFile is not null && newUser.ProfileImageFile.Length > maxFileSize)
                return Result<bool>.Failure("حجم تصویر پروفایل شما نباید بیشتر 300 کیلوبایت باشد");

            var user = new NewUserDataDTO()
            {
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Email = newUser.Email,
                UserName = newUser.UserName,
                IsActive = false,
                PasswordHash = authServices.ToMd5Hex(newUser.Password),
                Role = UserRoleEnum.User,

                ProfileImageUrl = newUser.ProfileImageFile is not null 
                    ? fileService.Upload(newUser.ProfileImageFile, "Profile") 
                    : null
            };

            try
            {
                var result = _userRepo.AddNewUser(user);
                return result == true 
                    ? Result<bool>.Success("کاربر با موفقیت افزوده شد") 
                    : Result<bool>.Failure("کاربر افزوده نشد");
            }
            catch (Exception e)
            {
                fileService.Delete(user.ProfileImageUrl);
                return Result<bool>.Failure("کاربر افزوده نشد، لطفا بعدا تلاش کنید.");
            }
        }

        public string GetUserFullName(int userId)
        {
            var user = _userRepo.GetUserFullNameById(userId);
            return user != null ? $"{user.FirstName} {user.LastName}" : "کاربر ناشناخته";
        }

        public List<UserDataDTO> AllUsersList()
        {
            return _userRepo.GetAllUsers();
        }
    }
}
