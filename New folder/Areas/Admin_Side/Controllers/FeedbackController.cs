using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Aryavarta.DAL;
using Aryavarta.Areas.Admin_Side.Models;

namespace Aryavarta.Areas.Admin_Side.Controllers
{
    [Area("Admin_Side")]
    [Route("Admin_Side/[controller]/[action]")]
    public class FeedbackController : Controller
    {
        Admin_DAL dalAdmin = new Admin_DAL();

        private IConfiguration Configuration;

        public FeedbackController(IConfiguration _configuration)
        {
            Configuration = _configuration;

        }

        #region Select All
        public IActionResult Index()
        {
            string connectionstr = Configuration.GetConnectionString("myConnectionString");

            DataTable dt = dalAdmin.PR_AVP_Feedback_SelectAll(connectionstr);
            return View("AVP_FeedbackList", dt);
        }
        #endregion

        #region SelectByPK
        public IActionResult Add(int? FeedbackID)
        {
            #region DropDown for User Name
            string connectionstr = Configuration.GetConnectionString("myConnectionString");

            //Prepare a connection
            DataTable dt1 = new DataTable();
            SqlConnection conn1 = new SqlConnection(connectionstr);
            conn1.Open();

            //Prepare a command

            SqlCommand objcmd1 = conn1.CreateCommand();
            objcmd1.CommandType = CommandType.StoredProcedure;
            objcmd1.CommandText = "PR_AVP_User_SelectForDropDown";
            SqlDataReader objSDR1 = objcmd1.ExecuteReader();
            dt1.Load(objSDR1);
            conn1.Close();

            List<AVP_UserDropDown> list = new List<AVP_UserDropDown>();
            foreach (DataRow dr in dt1.Rows)
            {
                AVP_UserDropDown vlst = new AVP_UserDropDown();
                vlst.UserID = Convert.ToInt32(dr["UserID"]);
                vlst.UserName = dr["UserName"].ToString();
                list.Add(vlst);
            }
            ViewBag.UserList = list;
            #endregion

            #region DropDown for Product Series Number

            //Prepare a connection
            DataTable dt2 = new DataTable();
            SqlConnection conn2 = new SqlConnection(connectionstr);
            conn2.Open();

            //Prepare a command

            SqlCommand objcmd2 = conn2.CreateCommand();
            objcmd2.CommandType = CommandType.StoredProcedure;
            objcmd2.CommandText = "PR_AVP_Products_SelectDropDownForSeriesNo";
            SqlDataReader objSDR2 = objcmd2.ExecuteReader();
            dt2.Load(objSDR2);
            conn2.Close();

            List<AVP_Products_SelectDropDownForSeriesNo> list1 = new List<AVP_Products_SelectDropDownForSeriesNo>();
            foreach (DataRow dr in dt2.Rows)
            {
                AVP_Products_SelectDropDownForSeriesNo vlst = new AVP_Products_SelectDropDownForSeriesNo();
                vlst.ProductID = Convert.ToInt32(dr["ProductID"]);
                vlst.SeriesNo = dr["SeriesNo"].ToString();
                list1.Add(vlst);
            }
            ViewBag.SeriesList = list1;
            #endregion

            if (FeedbackID != null)
            {

                DataTable dt = dalAdmin.PR_AVP_Feedback_SelectByPK(connectionstr, FeedbackID);

                FeedbackModel model = new FeedbackModel();

                foreach (DataRow dr in dt.Rows)
                {
                    model.FeedbackID = Convert.ToInt32(dr["FeedbackID"]);
                    model.UserID = Convert.ToInt32(dr["UserID"]);
                    model.ProductID = Convert.ToInt32(dr["ProductID"]);
                    model.FeedbackMsg = dr["FeedbackMsg"].ToString();
                    model.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    model.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);
                }

                return View("AVP_FeedbackAddEdit", model);
            }
            return View("AVP_FeedbackAddEdit");
        }
        #endregion

        #region Insert, Update => Save Method
        [HttpPost]
        public IActionResult Save(FeedbackModel modelFeedback)
        {

            string connectionstr = this.Configuration.GetConnectionString("myConnectionString");

            //prepare a connection
            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();

            //prepare a command
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;

            if (modelFeedback.File != null)
            {
                string FilePath = "wwwroot\\Upload";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                string fileNameWithPath = Path.Combine(path, modelFeedback.File.FileName);
                modelFeedback.Image = FilePath.Replace("wwwroot\\", "/") + "/" + modelFeedback.File.FileName;

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    modelFeedback.File.CopyTo(stream);
                }

            }

            if (modelFeedback.FeedbackID == null)
            {
                objcmd.CommandText = "PR_AVP_Feedback_Insert";
                objcmd.Parameters.Add("@CreationDate", SqlDbType.Date).Value = DBNull.Value;
            }
            else
            {
                objcmd.CommandText = "PR_AVP_Feedback_UpdateByPK";
                objcmd.Parameters.Add("@FeedbackID", SqlDbType.Int).Value = modelFeedback.FeedbackID;
            }

            //get the parameters
            objcmd.Parameters.Add("@UserID", SqlDbType.Int).Value = modelFeedback.UserID;
            objcmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = modelFeedback.ProductID;
            objcmd.Parameters.Add("@Image", SqlDbType.VarChar).Value = modelFeedback.Image;
            objcmd.Parameters.Add("@FeedbackMsg", SqlDbType.VarChar).Value = modelFeedback.FeedbackMsg;
            objcmd.Parameters.Add("@ModificationDate", SqlDbType.Date).Value = DBNull.Value;

            //execute a command

            if (Convert.ToBoolean(objcmd.ExecuteNonQuery()))
            {
                if (modelFeedback.FeedbackID == null)
                {
                    TempData["FeedbackInsertMsg"] = "Record Insert Successfully";
                }
                else
                {
                    TempData["FeedbackInsertMsg"] = "Record Update Successfully";
                }
            }

            //close connection
            conn.Close();

            return RedirectToAction("Add");
        }
        #endregion

        #region DeleteByPK
        public IActionResult Delete(int FeedbackID)
        {
            string connectionstr = Configuration.GetConnectionString("myConnectionString");
            DataTable dt = dalAdmin.PR_AVP_Feedback_DeleteByPK(connectionstr, FeedbackID);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
