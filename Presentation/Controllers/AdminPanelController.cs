using Domain.DTOs;
using Domain.Entities;
using Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Presentation.Filters;
using Presentation.Models;
using Services.Interfaces;

namespace Presentation.Controllers
{
    public class AdminPanelController(
        IUserServices userServices,
        ICategoryServices categoryServices,
        IBookServices bookServices) : Controller
    {
        private readonly string UserFullName = userServices.GetUserFullName(OnlineUser.Id);

        [AdminOnly]
        public IActionResult Dashboard()
        {
            ViewBag.FullName = UserFullName;
            return View();
        }

        #region User Management

        [AdminOnly]
        public IActionResult Users()
        {
            ViewBag.FullName = UserFullName;
            var userList = userServices.AllUsersList();
            return View(userList);
        }

        [AdminOnly]
        public IActionResult Registration(Result<bool> results)
        {
            return View(results);
        }

        [HttpPost]
        [AdminOnly]
        public IActionResult Registration(RegistrationDataViewModel newUserData)
        {
            var result = userServices.AddNewUser(newUserData, OnlineUser.Id, true);

            return result.IsSuccess
                ? RedirectToAction("Users")
                : View(result);
        }

        [AdminOnly]
        public IActionResult ChangeStatus(int id, bool changeStatusTo)
        {
            userServices.ChangeUserStatus(id, changeStatusTo);
            return RedirectToAction("Users");
        }

        [AdminOnly]
        public IActionResult Delete(int id)
        {
            userServices.UserSoftDelete(id, OnlineUser.Id);
            return RedirectToAction("Users");
        }

        [AdminOnly]
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
        [AdminOnly]
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
        #endregion

        #region Category Management

        [AdminOnly]
        public IActionResult Category()
        {
            ViewBag.FullName = UserFullName;
            var CategoryList = categoryServices.GetAllCategoryInfo();
            return View(CategoryList);
        }

        [AdminOnly]
        public IActionResult AddCategory(Result<bool> results)
        {
            return View(results);
        }

        [HttpPost]
        [AdminOnly]
        public IActionResult AddCategory(NewCategoryViewModel newCategoryData)
        {
            var result = categoryServices.AddNewCategory(newCategoryData, OnlineUser.Id);

            return result.IsSuccess
                ? RedirectToAction("Category")
                : View(result);
        }

        [AdminOnly]
        public IActionResult EditCategory(int id)
        {
            var Category = categoryServices.GetCategory(id);

            var data = new CategoryEditableInfoViewModel()
            {
                Id = Category.Id,
                Title = Category.Title,
                Description = Category.Description,
                EmojiIcon = Category.EmojiIcon,
                BackgroundColorHEX = Category.BackgroundColorHEX
            };
            return View(Result<CategoryEditableInfoViewModel>.Success("اطلاعات قبلی یافت شد", data));
        }

        [HttpPost]
        [AdminOnly]
        public IActionResult EditCategory(Result<CategoryEditableInfoViewModel> request)
        {

            var newData = new CategoryInfoDTO()
            {
                Id = request.Data.Id,
                Title = request.Data.Title,
                Description = request.Data.Description,
                EmojiIcon = request.Data.EmojiIcon,
                BackgroundColorHEX = request.Data.BackgroundColorHEX
            };

            var result = categoryServices.EditCategory(newData, OnlineUser.Id);

            if (!result.IsSuccess)
                return View(Result<CategoryEditableInfoViewModel>.Failure(result.Message, request.Data));

            return RedirectToAction("Category");
        }

        [AdminOnly]
        public IActionResult DeleteCategory(int id)
        {
            categoryServices.DeleteCategory(id, OnlineUser.Id);
            return RedirectToAction("Category");
        }

        #endregion

        #region Book Management

        [AdminOnly]
        public IActionResult Books()
        {
            ViewBag.FullName = UserFullName;
            var bookList = bookServices.AllBookList();
            return View(bookList);
        }

        [AdminOnly]
        public IActionResult AddNewBook(Result<bool> results)
        {
            ViewBag.CategoryList = new SelectList(categoryServices.GetAllCategorySummaryInfo(), "Id", "Title");
            return View(results);
        }

        [HttpPost]
        [AdminOnly]
        public IActionResult AddNewBook(NewBookViewModel newBookData)
        {
            var result = bookServices.AddNewBook(newBookData, OnlineUser.Id);

            return result.IsSuccess
                ? RedirectToAction("Books")
                : RedirectToAction("AddNewBook", result);
        }
        #endregion
    }
}
