using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.SqlServer.Server;
using POS_System.Classes;

namespace POS_System.Modules
{
    public class DatabaseFunctions
    {
        //for get the Transaction No
        public static int GetTrnNoList(int companyKey, int pCdKy, DateTime trnDate, string ourCode)
        {
            using(SqlConnection conn = DBConnectionManager.GetConnection())
            {
                conn.Open();
                string query = "Select LstTrnNo from TrnNoLst Where OurCd = @OurCd And fInAct = 0 And CKy = @companyKey";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@OurCd", ourCode);
                cmd.Parameters.AddWithValue("@companyKey", companyKey);
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    if(reader.HasRows)
                    {
                        reader.Read();
                        int result = Convert.ToInt32(reader["LstTrnNo"]);
                        reader.Close();
                        return result;
                    }
                    else
                    {
                        reader.Close ();
                        SqlCommand cmdInsert = new SqlCommand("Insert into TrnNoLst (OurCd,LstTrnNo,Cky,CdKy) Values(@OurCd, '0', @companyKey, @pCdKy)", conn);
                        cmdInsert.Parameters.AddWithValue("@OurCd", ourCode);
                        cmdInsert.Parameters.AddWithValue("@companyKey", companyKey);
                        cmdInsert.Parameters.AddWithValue("@pCdKy", pCdKy);
                        cmdInsert.ExecuteNonQuery();

                        return 0;
                    }
                }
            }
        }

        public static bool isValidTrnDate(int companyKey, string TrnType, DateTime pDt, SqlConnection conn)
        {
            int mDays = 0;
            int mFwdDays = 0;
            int HDays;
            DateTime mDt, svrDt;
            bool isValidTrnDate = true;

            svrDt = GetSvrDate(conn);
            if(conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand("Select CdNo4,CdNo5 From CdMas Where ConCd = 'TrnTyp' And OurCd =@strTrnTyp And fInAct = 0", conn);
            cmd.Parameters.AddWithValue("@strTrnTyp", TrnType);
            SqlDataReader reader = cmd.ExecuteReader();
            if(reader.Read())
            {
                mDays = Convert.ToInt32(reader["CdNo4"]);
                mFwdDays = Convert.ToInt32(reader["CdNo5"]);
            }
            reader.Close();
            cmd.Dispose();

            HDays = GetNoOfHolidays(1, pDt, svrDt, conn);
            mDays = mDays + HDays;
            HDays = GetNoOfHolidays(1, svrDt, pDt, conn);
            mFwdDays = mFwdDays + HDays;

            mDt = GetTrnCnfDt(companyKey, conn);
            if(pDt <= mDt)
            {
                isValidTrnDate = false;
            }
            else if((svrDt - pDt).Days > mDays)
            {
                isValidTrnDate = false;
            }

            reader.Close();
            cmd.Dispose();
            return isValidTrnDate;
        }

        public static DateTime GetSvrDate(SqlConnection conn)
        {
            DateTime DT;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            SqlCommand cmd = new SqlCommand("sprGetSvrDate", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 120;

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                DT = Convert.ToDateTime(reader[0]);
            }
            else
            {
                throw new Exception("sprGetSvrDate returned no result.");
            }

            reader.Close();
            cmd.Dispose();
            return DT;

        }

        public static int GetNoOfHolidays(int companyKey, DateTime pFrmDt, DateTime pToDt, SqlConnection conn)
        {
            int noOfHolidays = 0;

            SqlCommand cmd = new SqlCommand("sprCalendarNoOfHolidaysPFrmDtToDt", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlCommandBuilder.DeriveParameters(cmd);
            cmd.Parameters["@pCKy"].Value = companyKey;
            cmd.Parameters["@pFrmDt"].Value = pFrmDt.ToString("MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            cmd.Parameters["@pToDt"].Value = pToDt.ToString("MM/dd/yyyy",System.Globalization.CultureInfo.InvariantCulture);

            cmd.CommandTimeout = 60;

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    noOfHolidays = Convert.ToInt32(reader[0]);
                }
            }

            return noOfHolidays;
        }

        public static DateTime GetTrnCnfDt(int companyKey, SqlConnection conn)
        {
            DateTime svrDt = GetSvrDate(conn);
            DateTime getTrnCnfDt;

            using (SqlCommand cmd = new SqlCommand("Select TrnCnfDt from Company where CKy = @companyKey", conn))
            {
                cmd.Parameters.AddWithValue("@companyKey", companyKey);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        if (reader["TrnCnfDt"] != DBNull.Value)
                        {
                            return Convert.ToDateTime(reader["TrnCnfDt"]);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Company transactions setup or Company identification error", "Data Retrieval");
                        return svrDt;
                    }
                }
            }

            // If TrnCnfDt was null, calculate and update it
            getTrnCnfDt = new DateTime(svrDt.Year, svrDt.Month, 1);

            using (SqlCommand cmdUpdate = new SqlCommand("UPDATE Company SET TrnCnfDt = @TrnCnfDt WHERE CKy = @companyKey", conn))
            {
                cmdUpdate.Parameters.AddWithValue("@TrnCnfDt", getTrnCnfDt);
                cmdUpdate.Parameters.AddWithValue("@companyKey", companyKey);
                cmdUpdate.ExecuteNonQuery();
            }

            return getTrnCnfDt;
        }

