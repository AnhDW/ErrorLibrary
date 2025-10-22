using Microsoft.AspNetCore.Mvc;

namespace ErrorLibrary.Controllers
{
    public class ErrorLibraryController:Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
