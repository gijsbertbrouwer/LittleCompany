using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LittleCompany.DAL
{
    public class Settings
    {

        public BO.Setting GetSetting(string parameter)
        {
            // get the setting from the database

            try
            {
                using (SqlConnection connection = new SqlConnection(DAL.Connection.connectionstring))
                {
                    using (SqlCommand cmd = new SqlCommand("Settings_GetSetting") { CommandType = System.Data.CommandType.StoredProcedure, Connection = connection })
                    {

                        cmd.Parameters.Add(new SqlParameter() { ParameterName = "@parameter", Value = parameter });

                        connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                return new BO.Setting()
                                {

                                    id = (((int)reader["id"]) > 0) ? (int)reader["id"] : -1,
                                    parameter = (string)reader["Parameter"],
                                    value = (string)reader["Value"]
                                };
                            }
                        }

                        reader.Close();


                    }
                }
            }
            catch (Exception e)
            {

                //  do some errorlogic, this went terrible wrong
                new DAL.Logger().Log("Dal.Settings", string.Format("(GetSetting) - crash on finding setting with parameter {0} ", parameter));
                return null;
            }

            //  errorlogic, the setting was not found
            new DAL.Logger().Log("Dal.Settings", string.Format("(GetSetting) - could not find the setting with parameter {0} ", parameter));
            return null;

        }
    }
}