using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class UserPanelController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
