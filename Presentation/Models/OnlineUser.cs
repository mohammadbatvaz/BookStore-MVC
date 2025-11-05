using Domain.Enums;

namespace Presentation.Models
{
    public class OnlineUser
    {
        public static bool UserLoggedIn { get; private set; }
        public static int Id { get; private set; }
        public static string Username { get; private set; }
        public static UserRoleEnum Role { get; private set; }

        public static void Set(int id, string username, UserRoleEnum role)
        {
            UserLoggedIn = true;
            Id = id;
            Username = username;
            Role = role;
        }

        public static void Clear()
        {
            UserLoggedIn = false;
            Id = 0;
            Username = string.Empty;
            Role = UserRoleEnum.None;
        }
    }
}