        public static int GetTrnTypKy(int companyKey, string ourCode)
        {
            int trnTypeKy = 0;
            string query = "Select TrnTypKy from vewTrnTypCd where TrnTyp = @ourCode And CKy = @companyKey";
            using(SqlConnection conn = DBConnectionManager.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ourCode", ourCode);
                cmd.Parameters.AddWithValue("@companyKey", companyKey);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    if (reader["TrnTypKy"] == null)
                    {
                        trnTypeKy = 0;
                    }
                    else
                    {
                        trnTypeKy = Convert.ToInt32(reader["TrnTypKy"]);
                    }
                }
                else
                {
                    trnTypeKy = 0;
                }
                reader.Close ();
            }
            return trnTypeKy;
        }

        public static int GetTrnNoLstSave(int companyKey, int cdKy, DateTime trnDt, string ourCode, SqlConnection conn, SqlTransaction tran)
        {
            int nextTrnNo = 0;
            using (SqlCommand cmd = new SqlCommand("SELECT LstTrnNo FROM TrnNoLst WHERE OurCd = @OurCd AND fInAct = 0", conn, tran))
            {
                cmd.Parameters.AddWithValue("@OurCd", ourCode);

                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    int currentTrnNo = Convert.ToInt32(result);
                    nextTrnNo = currentTrnNo + 1;

                    using (SqlCommand cmdUpdate = new SqlCommand(
                        "UPDATE TrnNoLst SET LstTrnNo = @LstTrnNo WHERE OurCd = @OurCd AND fInAct = 0", conn, tran))
                    {
                        cmdUpdate.Parameters.AddWithValue("@LstTrnNo", nextTrnNo);
                        cmdUpdate.Parameters.AddWithValue("@OurCd", ourCode);
                        cmdUpdate.ExecuteNonQuery();
                    }
                }
                else
                {
                    using (SqlCommand cmdMax = new SqlCommand(
                        "SELECT MAX(TrnNo) FROM TrnMas WHERE OurCd = @OurCd AND CKy = @CKy", conn, tran))
                    {
                        cmdMax.Parameters.AddWithValue("@OurCd", ourCode);
                        cmdMax.Parameters.AddWithValue("@CKy", companyKey);

                        object maxResult = cmdMax.ExecuteScalar();
                        if (maxResult == DBNull.Value || maxResult == null)
                        {
                            nextTrnNo = 1;
                        }
                        else
                        {
                            nextTrnNo = Convert.ToInt32(maxResult) + 1;
                        }
                    }

                    using (SqlCommand cmdInsert = new SqlCommand(
                        "INSERT INTO TrnNoLst (OurCd, LstTrnNo, CKy, CdKy) VALUES (@OurCd, @LstTrnNo, @CKy, @CdKy)", conn, tran))
                    {
                        cmdInsert.Parameters.AddWithValue("@OurCd", ourCode);
                        cmdInsert.Parameters.AddWithValue("@LstTrnNo", nextTrnNo);
                        cmdInsert.Parameters.AddWithValue("@CKy", companyKey);
                        cmdInsert.Parameters.AddWithValue("@CdKy", cdKy);
                        cmdInsert.ExecuteNonQuery();
                    }
                }
            }

            return nextTrnNo;
        }

        public static void PostAccTrn(int tranKey, int supplierKey, int lineNo, string description, decimal amount, byte fchq = 0, int anlTyp1Key = 0, int anlTyp2Key = 0, int mtModeKey = 0, SqlConnection conn = null, SqlTransaction tran = null)
        {
            //lineNo -> pass value manually
            int accTranKey = 0;

            if (conn == null)
            {
                conn = DBConnectionManager.GetConnection();
                conn.Open();
            }

            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            string query = "SELECT AccTrnKy FROM AccTrn WHERE TrnKy = @tranKey and LiNo = @rowNo";
            using (SqlCommand cmdInsert = new SqlCommand(query, conn, tran))
            {
                cmdInsert.Parameters.AddWithValue("@tranKey", tranKey);
                cmdInsert.Parameters.AddWithValue("@rowNo", lineNo);
                using (SqlDataReader reader = cmdInsert.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        accTranKey = Convert.ToInt32(reader["AccTrnKy"]);
                    }
                }

                if (accTranKey != 0 && string.IsNullOrWhiteSpace(description))
                {
                    if (string.IsNullOrWhiteSpace(description))
                    {
                        string updateQuery = "UPDATE AccTrn " +
                            "SET PmtModeKy = @mtModeKey, Amt = @amount, AccKy = @supplierKey, AnlTyp1Ky = @anlTyp1Ky, AnlTyp2Ky = @anlTyp2Ky " +
                            "WHERE AccTrnKy = @accTranKey";

                        using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn, tran))
                        {
                            updateCmd.Parameters.AddWithValue("@mtModeKey", mtModeKey);
                            updateCmd.Parameters.AddWithValue("@amount", amount);
                            updateCmd.Parameters.AddWithValue("@supplierKey", supplierKey);
                            updateCmd.Parameters.AddWithValue("@anlTyp1Ky", anlTyp1Key);
                            updateCmd.Parameters.AddWithValue("@anlTyp2Ky", anlTyp2Key);
                            updateCmd.Parameters.AddWithValue("@accTranKey", accTranKey);

                            updateCmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        string updateQuery = @"Update AccTrn 
                                    SET Des = @description, Amt = @amount, AccKy = @supplierKey, PmtModeKy = @mtModeKey, AnlTyp1Ky = @anlTyp1Ky, AnlTyp2Ky = @anlTyp2Ky 
                                    WHERE AccTrnKy = @accTranKey";

                        using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn, tran))
                        {
                            updateCmd.Parameters.AddWithValue("@description", description);
                            updateCmd.Parameters.AddWithValue("@amount", amount);
                            updateCmd.Parameters.AddWithValue("@supplierKey", supplierKey);
                            updateCmd.Parameters.AddWithValue("@mtModeKey", mtModeKey);
                            updateCmd.Parameters.AddWithValue("@anlTyp1Ky", anlTyp1Key);
                            updateCmd.Parameters.AddWithValue("@anlTyp2Ky", anlTyp2Key);
                            updateCmd.Parameters.AddWithValue("@accTranKey", accTranKey);

                            updateCmd.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(description))
                    {
                        string updateQuery = "insert into AccTrn(TrnKy, AccKy, LiNo, Amt, Fchqdet, AnlTyp1Ky, AnlTyp2Ky, PmtModeKy) " +
                                            "values(@tranKey, @supplierKey, @lineNo, @amount, @fchq, @anlTyp1Ky, @anlTyp2Ky, @mtModeKey)";

                        using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn, tran))
                        {
                            updateCmd.Parameters.AddWithValue("@tranKey", tranKey);
                            updateCmd.Parameters.AddWithValue("@supplierKey", supplierKey);
                            updateCmd.Parameters.AddWithValue("@lineNo", lineNo);
                            updateCmd.Parameters.AddWithValue("@amount", amount);
                            updateCmd.Parameters.AddWithValue("@fchq", fchq);
                            updateCmd.Parameters.AddWithValue("@anlTyp1Ky", anlTyp1Key);
                            updateCmd.Parameters.AddWithValue("@anlTyp2Ky", anlTyp2Key);
                            updateCmd.Parameters.AddWithValue("@mtModeKey", mtModeKey);

                            updateCmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        string updateQuery = "insert into AccTrn(TrnKy, AccKy, LiNo, Des, Amt, Fchqdet, AnlTyp1Ky, AnlTyp2Ky, PmtModeKy) " +
                            "values(@tranKey, @supplierKey, @lineNo, @description, @amount, @fchq, @anlTyp1Ky, @anlTyp2Ky, @mtModeKey)";

                        using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn, tran))
                        {
                            updateCmd.Parameters.AddWithValue("@tranKey", tranKey);
                            updateCmd.Parameters.AddWithValue("@supplierKey", supplierKey);
                            updateCmd.Parameters.AddWithValue("@lineNo", lineNo);
                            updateCmd.Parameters.AddWithValue("@description", description);
                            updateCmd.Parameters.AddWithValue("@amount", amount);
                            updateCmd.Parameters.AddWithValue("@fchq", fchq);
                            updateCmd.Parameters.AddWithValue("@anlTyp1Ky", anlTyp1Key);
                            updateCmd.Parameters.AddWithValue("@anlTyp2Ky", anlTyp2Key);
                            updateCmd.Parameters.AddWithValue("@mtModeKey", mtModeKey);

                            updateCmd.ExecuteNonQuery();
                        }
                    }
                }
            }

        }

    }
}
