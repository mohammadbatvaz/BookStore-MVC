using Domain.Enums;

namespace Domain.DTOs
{
    public class CurrentUserDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }
        public UserRoleEnum Role { get; set; }
    }
}
