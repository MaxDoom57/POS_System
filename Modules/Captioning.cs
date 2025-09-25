using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS_System.Classes;

namespace POS_System.Modules
{
    public class Captioning
    {
        public static string CaptionMe(int companyKey, string prntNm, string colNm, string contCd = null, string ourCd = null)
        {
            string caption = "MissingCap";

            using(SqlConnection conn = DBConnectionManager.GetConnection())
            using(SqlCommand cmd = new SqlCommand("sprGetColCap", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 60;

                cmd.Parameters.AddWithValue("@pCKy", companyKey);
                cmd.Parameters.AddWithValue("@pPrntObjNm", prntNm);
                cmd.Parameters.AddWithValue("@pObjNm", colNm);

                SqlParameter shortCapParam = new SqlParameter("@pShortCap", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(shortCapParam);

                SqlParameter objCapParam = new SqlParameter("@pObjCap", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(objCapParam);

                conn.Open();
                cmd.ExecuteNonQuery();
                object result = shortCapParam.Value;
                if(result != DBNull.Value && result != null)
                {
                    caption = result.ToString();
                }
            }

            return caption;
        }
    }
}
