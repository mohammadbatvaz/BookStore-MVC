using Domain.DTOs;
using Domain.Entities;
using Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using Services.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Presentation.Controllers
{
    public class AdminPanelController(
        IUserServices userServices) : Controller
    {
        private readonly string UserFullName = userServices.GetUserFullName(OnlineUser.Id);
        
        public IActionResult Dashboard()
        {
            ViewBag.FullName = UserFullName;
            return View();
        }

        public IActionResult Users()
        {
            ViewBag.FullName = UserFullName;
            var userList = userServices.AllUsersList();
            return View(userList);
        }

        public IActionResult Registration(Result<bool> results)
        {
            return View(results);
        }

        [HttpPost]
        public IActionResult Registration(RegistrationDataViewModel newUserData)
        {
            var result = userServices.AddNewUser(newUserData, OnlineUser.Id, true);

            return result.IsSuccess
                ? RedirectToAction("Users")
                : View(result);
        }

        public IActionResult ChangeStatus(int id, bool changeStatusTo)
        {
            userServices.ChangeUserStatus(id, changeStatusTo);
            return RedirectToAction("Users");
        }

        public IActionResult Delete(int id)
        {
            userServices.UserSoftDelete(id, OnlineUser.Id);
            return RedirectToAction("Users");
        }

        public IActionResult EditUserInfo(int id)
        {
            var user = userServices.GetUserInfo(id);

            var data = new UserEditableInfoViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                CurrentProfileImageUrl = user.ProfileImageUrl
            };
            return View(Result<UserEditableInfoViewModel>.Success("اطلاعات قبلی یافت شد", data));
        }

        [HttpPost]
        public IActionResult EditUserInfo(Result<UserEditableInfoViewModel> request)
        {

            var newData = new UserEditedInfoDTO()
            {
                Id = request.Data.Id,
                FirstName = request.Data.FirstName,
                LastName = request.Data.LastName,
                Email = request.Data.Email,
                CurrentProfileImageUrl = request.Data.CurrentProfileImageUrl,
                NewProfileImage = request.Data.NewProfileImage
            };

            var result = userServices.EditUserInfo(newData, OnlineUser.Id);

            if(!result.IsSuccess) 
                return View(Result<UserEditableInfoViewModel>.Failure(result.Message, request.Data));
            
            return RedirectToAction("Users");
        }
    }
}
