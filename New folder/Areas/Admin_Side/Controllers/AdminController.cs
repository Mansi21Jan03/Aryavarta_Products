using Microsoft.AspNetCore.Mvc;

namespace Aryavarta.Areas.Admin_Side.Controllers
{
    [Area("Admin_Side")]
    [Route("Admin_Side/[controller]/[action]")]
    public class AdminController : Controller
    {
        public IActionResult Admin()
        {
            return View("Admin");
        }
    }
}
