using Microsoft.AspNetCore.Http;

namespace Domain.DTOs
{
    public class UserEditedInfoDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CurrentProfileImageUrl { get; set; }
        public IFormFile? NewProfileImage { get; set; }
    }
}
