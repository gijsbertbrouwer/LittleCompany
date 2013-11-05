using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LittleCompany.DAL
{
    public class Person
    {
    //    CREATE PROCEDURE [dbo].Person_Create
    //@customerid int = 0,
    //@organisationid int = null,
    //@name nvarchar(250)
        public int CreateNewPerson(string name, int organisationid, int customerid)
        {

            var r = 0;

            try
            {


                using (SqlConnection connection = new SqlConnection(DAL.Connection.connectionstring))
                {
                    using (SqlCommand cmd = new SqlCommand("Organisation_create") { CommandType = System.Data.CommandType.StoredProcedure, Connection = connection })
                    {

                        cmd.Parameters.Add(new SqlParameter() { ParameterName = "@customerid", Value = customerid });
                        cmd.Parameters.Add(new SqlParameter() { ParameterName = "@organisationid", Value = organisationid });
                        cmd.Parameters.Add(new SqlParameter() { ParameterName = "@name", Value = name });


                        connection.Open();
                        string respons = cmd.ExecuteScalar().ToString();
                        if (!int.TryParse(respons, out r))
                        {
                            r = -1;
                        }


                        return r; 

                    }
                }
            }
            catch (Exception e)
            {

                // do some errorlogic, this went terrible wrong
                new DAL.Logger().Log("DAL.Person", string.Format("(CreateNewPerson) - The person with name: {0}, organisationid: {1}, customerid {2} could not be created", name, organisationid, customerid));
                return -1;
            }


        }


    }
}