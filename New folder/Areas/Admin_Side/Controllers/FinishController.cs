using Aryavarta.Areas.Admin_Side.Models;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Aryavarta.DAL;

namespace Aryavarta.Areas.Admin_Side.Controllers
{
    [Area("Admin_Side")]
    [Route("Admin_Side/[controller]/[action]")]

    public class FinishController : Controller
    {
        Admin_DAL dalAdmin = new Admin_DAL();

        private IConfiguration Configuration;

        public FinishController(IConfiguration _configuration)
        {
            Configuration = _configuration;

        }

        #region Select All
        public IActionResult Index()
        {
            string connectionstr = Configuration.GetConnectionString("myConnectionString");

            DataTable dt = dalAdmin.PR_AVP_ProductFinish_SelectAll(connectionstr);
            return View("AVP_FinishList", dt);
        }
        #endregion

        #region SelectByPK
        public IActionResult Add(int? FinishID)
        {
            if (FinishID != null)
            {
                string connectionstr = Configuration.GetConnectionString("myConnectionString");

                DataTable dt = dalAdmin.PR_AVP_ProductFinish_SelectByPK(connectionstr, FinishID);

                FinishModel model = new FinishModel();

                foreach (DataRow dr in dt.Rows)
                {
                    model.FinishID = Convert.ToInt32(dr["FinishID"]);
                    model.FinishType = dr["FinishType"].ToString();
                    model.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    model.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);
                }

                return View("AVP_FinishAddEdit", model);
            }
            return View("AVP_FinishAddEdit");
        }
        #endregion

        #region Insert, Update => Save Method
        [HttpPost]
        public IActionResult Save(FinishModel modelFinish)
        {

            string connectionstr = this.Configuration.GetConnectionString("myConnectionString");

            //prepare a connection
            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();

            //prepare a command
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;

            if (modelFinish.FinishID == null)
            {
                objcmd.CommandText = "PR_AVP_ProductFinish_Insert";
                objcmd.Parameters.Add("@CreationDate", SqlDbType.Date).Value = DBNull.Value;
            }
            else
            {
                objcmd.CommandText = "PR_AVP_ProductFinish_UpdateByPK";
                objcmd.Parameters.Add("@FinishID", SqlDbType.Int).Value = modelFinish.FinishID;
            }

            //get the parameters
            objcmd.Parameters.Add("@FinishType", SqlDbType.VarChar).Value = modelFinish.FinishType;
            objcmd.Parameters.Add("@ModificationDate", SqlDbType.Date).Value = DBNull.Value;

            //execute a command

            if (Convert.ToBoolean(objcmd.ExecuteNonQuery()))
            {
                if (modelFinish.FinishID == null)
                {
                    TempData["FinishInsertMsg"] = "Record Insert Successfully";
                }
                else
                {
                    TempData["FinishInsertMsg"] = "Record Update Successfully";
                }
            }

            //close connection
            conn.Close();
            return View("AVP_FinishAddEdit");

        }
        #endregion

        #region DeleteByPK
        public IActionResult Delete(int FinishID)
        {
            string connectionstr = Configuration.GetConnectionString("myConnectionString");
            DataTable dt = dalAdmin.PR_AVP_ProductFinish_DeleteByPK(connectionstr, FinishID);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
