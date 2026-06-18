using Aryavarta.Areas.Admin_Side.Models;
using Aryavarta.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Data;

namespace Aryavarta.Areas.Admin_Side.Controllers
{
    [Area("Admin_Side")]
    [Route("Admin_Side/[controller]/[action]")]

    public class UserController : Controller
    {
        Admin_DAL dalAdmin = new Admin_DAL();

        private IConfiguration Configuration;

        public UserController(IConfiguration _configuration)
        {
            Configuration = _configuration;

        }

        #region SelectAll
        public IActionResult Index()
        {
            string connectionstr = Configuration.GetConnectionString("myConnectionString");
            DataTable dt = dalAdmin.PR_AVP_User_SelectAll(connectionstr);
            return View("AVP_UserList", dt);
        }
        #endregion

    }
}
