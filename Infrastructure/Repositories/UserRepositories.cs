using Domain.DTOs;
using Domain.Entities;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories
{
    public class UserRepositories(AppDbContext _db) : IUserRepositories
    {
        public bool AddNewUser(NewUserDataDTO user)
        {
            _db.Users.Add(new User()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
                PasswordHash = user.PasswordHash,
                ProfileImageUrl = user.ProfileImageUrl,
                IsActive = user.IsActive,
                Role = user.Role,
                CreatedAt = DateTime.Now
            });
            return _db.SaveChanges() > 0;
        }

        public CurrentUserDTO? UserAuthentication(string username, string passwordHash)
        {
            return _db.Users
                .Where(u => u.UserName == username && u.PasswordHash==passwordHash)
                .Select(u => new CurrentUserDTO
                { 
                    Id = u.Id,
                    UserName = u.UserName,
                    Role = u.Role,
                    IsActive = u.IsActive
                }).FirstOrDefault();
        }

        public UserFullNameDTO? GetUserFullNameById(int Id)
        {
            return _db.Users
                .Where(u => u.Id == Id)
                .Select(u => new UserFullNameDTO
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName
                }).FirstOrDefault();
        }

        public List<UserDataDTO> GetAllUsers()
        {
            return _db.Users.Select(u => new UserDataDTO
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Username = u.UserName,
                IsActive = u.IsActive,
                Role = u.Role,
                ProfileImageUrl = u.ProfileImageUrl
            }).ToList();
        }
    }
}