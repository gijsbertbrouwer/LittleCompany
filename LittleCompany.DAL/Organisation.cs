using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LittleCompany.DAL
{
    public class Organisation
    {

        public int CreateNewOrganisation(string name)
        {
 
            var r = 0;

            try
            {


                using (SqlConnection connection = new SqlConnection(DAL.Connection.connectionstring))
                {
                    using (SqlCommand cmd = new SqlCommand("Organisation_create") { CommandType = System.Data.CommandType.StoredProcedure, Connection = connection })
                    {

                        cmd.Parameters.Add(new SqlParameter() { ParameterName = "@name", Value = name });

                        connection.Open();
                        //int mainloginid = (Int32)cmd.ExecuteScalar();
                        string respons = cmd.ExecuteScalar().ToString();
                        if(!int.TryParse(respons,out r)){
                            r = -1;
                        }

                     
                        return r; // return mainloginid

                    }
                }
            }
            catch (Exception e)
            {

                // do some errorlogic, this went terrible wrong
                new DAL.Logger().Log("DAL.Organisation", string.Format("(CreateNewOrganisation) - The organisation with name: {0} could not be created", name));
                return -1;
            }


        }


    }
}