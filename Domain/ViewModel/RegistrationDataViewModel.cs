using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Domain.ViewModel
{
    public class RegistrationDataViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public IFormFile? ProfileImageFile { get; set; }
    }
}
