using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Data;
using Aryavarta.Areas.Admin_Side.Models;
using Aryavarta.DAL;
using System.Data.SqlClient;

namespace Aryavarta.Areas.Admin_Side.Controllers
{
    [Area("Admin_Side")]
    [Route("Admin_Side/[controller]/[action]")]
    public class ProductsController : Controller
    {
        Admin_DAL dalAdmin = new Admin_DAL();

        private IConfiguration Configuration;

        public ProductsController(IConfiguration _configuration)
        {
            Configuration = _configuration;

        }

        #region SelectAll
        public IActionResult Index()
        {
            string connectionstr = Configuration.GetConnectionString("myConnectionString");
            DataTable dt = dalAdmin.PR_AVP_Products_SelectAll(connectionstr);
            return View("AVP_ProductsList", dt);
        }
        #endregion

        #region SelectByPK
        public IActionResult Add(int? ProductID)
        {

            #region DropDown for Product Material
            string connectionstr = Configuration.GetConnectionString("myConnectionString");

            //Prepare a connection
            DataTable dt1 = new DataTable();
            SqlConnection conn1 = new SqlConnection(connectionstr);
            conn1.Open();

            //Prepare a command

            SqlCommand objcmd1 = conn1.CreateCommand();
            objcmd1.CommandType = CommandType.StoredProcedure;
            objcmd1.CommandText = "PR_AVP_ProductMaterial_SelectForDropDown";
            SqlDataReader objSDR1 = objcmd1.ExecuteReader();
            dt1.Load(objSDR1);
            conn1.Close();

            List<AVP_ProductMaterialDropDownModel> list = new List<AVP_ProductMaterialDropDownModel>();
            foreach (DataRow dr in dt1.Rows)
            {
                AVP_ProductMaterialDropDownModel vlst = new AVP_ProductMaterialDropDownModel();
                vlst.MaterialID = Convert.ToInt32(dr["MaterialID"]);
                vlst.MaterialType = dr["MaterialType"].ToString();
                list.Add(vlst);
            }
            ViewBag.MaterialList = list;
            #endregion

            #region DropDown for Product Finish

            //Prepare a connection
            DataTable dt2 = new DataTable();
            SqlConnection conn2 = new SqlConnection(connectionstr);
            conn2.Open();

            //Prepare a command

            SqlCommand objcmd2 = conn2.CreateCommand();
            objcmd2.CommandType = CommandType.StoredProcedure;
            objcmd2.CommandText = "PR_AVP_ProductFinish_SelectForDropDown";
            SqlDataReader objSDR2 = objcmd2.ExecuteReader();
            dt2.Load(objSDR2);
            conn2.Close();

            List<AVP_ProductFinishDropDown> list1 = new List<AVP_ProductFinishDropDown>();
            foreach (DataRow dr in dt2.Rows)
            {
                AVP_ProductFinishDropDown vlst = new AVP_ProductFinishDropDown();
                vlst.FinishID = Convert.ToInt32(dr["FinishID"]);
                vlst.FinishType = dr["FinishType"].ToString();
                list1.Add(vlst);
            }
            ViewBag.FinishList = list1;
            #endregion

            #region DropDown for Product Type

            //Prepare a connection
            DataTable dt3 = new DataTable();
            SqlConnection conn3 = new SqlConnection(connectionstr);
            conn3.Open();

            //Prepare a command

            SqlCommand objcmd3 = conn3.CreateCommand();
            objcmd3.CommandType = CommandType.StoredProcedure;
            objcmd3.CommandText = "PR_AVP_ProductType_SelectForDropDown";
            SqlDataReader objSDR3 = objcmd3.ExecuteReader();
            dt3.Load(objSDR3);
            conn3.Close();

            List<AVP_ProductTypeDropDownModel> list2 = new List<AVP_ProductTypeDropDownModel>();
            foreach (DataRow dr in dt3.Rows)
            {
                AVP_ProductTypeDropDownModel vlst = new AVP_ProductTypeDropDownModel();
                vlst.ProductTypeID = Convert.ToInt32(dr["ProductTypeID"]);
                vlst.ProductType = dr["ProductType"].ToString();
                list2.Add(vlst);
            }
            ViewBag.ProductTypeList = list2;
            #endregion

            #region Record Select By PK
            if (ProductID != null)
            {
                DataTable dt = dalAdmin.PR_AVP_Products_SelectByPK(connectionstr, ProductID);

                ProductsModel model = new ProductsModel();

                foreach (DataRow dr in dt.Rows)
                {
                    model.ProductID = Convert.ToInt32(dr["ProductID"]);
                    model.MaterialID = Convert.ToInt32(dr["MaterialID"]);
                    model.FinishID = Convert.ToInt32(dr["FinishID"]);
                    model.SeriesNo = dr["SeriesNo"].ToString();
                    model.ProductTypeID = Convert.ToInt32(dr["ProductTypeID"]);
                    model.ProductPrice = Convert.ToInt32(dr["ProductPrice"]);
                    model.ProductImg = dr["ProductImg"].ToString();
                    model.AvailableStock = Convert.ToInt32(dr["AvailableStock"]);
                    model.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    model.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);
                }

                return View("AVP_ProductsAddEdit", model);
            }
            #endregion

