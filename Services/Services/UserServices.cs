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
        public Result<bool> AddNewUser(RegistrationDataViewModel newUser, int createdBy = 0, bool isActive = false)
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
                IsActive = isActive,
                PasswordHash = authServices.ToMd5Hex(newUser.Password),
                Role = UserRoleEnum.User,
                CreatedBy = createdBy,
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

        public void ChangeUserStatus(int userID, bool newStatus)
        {
            if(newStatus)
                _userRepo.ActiveUser(userID);
            else
                _userRepo.DeActiveUser(userID);
        }

        public void UserSoftDelete(int userId, int adminId)
        {
            _userRepo.UserSoftDelete(userId, adminId);
        }

        public UserDataDTO GetUserInfo(int id)
        {
            return _userRepo.GetUserInfoById(id);
        }

        public Result<bool> EditUserInfo(UserEditedInfoDTO userNewInfo, int adminId)
        {
            long maxFileSize = 300 * 1024;

            if (userNewInfo.FirstName.Length > 100)
                return Result<bool>.Failure("نام شما نباید بیشتر از 100 کاراکتر باشد");
            if (userNewInfo.LastName.Length > 100)
                return Result<bool>.Failure("نام خانوادگی شما نباید بیشتر از 100 کاراکتر باشد");
            if (userNewInfo.Email.Length > 255)
                return Result<bool>.Failure("ایمیل شما نباید بیشتر از 255 کاراکتر باشد");
            if (userNewInfo.NewProfileImage is not null && userNewInfo.NewProfileImage.Length > maxFileSize)
                return Result<bool>.Failure("حجم تصویر پروفایل شما نباید بیشتر 300 کیلوبایت باشد");

            if (userNewInfo.NewProfileImage is not null)
            {
                if(!userNewInfo.CurrentProfileImageUrl.Contains("default.jpg"))
                    fileService.Delete(userNewInfo.CurrentProfileImageUrl);

                userNewInfo.CurrentProfileImageUrl = fileService.Upload(userNewInfo.NewProfileImage, "Profile");
            }

            _userRepo.UpdateUser(userNewInfo, adminId);
            return Result<bool>.Success("اطلاعات با موفقیت بروز شد");
        }
    }
}
