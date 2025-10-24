using Microsoft.AspNetCore.Mvc;

namespace ErrorLibrary.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
