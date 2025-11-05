using Domain.Entities;
using Domain.Enums;
using Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using Services.Interfaces;
using System.Diagnostics;

namespace Presentation.Controllers
{
    public class AuthenticationController(
        IUserServices userServices,
        IAuthenticationServices authServices) : Controller
    {
        public IActionResult Login(Result<bool> results)
        {
            return View(results);
        }

        [HttpPost]
        public IActionResult Login(LoginInputViewModel loginInfo)
        {
            var result = authServices.Authentication(loginInfo);

            if (!result.IsSuccess || !result.Data.IsActive)
                return View(Result<bool>.Failure(result.Message));

            OnlineUser.Set(result.Data.Id, result.Data.UserName, result.Data.Role);

            return result.Data.Role == UserRoleEnum.Admin
                ? RedirectToAction("Dashboard", "AdminPanel")
                : RedirectToAction("Dashboard", "UserPanel");
        }

        public IActionResult Registration(Result<bool> results)
        {
            return View(results);
        }

        [HttpPost]
        public IActionResult Registration(RegistrationDataViewModel newUserData)
        {
            var result = userServices.AddNewUser(newUserData);

            return result.IsSuccess 
                ? View("Login", result) 
                : View(result);
        }

        public IActionResult Logout()
        {
            OnlineUser.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
