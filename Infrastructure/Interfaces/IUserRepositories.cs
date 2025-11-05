using Domain.DTOs;

namespace Infrastructure.Interfaces
{
    public interface IUserRepositories
    {
        bool AddNewUser(NewUserDataDTO user);

        CurrentUserDTO? UserAuthentication(string username, string passwordHash);

        UserFullNameDTO? GetUserFullNameById(int Id);

        List<UserDataDTO> GetAllUsers();
    }
}
