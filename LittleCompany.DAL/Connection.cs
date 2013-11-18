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

                SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder(connectionstring);
                csb.ConnectTimeout = 5;
                string newConnectionString = csb.ToString();


                using (System.Data.SqlClient.SqlConnection conn = new SqlConnection(newConnectionString))
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