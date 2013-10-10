using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LittleCompany.DAL
{
    public class Customer
    {

        public int CreateNewCustomer(string custmername, string mainusername, string mainuserpassword)
        {
            // check if customername already exist (return if so)
            // check if mainloginemail already exist (return is so)

            
            if (!Check_RegistrationPossible( custmername,  mainusername)) { return -1; }

            var mainloginid =    Create_newCustomer(custmername,mainusername,mainuserpassword);

            return mainloginid; ;
        }

        private int Create_newCustomer(string custmername, string mainusername, string mainuserpassword)
        {
            var r = 0;

            try
            {


                using (SqlConnection connection = new SqlConnection(DAL.Connection.connectionstring))
                {
                    using (SqlCommand cmd = new SqlCommand("RegisterCustomer_CreateCustomer") { CommandType = System.Data.CommandType.StoredProcedure, Connection = connection })
                    {

                        cmd.Parameters.Add(new SqlParameter(){ ParameterName = "@Customername", Value =custmername});
                        cmd.Parameters.Add(new SqlParameter() { ParameterName = "@password", Value = mainuserpassword });
                        cmd.Parameters.Add(new SqlParameter() { ParameterName = "@mainloginusername", Value = mainusername });

                        connection.Open();
                        int mainloginid = (Int32)cmd.ExecuteScalar();
                        r = mainloginid;
                        return r; // return mainloginid
                    
                    }
                }
            }
            catch (Exception e)
            {
                return -1;
                // todo: do some errorlogic, this went terrible wrong
            }

            return r;
        }

        private bool Check_RegistrationPossible(string custmername, string mainusername)
        {
            var r = false;

            try
            {


                using (SqlConnection connection = new SqlConnection(DAL.Connection.connectionstring))
                {
                    using (SqlCommand cmd = new SqlCommand("RegisterCustomer_CheckPossible") { CommandType = System.Data.CommandType.StoredProcedure, Connection = connection })
                    {
                        cmd.Parameters.Add(new SqlParameter() { ParameterName = "@customername", Value = custmername });
                        cmd.Parameters.Add(new SqlParameter() { ParameterName = "@mainloginemail", Value = mainusername });

                        connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                bool customerexist = (((int)reader["customerexist"]) > 0) ? true : false; 
                                bool mainloginexist = (((int)reader["loginexist"]) > 0) ? true : false;

                                if (!customerexist &&  !mainloginexist){r = true;}

                            }
                        }
                        else
                        {
                            // todo: error this, no result means a broken link..
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception)
            {
                return false;
                // todo: do some errorlogic, this went terrible wrong
            }

            return r;
        }



    }
}