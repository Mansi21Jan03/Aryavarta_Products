using Aryavarta.Areas.Admin_Side.Models;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Aryavarta.DAL
{
    public class Admin_DALBase
    {
        //Products Table 

        #region dbo.PR_AVP_Products_SelectAll
        public DataTable PR_AVP_Products_SelectAll(string conn)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_AVP_Products_SelectAll");

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region PR_AVP_Products_SelectByPK

        public DataTable PR_AVP_Products_SelectByPK(string conn, int? PID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_AVP_Products_SelectByPK");
                sqlDB.AddInParameter(dbCMD, "ProductID", SqlDbType.Int, PID);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }

            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region PR_AVP_Products_DeleteByPK

        public DataTable PR_AVP_Products_DeleteByPK(string conn, int PID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_AVP_Products_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "ProductID", SqlDbType.Int, PID);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }

            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        //Material Table

        #region dbo.PR_AVP_ProductMaterial_SelectAll
        public DataTable PR_AVP_ProductMaterial_SelectAll(string conn)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_AVP_ProductMaterial_SelectAll");

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
        
        #region PR_AVP_ProductMaterial_SelectByPK

        public DataTable PR_AVP_ProductMaterial_SelectByPK(string conn, int? MID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_AVP_ProductMaterial_SelectByPK");
                sqlDB.AddInParameter(dbCMD, "MaterialID", SqlDbType.Int, MID);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }

            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region PR_AVP_ProductMaterial_DeleteByPK

        public DataTable PR_AVP_ProductMaterial_DeleteByPK(string conn, int MID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_AVP_ProductMaterial_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "MaterialID", SqlDbType.Int, MID);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }

            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        //ProductFinish Table

        #region dbo.PR_AVP_ProductFinish_SelectAll
        public DataTable PR_AVP_ProductFinish_SelectAll(string conn)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_AVP_ProductFinish_SelectAll");

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region PR_AVP_ProductFinish_SelectByPK

        public DataTable PR_AVP_ProductFinish_SelectByPK(string conn, int? FID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_AVP_ProductFinish_SelectByPK");
                sqlDB.AddInParameter(dbCMD, "FinishID", SqlDbType.Int, FID);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }

            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region PR_AVP_ProductFinish_DeleteByPK

        public DataTable PR_AVP_ProductFinish_DeleteByPK(string conn, int FID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_AVP_ProductFinish_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "FinishID", SqlDbType.Int, FID);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }

            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        //ProductType Table

        #region dbo.PR_AVP_ProductType_SelectAll
        public DataTable PR_AVP_ProductType_SelectAll(string conn)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_AVP_ProductType_SelectAll");

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region PR_AVP_ProductType_SelectByPK

        public DataTable PR_AVP_ProductType_SelectByPK(string conn, int? PTID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_AVP_ProductType_SelectByPK");
                sqlDB.AddInParameter(dbCMD, "ProductTypeID", SqlDbType.Int, PTID);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }

            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region PR_AVP_ProductType_DeleteByPK

        public DataTable PR_AVP_ProductType_DeleteByPK(string conn, int PTID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_AVP_ProductType_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "ProductTypeID", SqlDbType.Int, PTID);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }

            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        //Oredr Table

        #region dbo.PR_AVP_Order_SelectAll
        public DataTable PR_AVP_Order_SelectAll(string conn)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_AVP_Order_SelectAll");

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region PR_AVP_Order_SelectByPK

        public DataTable PR_AVP_Order_SelectByPK(string conn, int? OID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_AVP_Order_SelectByPK");
                sqlDB.AddInParameter(dbCMD, "OrderID", SqlDbType.Int, OID);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }

            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region PR_AVP_Order_DeleteByPK

        public DataTable PR_AVP_Order_DeleteByPK(string conn, int OID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_AVP_Order_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "OrderID", SqlDbType.Int, OID);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }

            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        public DataTable PR_AVP_Products_SelectForDropDown(string conn)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_AVP_Products_SelectForDropDown");

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }

                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable PR_AVP_Products_SelectPriceByPK(string conn, int productID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_AVP_Products_SelectPriceByPK");
                sqlDB.AddInParameter(dbCMD, "@ProductID", SqlDbType.Int, productID);


                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }

                return dt;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //User Table

        #region dbo.PR_AVP_User_SelectAll
        public DataTable PR_AVP_User_SelectAll(string conn)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_AVP_User_SelectAll");

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        //Feedback Table

        #region dbo.PR_AVP_Feedback_SelectAll
        public DataTable PR_AVP_Feedback_SelectAll(string conn)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_AVP_Feedback_SelectAll");

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region PR_AVP_Feedback_SelectByPK

        public DataTable PR_AVP_Feedback_SelectByPK(string conn, int? FDID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_AVP_Feedback_SelectByPK");
                sqlDB.AddInParameter(dbCMD, "FeedbackID", SqlDbType.Int, FDID);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }

            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region PR_AVP_Feedback_DeleteByPK

        public DataTable PR_AVP_Feedback_DeleteByPK(string conn, int FDID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_AVP_Feedback_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "FeedbackID", SqlDbType.Int, FDID);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }

            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        //Cart Table

        #region dbo.PR_AVP_Cart_SelectAll
        public DataTable PR_AVP_Cart_SelectAll(string conn)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_AVP_Cart_SelectAll");

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region PR_AVP_Cart_SelectByPK

        public DataTable PR_AVP_Cart_SelectByPK(string conn, int? CID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_AVP_Cart_SelectByPK");
                sqlDB.AddInParameter(dbCMD, "CartID", SqlDbType.Int, CID);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }

            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region PR_AVP_Cart_DeleteByPK

        public DataTable PR_AVP_Cart_DeleteByPK(string conn, int CID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_AVP_Cart_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "CartID", SqlDbType.Int, CID);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }

            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        //Order Items Table

        #region dbo.PR_AVP_OrderItems_SelectAll
        public DataTable PR_AVP_OrderItems_SelectAll(string conn)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_AVP_OrderItems_SelectAll");

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region PR_AVP_OrderItems_SelectByPK

        public DataTable PR_AVP_OrderItems_SelectByPK(string conn, int? OIID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_AVP_OrderItems_SelectByPK");
                sqlDB.AddInParameter(dbCMD, "OrderItemID", SqlDbType.Int, OIID);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }

            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region PR_AVP_OrderItems_SelectByUserID

        public DataTable PR_AVP_OrderItems_SelectByUserID(string conn, int? UID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_AVP_OrderItems_SelectByUserID");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UID);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }

            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region PR_AVP_OrderItems_DeleteByPK

        public DataTable PR_AVP_OrderItems_DeleteByPK(string conn, int OIID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_AVP_OrderItems_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "OrderItemID", SqlDbType.Int, OIID);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }

            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion
    }
}
