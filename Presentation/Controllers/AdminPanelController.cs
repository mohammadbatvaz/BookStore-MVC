using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using Services.Interfaces;

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
    }
}
