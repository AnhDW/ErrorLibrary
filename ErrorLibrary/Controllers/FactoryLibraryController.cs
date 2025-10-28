using Microsoft.AspNetCore.Mvc;

namespace ErrorLibrary.Controllers
{
    public class FactoryLibraryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
