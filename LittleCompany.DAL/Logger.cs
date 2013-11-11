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
              
                // save error on local machine
                LogOnFileSystem(code, message, e.Message);
                return;
            }
            return;
        }



        private void LogOnFileSystem(string code, string message, string errormessage)
        {


            string messageToStore = DateTime.Now.ToString("yyyy-MM-dd") + "\t" + code.ToUpper() + "\t" + message;

    
                // log this error.
                var logmessage = messageToStore;
                string filepath = HttpContext.Current.Server.MapPath("~");
               
                filepath = System.IO.Path.Combine(filepath, "Log");
               
                // Create path if missing
                new DAL.Files().CreatePathIfMising(filepath);

                filepath = System.IO.Path.Combine(filepath,string.Format(@"log_{0}.txt", DateTime.Now.Date.ToString("yyyyMMdd")));

                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(filepath, true))
                {
                    sw.WriteLine("--------------------------------ERROR REACHING DB, SAVING TO FILE INSTEAD--------------------------------------");
                    sw.WriteLine(errormessage);
                    sw.WriteLine(messageToStore);
                   
                }
            
        }
    

    }
}