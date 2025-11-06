using Domain.DTOs;

namespace Infrastructure.Interfaces
{
    public interface IUserRepositories
    {
        bool AddNewUser(NewUserDataDTO user);

        CurrentUserDTO? UserAuthentication(string username, string passwordHash);

        UserFullNameDTO? GetUserFullNameById(int Id);

        List<UserDataDTO> GetAllUsers();

        void ActiveUser(int userId);

        void DeActiveUser(int userId);

        void UserSoftDelete(int userId, int adminId);

        UserDataDTO GetUserInfoById(int userId);

        void UpdateUser(UserEditedInfoDTO userNewInfo, int adminId);
    }
}
