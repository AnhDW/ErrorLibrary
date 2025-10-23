using Microsoft.AspNetCore.Mvc;

namespace ErrorLibrary.Controllers
{
    public class UserLibraryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
