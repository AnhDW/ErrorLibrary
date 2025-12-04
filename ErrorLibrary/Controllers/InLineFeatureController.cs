using Microsoft.AspNetCore.Mvc;

namespace ErrorLibrary.Controllers
{
    public class InLineFeatureController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
