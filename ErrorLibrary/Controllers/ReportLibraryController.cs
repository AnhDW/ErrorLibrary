using Microsoft.AspNetCore.Mvc;

namespace ErrorLibrary.Controllers
{
    public class ReportLibraryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
