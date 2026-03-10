using Microsoft.AspNetCore.Mvc;

namespace ErrorLibrary.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
