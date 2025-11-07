using Domain.DTOs;
using Domain.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

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
                CreatedBy = user.CreatedBy
            });
            return _db.SaveChanges() > 0;
        }

        public CurrentUserDTO? UserAuthentication(string username, string passwordHash)
        {
            return _db.Users
                .AsNoTracking()
                .Where(u => u.UserName == username && u.PasswordHash == passwordHash)
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
                .AsNoTracking()
                .Where(u => u.Id == Id)
                .Select(u => new UserFullNameDTO
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName
                }).FirstOrDefault();
        }

        public List<UserDataDTO> GetAllUsers()
        {
            return _db.Users
                .AsNoTracking()
                .Select(u => new UserDataDTO
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

        public void ActiveUser(int userId)
        {
            _db.Users.Where(u => u.Id == userId)
                .ExecuteUpdate(u => u
                    .SetProperty(user => user.IsActive, true));
        }

        public void DeActiveUser(int userId)
        {
            _db.Users.Where(u => u.Id == userId)
                .ExecuteUpdate(u => u
                    .SetProperty(user => user.IsActive, false));
        }

        public void UserSoftDelete(int userId, int adminId)
        {
            _db.Users.Where(u => u.Id == userId)
                .ExecuteUpdate(u => u
                    .SetProperty(user => user.SoftDelete, true)
                    .SetProperty(user => user.DeletedBy, adminId)
                    .SetProperty(user => user.DeletedAt, DateTime.Now));
        }

        public UserDataDTO GetUserInfoById(int userId)
        {
            return _db.Users
                .AsNoTracking()
                .Where(u => u.Id == userId)
                .Select(u => new UserDataDTO()
                {
                    Id = u.Id,
                    Username = u.UserName,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    ProfileImageUrl = u.ProfileImageUrl,
                    IsActive = u.IsActive,
                    Role = u.Role
                })
                .FirstOrDefault();
        }

        public void UpdateUser(UserEditedInfoDTO userNewInfo, int adminId)
        {
            _db.Users.Where(u => u.Id == userNewInfo.Id)
                .ExecuteUpdate(u => u
                    .SetProperty(u => u.FirstName, userNewInfo.FirstName)
                    .SetProperty(u => u.LastName, userNewInfo.LastName)
                    .SetProperty(u => u.Email, userNewInfo.Email)
                    .SetProperty(u => u.ProfileImageUrl, userNewInfo.CurrentProfileImageUrl)
                    .SetProperty(u => u.UpdatedAt, DateTime.Now)
                    .SetProperty(u => u.UpdatedBy, adminId));
        }
    }
}