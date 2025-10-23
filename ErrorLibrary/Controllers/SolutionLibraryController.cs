using Microsoft.AspNetCore.Mvc;

namespace ErrorLibrary.Controllers
{
    public class SolutionLibraryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
