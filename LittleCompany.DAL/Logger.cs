using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LittleCompany.DAL
{
    public class Logger
    {

        public void Log(string code, string message)
        {
            // get the setting from the database

            try
            {
                using (SqlConnection connection = new SqlConnection(DAL.Connection.connectionstring))
                {
                    using (SqlCommand cmd = new SqlCommand("Logdata_Create") { CommandType = System.Data.CommandType.StoredProcedure, Connection = connection })
                    {

                        cmd.Parameters.Add(new SqlParameter() { ParameterName = "@code", Value = code });
                        cmd.Parameters.Add(new SqlParameter() { ParameterName = "@message", Value = message });

                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                return ;
                // todo: save error on local machine
            }
            return;
        }

    }
}