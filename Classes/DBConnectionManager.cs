using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace POS_System.Classes
{
    public class DBConnectionManager
    {
        public static SqlConnection newConnection;
        public static string connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;

        public static SqlConnection GetConnection()
        {
            newConnection = new SqlConnection(connectionString);
            return newConnection;
        }
    }
}
