using BDConnect;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcces
{
    public class OrderDAO : Connection
    {
        public DataTable GetSalesOrders(DateTime fromdate, DateTime todate)
        {
            DataTable dataTable = new DataTable();
            using(SqlConnection con = GetSqlConnection())
            {
                con.Open();
                using(SqlCommand scmd = new SqlCommand())
                {
                    scmd.Connection = con;
                    scmd.CommandText = "FechaVentas";
                    scmd.CommandType = CommandType.StoredProcedure;
                    scmd.Parameters.AddWithValue("@fromdate", fromdate);
                    scmd.Parameters.AddWithValue("@todate", todate);
                    SqlDataReader sdr = scmd.ExecuteReader();
                    dataTable.Load(sdr);
                    return dataTable;
                }
            }
        }
    }
}