            return View("AVP_ProductsAddEdit");
        }
        #endregion

        #region Insert, Update => Save Method
        [HttpPost]
        public IActionResult Save(ProductsModel modelProducts)
        {

            string connectionstr = this.Configuration.GetConnectionString("myConnectionString");

            //prepare a connection
            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();

            //prepare a command
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;

            if (modelProducts.File != null)
            {
                string FilePath = "wwwroot\\Upload";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                string fileNameWithPath = Path.Combine(path, modelProducts.File.FileName);
                modelProducts.ProductImg = FilePath.Replace("wwwroot\\", "/") + "/" + modelProducts.File.FileName;

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    modelProducts.File.CopyTo(stream);
                }

            }

            if (modelProducts.ProductID == null)
            {
                objcmd.CommandText = "PR_AVP_Products_Insert";
                objcmd.Parameters.Add("@CreationDate", SqlDbType.Date).Value = DBNull.Value;
            }
            else
            {
                objcmd.CommandText = "PR_AVP_Products_UpdateByPK";
                objcmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = modelProducts.ProductID;
            }

            //get the parameters
            objcmd.Parameters.Add("@MaterialID", SqlDbType.Int).Value = modelProducts.MaterialID;
            objcmd.Parameters.Add("@FinishID", SqlDbType.Int).Value = modelProducts.FinishID;
            objcmd.Parameters.Add("@SeriesNo", SqlDbType.VarChar).Value = modelProducts.SeriesNo;
            objcmd.Parameters.Add("@ProductTypeID", SqlDbType.Int).Value = modelProducts.ProductTypeID;
            objcmd.Parameters.Add("@ProductPrice", SqlDbType.Int).Value = modelProducts.ProductPrice;
            objcmd.Parameters.Add("@ProductImg", SqlDbType.VarChar).Value = modelProducts.ProductImg;
            objcmd.Parameters.Add("@AvailableStock", SqlDbType.Int).Value = modelProducts.AvailableStock;
            objcmd.Parameters.Add("@ModificationDate", SqlDbType.Date).Value = DBNull.Value;

            //execute a command

            if (Convert.ToBoolean(objcmd.ExecuteNonQuery()))
            {
                if (modelProducts.ProductID == null)
                {
                    TempData["ProductInsertMsg"] = "Record Insert Successfully";
                }
                else
                {
                    TempData["ProductInsertMsg"] = "Record Update Successfully";
                }
            }

            //close connection
            conn.Close();

            return RedirectToAction("Add");

        }
        #endregion

        #region DeleteByPK
        public IActionResult Delete(int ProductID)
        {
            string connectionstr = Configuration.GetConnectionString("myConnectionString");
            DataTable dt = dalAdmin.PR_AVP_Products_DeleteByPK(connectionstr, ProductID);
            return RedirectToAction("Index");
        }
        #endregion



    }
}
