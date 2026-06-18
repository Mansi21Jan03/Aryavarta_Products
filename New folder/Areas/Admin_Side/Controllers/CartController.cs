using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Aryavarta.DAL;
using Aryavarta.Areas.Admin_Side.Models;

namespace Aryavarta.Areas.Admin_Side.Controllers
{
    [Area("Admin_Side")]
    [Route("Admin_Side/[controller]/[action]")]
    public class CartController : Controller
    {
        Admin_DAL dalAdmin = new Admin_DAL();

        private IConfiguration Configuration;

        public CartController(IConfiguration _configuration)
        {
            Configuration = _configuration;

        }

        #region Select All
        public IActionResult Index()
        {
            string connectionstr = Configuration.GetConnectionString("myConnectionString");

            DataTable dt = dalAdmin.PR_AVP_Cart_SelectAll(connectionstr);
            return View("AVP_CartList", dt);
        }
        #endregion

        #region SelectByPK
        public IActionResult Add(int? CartID)
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

            #region DropDown for Product Price fiiled by Series no

            List<AVP_Products_SelectDropDownForProductPrice> list2 = new List<AVP_Products_SelectDropDownForProductPrice>();
            ViewBag.PriceList = list2;

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

            if (CartID != null)
            {

                DataTable dt = dalAdmin.PR_AVP_Cart_SelectByPK(connectionstr, CartID);

                CartModel model = new CartModel();

                foreach (DataRow dr in dt.Rows)
                {
                    model.CartID = Convert.ToInt32(dr["CartID"]);
                    model.ProductID = Convert.ToInt32(dr["ProductID"]);
                    model.UserID = Convert.ToInt32(dr["UserID"]);
                    model.Quantity = Convert.ToInt32(dr["Quantity"]);
                    model.TotalPrice = Convert.ToInt32(dr["TotalPrice"]);
                    model.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    model.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);
                }

                /*DataTable dt4 = new DataTable();
                SqlConnection conn4 = new SqlConnection(connectionstr);
                conn4.Open();
                SqlCommand objCmd4 = conn4.CreateCommand();
                objCmd4.CommandType = CommandType.StoredProcedure;
                objCmd4.CommandText = "PR_AVP_Products_SelectDropDownForProductPrice";
                objCmd4.Parameters.AddWithValue("@ProductID", model.ProductID);
                SqlDataReader objSDR4 = objCmd4.ExecuteReader();
                dt4.Load(objSDR4);

                List<AVP_Products_SelectDropDownForProductPrice> list4 = new List<AVP_Products_SelectDropDownForProductPrice>();

                foreach (DataRow dr in dt4.Rows)
                {
                    AVP_Products_SelectDropDownForProductPrice vlst = new AVP_Products_SelectDropDownForProductPrice();
                    vlst.ProductID = Convert.ToInt32(dr["ProductID"]);
                    vlst.ProductPrice = Convert.ToInt32(dr["ProductPrice"]);
                    list4.Add(vlst);
                }

                ViewBag.PriceList = list4;*/

                return View("AVP_CartAddEdit", model);
            }
            return View("AVP_CartAddEdit");
        }
        #endregion

        #region Insert, Update => Save Method
        [HttpPost]
        public IActionResult Save(CartModel modelCart)
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

            if (modelCart.CartID == null)
            {
                objcmd.CommandText = "PR_AVP_Cart_Insert";
                objcmd.Parameters.Add("@CreationDate", SqlDbType.Date).Value = DBNull.Value;
            }
            else
            {
                objcmd.CommandText = "PR_AVP_Cart_UpdateByPK";
                objcmd.Parameters.Add("@CartID", SqlDbType.Int).Value = modelCart.CartID;
            }

            //get the parameters
            objcmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = modelCart.ProductID;
            objcmd.Parameters.Add("@UserID", SqlDbType.Int).Value = modelCart.UserID;
            objcmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = modelCart.Quantity;
            objcmd.Parameters.Add("@TotalPrice", SqlDbType.Int).Value = modelCart.TotalPrice;
            objcmd.Parameters.Add("@ModificationDate", SqlDbType.Date).Value = DBNull.Value;

            //execute a command

            if (Convert.ToBoolean(objcmd.ExecuteNonQuery()))
            {
                if (modelCart.CartID == null)
                {
                    TempData["CartInsertMsg"] = "Record Insert Successfully";
                }
                else
                {
                    TempData["CartInsertMsg"] = "Record Update Successfully";
                }
            }

            //close connection
            conn.Close();

            return RedirectToAction("Add");
        }
        #endregion

        #region DeleteByPK
        public IActionResult Delete(int CartID)
        {
            string connectionstr = Configuration.GetConnectionString("myConnectionString");
            DataTable dt = dalAdmin.PR_AVP_Cart_DeleteByPK(connectionstr, CartID);
            return RedirectToAction("Index");
        }
        #endregion

        #region DropDownByProduct
        public IActionResult DropDownByProduct(int ProductID)
        {
            string connectionstr = Configuration.GetConnectionString("myConnectionString");

            //Prepare a connection
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();

            //Prepare a command

            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_AVP_Products_SelectDropDownForProductPrice";
            objcmd.Parameters.AddWithValue("@ProductID", ProductID);
            SqlDataReader objSDR = objcmd.ExecuteReader();
            dt.Load(objSDR);
            conn.Close();

            List<AVP_Products_SelectDropDownForProductPrice> list = new List<AVP_Products_SelectDropDownForProductPrice>();
            foreach (DataRow dr in dt.Rows)
            {
                AVP_Products_SelectDropDownForProductPrice vlst = new AVP_Products_SelectDropDownForProductPrice();
                vlst.ProductID = Convert.ToInt32(dr["ProductID"]);
                vlst.ProductPrice = Convert.ToInt32(dr["ProductPrice"]);
                list.Add(vlst);
            }

            var vModel = list;
            return Json(vModel);

        }

        #endregion
    }
}
