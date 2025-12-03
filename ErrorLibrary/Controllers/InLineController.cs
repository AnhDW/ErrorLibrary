using Microsoft.AspNetCore.Mvc;

namespace ErrorLibrary.Controllers
{
    public class InLineController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
