using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Aryavarta.DAL;
using Aryavarta.Areas.Admin_Side.Models;

namespace Aryavarta.Areas.Admin_Side.Controllers
{
    [Area("Admin_Side")]
    [Route("Admin_Side/[controller]/[action]")]
    public class OrderItemsController : Controller
    {
        Admin_DAL dalAdmin = new Admin_DAL();

        private IConfiguration Configuration;

        public OrderItemsController(IConfiguration _configuration)
        {
            Configuration = _configuration;

        }

        #region Select All
        public IActionResult Index()
        {
            string connectionstr = Configuration.GetConnectionString("myConnectionString");

            DataTable dt = dalAdmin.PR_AVP_OrderItems_SelectAll(connectionstr);
            return View("AVP_OrderItemsList", dt);
        }
        #endregion

        #region SelectByPK
        public IActionResult Add(int? OrderItemID)
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

            #region DropDown for Product Image

            //Prepare a connection
            DataTable dt3 = new DataTable();
            SqlConnection conn3 = new SqlConnection(connectionstr);
            conn3.Open();

            //Prepare a command

            SqlCommand objcmd3 = conn3.CreateCommand();
            objcmd3.CommandType = CommandType.StoredProcedure;
            objcmd3.CommandText = "PR_AVP_Products_SelectDropDownForProductImg";
            SqlDataReader objSDR3 = objcmd3.ExecuteReader();
            dt3.Load(objSDR3);
            conn3.Close();

            List<AVP_Products_SelectDropDownForProductImg> list3 = new List<AVP_Products_SelectDropDownForProductImg>();
            foreach (DataRow dr in dt3.Rows)
            {
                AVP_Products_SelectDropDownForProductImg vlst = new AVP_Products_SelectDropDownForProductImg();
                vlst.ProductID = Convert.ToInt32(dr["ProductID"]);
                vlst.ProductImg = dr["ProductImg"].ToString();
                list3.Add(vlst);
            }
            ViewBag.ImageList = list3;
            #endregion

            if (OrderItemID != null)
            {

                DataTable dt = dalAdmin.PR_AVP_Cart_SelectByPK(connectionstr, OrderItemID);

                OrderItemsModel model = new OrderItemsModel();

                foreach (DataRow dr in dt.Rows)
                {
                    model.OrderItemID = Convert.ToInt32(dr["OrderItemID"]);
                    model.OrderID = Convert.ToInt32(dr["OrderID"]);
                    model.ProductID = Convert.ToInt32(dr["ProductID"]);
                    model.UserID = Convert.ToInt32(dr["UserID"]);
                    model.Price = Convert.ToDecimal(dr["Price"]);
                    model.Total = Convert.ToDecimal(dr["Total"]);
                    model.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    model.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);
                }


                return View("AVP_OrderItemsAddEdit", model);
            }
            return View("AVP_OrderItemsAddEdit");
        }
        #endregion

        #region SelectByUserID
        public IActionResult AddUser(int? UserID)
        {
            string connectionstr = Configuration.GetConnectionString("myConnectionString");

            if (UserID != null)
            {

                DataTable dt = dalAdmin.PR_AVP_OrderItems_SelectByUserID(connectionstr, UserID);

                UserModel model = new UserModel();

                foreach (DataRow dr in dt.Rows)
                {
                    model.UserID = Convert.ToInt32(dr["UserID"]);
                    model.UserName = dr["UserName"].ToString();
                    model.MobileNo = dr["MobileNo"].ToString();
                    model.Address = dr["Address"].ToString();
                    model.EmailID = dr["EmailID"].ToString();
                }
                return View("AVP_OrderItemsAddEdit", model);
            }
            return View("AVP_OrderItemsAddEdit");
        }
        #endregion

        #region Insert, Update => Save Method
        [HttpPost]
        public IActionResult Save(OrderItemsModel modelOrderItems)
        {

            string connectionstr = this.Configuration.GetConnectionString("myConnectionString");

            //prepare a connection
            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();

            //prepare a command
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;

            /*if (modelCart.File != null)
            {
                string FilePath = "wwwroot\\Upload";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                string fileNameWithPath = Path.Combine(path, modelCart.File.FileName);
                modelCart.Image = FilePath.Replace("wwwroot\\", "/") + "/" + modelFeedback.File.FileName;

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    modelCart.File.CopyTo(stream);
                }

            }*/

            if (modelOrderItems.OrderItemID == null)
            {
                objcmd.CommandText = "PR_AVP_OrderItems_Insert";
                objcmd.Parameters.Add("@CreationDate", SqlDbType.Date).Value = DBNull.Value;
            }
            else
            {
                objcmd.CommandText = "PR_AVP_OrderItems_UpdateByPK";
                objcmd.Parameters.Add("@OrderItemID", SqlDbType.Int).Value = modelOrderItems.OrderItemID;
            }

            //get the parameters
            objcmd.Parameters.Add("@OrderID", SqlDbType.Int).Value = modelOrderItems.OrderID;
            objcmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = modelOrderItems.ProductID;
            objcmd.Parameters.Add("@UserID", SqlDbType.Int).Value = modelOrderItems.UserID;
            objcmd.Parameters.Add("@Price", SqlDbType.Decimal).Value = modelOrderItems.Price;
            objcmd.Parameters.Add("@Total", SqlDbType.Decimal).Value = modelOrderItems.Total;
            objcmd.Parameters.Add("@ModificationDate", SqlDbType.Date).Value = DBNull.Value;

            //execute a command

            if (Convert.ToBoolean(objcmd.ExecuteNonQuery()))
            {
                if (modelOrderItems.OrderItemID == null)
                {
                    TempData["OrderItemsInsertMsg"] = "Record Insert Successfully";
                }
                else
                {
                    TempData["OrderItemsInsertMsg"] = "Record Update Successfully";
                }
            }

            //close connection
            conn.Close();

            return RedirectToAction("Add");
        }
        #endregion

        #region DeleteByPK
        public IActionResult Delete(int OrderItemsID)
        {
            string connectionstr = Configuration.GetConnectionString("myConnectionString");
            DataTable dt = dalAdmin.PR_AVP_OrderItems_DeleteByPK(connectionstr, OrderItemsID);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
