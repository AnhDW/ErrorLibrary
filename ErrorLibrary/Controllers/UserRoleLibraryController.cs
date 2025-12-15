using Microsoft.AspNetCore.Mvc;

namespace ErrorLibrary.Controllers
{
    public class UserRoleLibraryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
