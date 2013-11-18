using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LittleCompany.DAL
{
    public class Organisation
    {

        public int CreateNewOrganisation(string name, int customerid)
        {
 
            var r = 0;

            try
            {


                using (SqlConnection connection = new SqlConnection(DAL.Connection.connectionstring))
                {
                    using (SqlCommand cmd = new SqlCommand("Organisation_create") { CommandType = System.Data.CommandType.StoredProcedure, Connection = connection })
                    {

                        cmd.Parameters.Add(new SqlParameter() { ParameterName = "@name", Value = name });
                        cmd.Parameters.Add(new SqlParameter() { ParameterName = "@customerid", Value = customerid });


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

        public BO.Organisation GetOrganisation(int organisationid, int customerid)
        {
            var r = new BO.Organisation();

            try
            {


                using (SqlConnection connection = new SqlConnection(DAL.Connection.connectionstring))
                {
                    using (SqlCommand cmd = new SqlCommand("Organisation_Get") { CommandType = System.Data.CommandType.StoredProcedure, Connection = connection })
                    {


                        cmd.Parameters.Add(new SqlParameter() { ParameterName = "@organisationid", Value = organisationid });
                        cmd.Parameters.Add(new SqlParameter() { ParameterName = "@customerid", Value = customerid });


                        connection.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {

                            while (dr.Read())
                            {
                                //id, name, organisationid, personid, customerid, [path], dateupload, [version], [guid], [password]
                                r.name = (string)dr["name"];
                                if (dr["id"] != System.DBNull.Value){r.id = (int)dr["id"]; }
                                if (dr["Emailaddress"] != System.DBNull.Value) { r.emailaddress = (string)dr["Emailaddress"]; }
                                if (dr["name"] != System.DBNull.Value) { r.name = (string)dr["name"]; }
                                if (dr["Phonenumber"] != System.DBNull.Value) { r.phonenumber = (string)dr["Phonenumber"]; }
                                if (dr["Notes"] != System.DBNull.Value) { r.notes = (string)dr["Notes"]; }
                            }
                        }



                    }
                }

                return r;

            }
            catch (Exception e)
            {
                // do some errorlogic, this went terrible wrong
                new DAL.Logger().Log("DAL.Organisation", string.Format("(GetOrganisation) - The organisation with id: {0}, customerid {1} could not be found.", organisationid, customerid));
                return null;
            }



        }

    }
}