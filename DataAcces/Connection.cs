using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDConnect
{
    public class Connection
    {
        protected SqlConnection GetSqlConnection()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Server=(local); DataBase=BikeStore; Integrated Security=true;";
            return con;
        }
    }
}
