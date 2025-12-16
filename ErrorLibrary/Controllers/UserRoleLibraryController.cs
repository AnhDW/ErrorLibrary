using Microsoft.AspNetCore.Mvc;

namespace ErrorLibrary.Controllers
{
    public class UserRoleLibraryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        //[HasPermission("UserRoles", "Create")]
        //[HasPermission("UserRoles", "Update")]
        //[HasPermission("UserRoles", "Delete")]
        //[HasPermission("UserRoles", "View")]
    }
}
