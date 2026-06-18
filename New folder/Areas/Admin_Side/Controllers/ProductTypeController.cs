using Aryavarta.Areas.Admin_Side.Models;
using Aryavarta.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace Aryavarta.Areas.Admin_Side.Controllers
{
    [Area("Admin_Side")]
    [Route("Admin_Side/[controller]/[action]")]

    public class ProductTypeController : Controller
    {
        Admin_DAL dalAdmin = new Admin_DAL();

        private IConfiguration Configuration;

        public ProductTypeController(IConfiguration _configuration)
        {
            Configuration = _configuration;

        }

        #region Select All
        public IActionResult Index()
        {
            string connectionstr = Configuration.GetConnectionString("myConnectionString");

            DataTable dt = dalAdmin.PR_AVP_ProductType_SelectAll(connectionstr);
            return View("AVP_ProductTypeList", dt);
        }
        #endregion

        #region SelectByPK
        public IActionResult Add(int? ProductTypeID)
        {
            if (ProductTypeID != null)
            {
                string connectionstr = Configuration.GetConnectionString("myConnectionString");

                DataTable dt = dalAdmin.PR_AVP_ProductType_SelectByPK(connectionstr, ProductTypeID);

                ProductTypeModel model = new ProductTypeModel();

                foreach (DataRow dr in dt.Rows)
                {
                    model.ProductTypeID = Convert.ToInt32(dr["ProductTypeID"]);
                    model.ProductType = dr["ProductType"].ToString();
                    model.ProductTypeImg = dr["ProductTypeImg"].ToString();
                    model.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    model.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);
                }

                return View("AVP_ProductTypeAddEdit", model);
            }
            return View("AVP_ProductTypeAddEdit");
        }
        #endregion

        #region Insert, Update => Save Method
        [HttpPost]
        public IActionResult Save(ProductTypeModel modelProductType)
        {

            string connectionstr = this.Configuration.GetConnectionString("myConnectionString");

            //prepare a connection
            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();

            //prepare a command
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;

            if (modelProductType.File != null)
            {
                string FilePath = "wwwroot\\Upload";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                string fileNameWithPath = Path.Combine(path, modelProductType.File.FileName);
                modelProductType.ProductTypeImg = FilePath.Replace("wwwroot\\", "/") + "/" + modelProductType.File.FileName;

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    modelProductType.File.CopyTo(stream);
                }

            }

            if (modelProductType.ProductTypeID == null)
            {
                objcmd.CommandText = "PR_AVP_ProductType_Insert";
                objcmd.Parameters.Add("@CreationDate", SqlDbType.Date).Value = DBNull.Value;
            }
            else
            {
                objcmd.CommandText = "PR_AVP_ProductType_UpdateByPK";
                objcmd.Parameters.Add("@ProductTypeID", SqlDbType.Int).Value = modelProductType.ProductTypeID;
            }

            //get the parameters
            objcmd.Parameters.Add("@ProductType", SqlDbType.VarChar).Value = modelProductType.ProductType;
            objcmd.Parameters.Add("@ProductTypeImg", SqlDbType.VarChar).Value = modelProductType.ProductTypeImg;
            objcmd.Parameters.Add("@ModificationDate", SqlDbType.Date).Value = DBNull.Value;

            //execute a command

            if (Convert.ToBoolean(objcmd.ExecuteNonQuery()))
            {
                if (modelProductType.ProductTypeID == null)
                {
                    TempData["ProductTypeInsertMsg"] = "Record Insert Successfully";
                }
                else
                {
                    TempData["ProductTypeInsertMsg"] = "Record Update Successfully";
                }
            }

            //close connection
            conn.Close();
            return View("AVP_ProductTypeAddEdit");

        }
        #endregion

        #region DeleteByPK
        public IActionResult Delete(int ProductTypeID)
        {
            string connectionstr = Configuration.GetConnectionString("myConnectionString");
            DataTable dt = dalAdmin.PR_AVP_ProductType_DeleteByPK(connectionstr, ProductTypeID);
            return RedirectToAction("Index");
        }
        #endregion

    }
}
