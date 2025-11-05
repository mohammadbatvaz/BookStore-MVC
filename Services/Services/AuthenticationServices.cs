using Domain.DTOs;
using Domain.Entities;
using Domain.ViewModel;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Services.Interfaces;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Services.Services
{
    public class AuthenticationServices(
        IUserRepositories _userRepo) : IAuthenticationServices
    {
        public Result<CurrentUserDTO> Authentication(LoginInputViewModel loginInput)
        {
            var result = _userRepo.UserAuthentication(loginInput.Username, ToMd5Hex(loginInput.Password));
            
            if(result is null)
                return Result<CurrentUserDTO>.Failure("کاربری با این مشخصات یافت نشد.");

            if(!result.IsActive)
                return Result<CurrentUserDTO>.Failure("حساب کاربری شما غیر فعال است.");
            
            return Result<CurrentUserDTO>.Success("کاربر با موفقیت وارد شد",result);
        }

        public string ToMd5Hex(string input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            using var md5 = MD5.Create();
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = md5.ComputeHash(bytes);
                
            var sb = new StringBuilder(hash.Length * 2);
            foreach (var b in hash)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }
    }
}
