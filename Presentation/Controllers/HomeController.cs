using Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using Services.Interfaces;
using Services.Services;

namespace Presentation.Controllers
{
    public class HomeController(
        ILogger<HomeController> logger,
        IBookServices bookServices,
        ICategoryServices categoryServices,
        IUserServices userServices) 
        : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly int NumberOfNewBooksDisplayed = 5;

        public IActionResult Index()
        {
            var homePageData = new HomePageViewModel()
            {
                Books = bookServices.GetNewBooksSummaryInfoList(NumberOfNewBooksDisplayed),
                Categories = categoryServices.GetAllCategoryInfo()
            };

            ViewBag.UserFullName = OnlineUser.UserLoggedIn 
                ? userServices.GetUserFullName(OnlineUser.Id) 
                : null;
            ViewBag.Role = OnlineUser.Role;
            
            return View(homePageData);
        }
    }
}
