using Microsoft.AspNetCore.Mvc;

namespace ErrorLibrary.Controllers
{
    public class ProductLibraryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
