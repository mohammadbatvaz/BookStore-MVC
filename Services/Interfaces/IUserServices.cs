using Domain.DTOs;
using Domain.Entities;
using Domain.ViewModel;

namespace Services.Interfaces
{
    public interface IUserServices
    {
        Result<bool> AddNewUser(RegistrationDataViewModel newUser);
        
        string GetUserFullName(int userId);

        List<UserDataDTO> AllUsersList();
    }
}
