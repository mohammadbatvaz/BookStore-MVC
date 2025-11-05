using Domain.DTOs;
using Domain.Entities;
using Domain.ViewModel;

namespace Services.Interfaces
{
    public interface IAuthenticationServices
    {
        Result<CurrentUserDTO> Authentication(LoginInputViewModel loginInput);

        string ToMd5Hex(string input);
    }
}
