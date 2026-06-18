using Aryavarta.Areas.Admin_Side.Models;
using Aryavarta.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using static Aryavarta.Areas.Admin_Side.Models.OrderModel;

namespace Aryavarta.Areas.Admin_Side.Controllers
{
    [Area("Admin_Side")]
    [Route("Admin_Side/[controller]/[action]")]

    public class OrderController : Controller
    {
        Admin_DAL dalAdmin = new Admin_DAL();

        private IConfiguration Configuration;

        public OrderController(IConfiguration _configuration)
        {
            Configuration = _configuration;

        }

        #region Select All
        public IActionResult Index()
        {
            string connectionstr = Configuration.GetConnectionString("myConnectionString");

            DataTable dt = dalAdmin.PR_AVP_Order_SelectAll(connectionstr);
            return View("AVP_OrderList", dt);
        }
        #endregion

        #region SelectByPK
        public IActionResult Add(int? OrderID)
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

            #region Product & SeriesNo And ProductPrice
            //Prepare a connection
            DataTable dt2 = new DataTable();
            SqlConnection conn2 = new SqlConnection(connectionstr);
            conn2.Open();

            SqlCommand objcmd2 = conn2.CreateCommand();
            objcmd2.CommandType = CommandType.StoredProcedure;
            objcmd2.CommandText = "PR_AVP_Products_SelectForDropDown";
            SqlDataReader objSDR2 = objcmd2.ExecuteReader();
            dt2.Load(objSDR2);
            conn2.Close();

            List<PR_AVP_Products_SelectForDropDown> list1 = new List<PR_AVP_Products_SelectForDropDown>();
            foreach (DataRow dr in dt2.Rows)
            {
                PR_AVP_Products_SelectForDropDown vlst = new PR_AVP_Products_SelectForDropDown();
                vlst.ProductID = Convert.ToInt32(dr["ProductID"]);
                vlst.SeriesNo = dr["SeriesNo"].ToString();
                vlst.ProductPrice = Convert.ToInt32(dr["ProductPrice"]);
                list1.Add(vlst);
            }
            ViewBag.ProductList = list1;
            #endregion

            #region Record Select by PK 

            if (OrderID != null)
            {

                DataTable dt = dalAdmin.PR_AVP_Order_SelectByPK(connectionstr, OrderID);

                OrderModel model = new OrderModel();

                foreach (DataRow dr in dt.Rows)
                {
                    model.OrderID = Convert.ToInt32(dr["OrderID"]);
                    model.UserID = Convert.ToInt32(dr["UserID"]);
                    model.ProductID = Convert.ToInt32(dr["ProductID"]);
                    model.Quantity = Convert.ToInt32(dr["Quantity"]);
                    model.TotalAmount = Convert.ToDecimal(dr["TotalAmount"]);
                    model.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    model.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);
                }

                return View("AVP_OrderAddEdit", model);
            }
            #endregion

            return View("AVP_OrderAddEdit");
        }

        #endregion

        /*#region ProductPrice & Quantity

        public IActionResult AddOrderPage()
        {
            string connectionstr = Configuration.GetConnectionString("myConnectionString");

            DataTable dt1 = dalAdmin.PR_AVP_Products_SelectForDropDown(connectionstr);

            List<PR_AVP_Products_SelectForDropDown> list = new List<PR_AVP_Products_SelectForDropDown>();
            foreach (DataRow dr in dt1.Rows)
            {
                PR_AVP_Products_SelectForDropDown vlst = new PR_AVP_Products_SelectForDropDown();
                vlst.ProductID = Convert.ToInt32(dr["ProductID"]);
                vlst.SeriesNo = dr["SeriesNo"].ToString();
                vlst.ProductPrice = Convert.ToInt32(dr["ProductPrice"]);
                list.Add(vlst);
            }
            ViewBag.ProductList = list;

            return View("AVP_OrderAddEdit");
        }

        #endregion*/

        #region GetProductPrice
        [HttpGet]
        public IActionResult GetProductPrice(int ProductId)
        {
            string connectionstr = Configuration.GetConnectionString("myConnectionString");
            DataTable dt = dalAdmin.PR_AVP_Products_SelectPriceByPK(connectionstr, ProductId);
            OrderModel modelOrder = new OrderModel();

            foreach (DataRow dr in dt.Rows)
            {
                modelOrder.ProductPrice = Convert.ToInt32(dr["ProductPrice"]);
            }
            return Json(modelOrder.ProductPrice);

        }
        #endregion

        #region Insert, Update => Save Method
        [HttpPost]
        public IActionResult Save(OrderModel modelOrder)
        {

            string connectionstr = this.Configuration.GetConnectionString("myConnectionString");

            //prepare a connection
            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();

            //prepare a command
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;

            if (modelOrder.OrderID == 0)
            {
                objcmd.CommandText = "PR_AVP_Order_Insert";
                objcmd.Parameters.Add("@CreationDate", SqlDbType.Date).Value = DBNull.Value;
            }
            else
            {
                objcmd.CommandText = "PR_AVP_Order_UpdateByPK";
                objcmd.Parameters.Add("@OrderID", SqlDbType.Int).Value = modelOrder.OrderID;
            }

            //get the parameters
            objcmd.Parameters.Add("@UserID", SqlDbType.Int).Value = modelOrder.UserID;
            objcmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = modelOrder.ProductID;
            objcmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = modelOrder.Quantity;
            objcmd.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = modelOrder.TotalAmount;
            objcmd.Parameters.Add("@ModificationDate", SqlDbType.Date).Value = DBNull.Value;



            //execute a command

            if (Convert.ToBoolean(objcmd.ExecuteNonQuery()))
            {
                if (modelOrder.OrderID == 0)
                {
                    TempData["OrderInsertMsg"] = "Record Insert Successfully";
                }
                else
                {
                    TempData["OrderInsertMsg"] = "Record Update Successfully";
                }
            }

            //close connection
            conn.Close();
            return RedirectToAction("Add");

        }
        #endregion

        #region DeleteByPK
        public IActionResult Delete(int OrderID)
        {
            string connectionstr = Configuration.GetConnectionString("myConnectionString");
            DataTable dt = dalAdmin.PR_AVP_Order_DeleteByPK(connectionstr, OrderID);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
