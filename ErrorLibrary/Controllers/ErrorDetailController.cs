using Microsoft.AspNetCore.Mvc;

namespace ErrorLibrary.Controllers
{
    public class ErrorDetailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
