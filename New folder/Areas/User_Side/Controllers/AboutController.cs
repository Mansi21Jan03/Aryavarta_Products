using Microsoft.AspNetCore.Mvc;

namespace Aryavarta.Areas.User_Side.Controllers
{
    [Area("User_Side")]
    [Route("User_Side/[controller]/[action]")]

    public class AboutController : Controller
    {


        public IActionResult About()
        {
            return View();
        }
    }
}
