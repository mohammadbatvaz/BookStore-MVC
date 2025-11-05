using Domain.Enums;

namespace Domain.DTOs
{
    public class UserDataDTO
    {
        public string ProfileImageUrl { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public bool IsActive { get; set; }
        public UserRoleEnum Role { get; set; }
    }
}
