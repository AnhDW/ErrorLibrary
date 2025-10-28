using Microsoft.AspNetCore.Mvc;

namespace ErrorLibrary.Controllers
{
    public class UnitLibraryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
