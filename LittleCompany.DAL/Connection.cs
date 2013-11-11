using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace LittleCompany.DAL
{




    public static class Connection
    {
        public static string connectionstring { get; set; }

        public static bool databaseIsAccesible()
        {
            try
            {
                using (System.Data.SqlClient.SqlConnection conn = new SqlConnection(connectionstring))
                {
                    conn.Open();
                    return conn.State == System.Data.ConnectionState.Open;
                }
            }
            catch (SqlException)
            {
                // There was an error in opening the database so it is must not up.
                return false;
            }

        }

    }

   

}