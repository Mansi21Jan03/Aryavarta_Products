using Aryavarta.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Data;
using Aryavarta.Areas.Admin_Side.Models;
using System.Data.SqlClient;

namespace Aryavarta.Areas.Admin_Side.Controllers
{
    [Area("Admin_Side")]
    [Route("Admin_Side/[controller]/[action]")]

    public class MaterialController : Controller
    {
        Admin_DAL dalAdmin = new Admin_DAL();

        private IConfiguration Configuration;

        public MaterialController(IConfiguration _configuration)
        {
            Configuration = _configuration;

        }

        #region Select All
        public IActionResult Index()
        {
            string connectionstr = Configuration.GetConnectionString("myConnectionString");
            
            DataTable dt = dalAdmin.PR_AVP_ProductMaterial_SelectAll(connectionstr);
            return View("AVP_MaterialList", dt);
        }
        #endregion

        #region SelectByPK
        public IActionResult Add(int? MaterialID)
        {
            if (MaterialID != null)
            {
                string connectionstr = Configuration.GetConnectionString("myConnectionString");
               
                DataTable dt = dalAdmin.PR_AVP_ProductMaterial_SelectByPK(connectionstr, MaterialID);

                MaterialModel model = new MaterialModel();

                foreach (DataRow dr in dt.Rows)
                {
                    model.MaterialID = Convert.ToInt32(dr["MaterialID"]);
                    model.MaterialType = dr["MaterialType"].ToString();
                    model.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    model.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);
                }

                return View("AVP_MaterialAddEdit", model);
            }
            return View("AVP_MaterialAddEdit");
        }
        #endregion

        #region Insert, Update => Save Method
        [HttpPost]
        public IActionResult Save(MaterialModel modelMaterial)
        {

            string connectionstr = this.Configuration.GetConnectionString("myConnectionString");

            //prepare a connection
            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();

            //prepare a command
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;

            if (modelMaterial.MaterialID == null)
            {
                objcmd.CommandText = "PR_AVP_ProductMaterial_Insert";
                objcmd.Parameters.Add("@CreationDate", SqlDbType.Date).Value = DBNull.Value;
            }
            else
            {
                objcmd.CommandText = "PR_AVP_ProductMaterial_UpdateByPK";
                objcmd.Parameters.Add("@MaterialID", SqlDbType.Int).Value = modelMaterial.MaterialID;
            }

            //get the parameters
            objcmd.Parameters.Add("@MaterialType", SqlDbType.VarChar).Value = modelMaterial.MaterialType;
            objcmd.Parameters.Add("@ModificationDate", SqlDbType.Date).Value = DBNull.Value;

            //execute a command
            
            if (Convert.ToBoolean(objcmd.ExecuteNonQuery()))
            {
                if (modelMaterial.MaterialID == null)
                {
                    TempData["MaterialInsertMsg"] = "Record Insert Successfully";
                }
                else
                {
                    TempData["MaterialInsertMsg"] = "Record Update Successfully";
                }
            }

            //close connection
            conn.Close();
            return View("AVP_MaterialAddEdit");
        
        }
        #endregion

        #region DeleteByPK
        public IActionResult Delete(int MaterialID)
        {
            string connectionstr = Configuration.GetConnectionString("myConnectionString");
            DataTable dt = dalAdmin.PR_AVP_ProductMaterial_DeleteByPK(connectionstr, MaterialID);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
