using System.Data.SqlClient;

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
