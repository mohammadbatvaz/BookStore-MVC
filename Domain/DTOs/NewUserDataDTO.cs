using Domain.Enums;

namespace Domain.DTOs
{
    public class NewUserDataDTO
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public string? ProfileImageUrl { get; set; }
        public UserRoleEnum Role { get; set; }
        public int? CreatedBy { get; set; } 
    }
}
