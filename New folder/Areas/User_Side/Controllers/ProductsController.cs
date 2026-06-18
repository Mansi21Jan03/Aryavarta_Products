using Aryavarta.Areas.Admin_Side.Models;
using Aryavarta.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Data;

namespace Aryavarta.Areas.User_Side.Controllers
{
    [Area("User_Side")]
    [Route("User_Side/[controller]/[action]")]

    public class ProductsController : Controller
    {
        User_DAL dalUser = new User_DAL();

        private IConfiguration Configuration;

        public ProductsController(IConfiguration _configuration)
        {
            Configuration = _configuration;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Products()
        {
            return View();
        }

        public IActionResult MortiseHandles()
        {
            string connectionstr = Configuration.GetConnectionString("myConnectionString");
            DataTable dt = dalUser.PR_AVP_Products_SelectAll(connectionstr);
            return View("MortiseHandles", dt);
        }
    }
}
