using Domain.DTOs;
using Domain.Entities;
using Domain.ViewModel;

namespace Services.Interfaces
{
    public interface IUserServices
    {
        Result<bool> AddNewUser(RegistrationDataViewModel newUser, int createdBy = 0, bool isActive = false);
        
        string GetUserFullName(int userId);

        List<UserDataDTO> AllUsersList();

        void ChangeUserStatus(int userID, bool newStatus);

        void UserSoftDelete(int userId, int adminId);

        UserDataDTO GetUserInfo(int id);

        Result<bool> EditUserInfo(UserEditedInfoDTO userNewInfo, int adminId);
    }
}
