using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LittleCompany.DAL
{


    public class Security
    {

        public int Login(string username, string password, string token)
        {
            // get the loginid if existing
            var r = 0;

            try
            {

                using (SqlConnection connection = new SqlConnection(DAL.Connection.connectionstring))
                {
                    using (SqlCommand cmd = new SqlCommand("Security_Login") { CommandType = System.Data.CommandType.StoredProcedure, Connection = connection })
                    {

                        cmd.Parameters.Add(new SqlParameter() { ParameterName = "@username", Value = username });
                        cmd.Parameters.Add(new SqlParameter() { ParameterName = "@password", Value = password });
                        cmd.Parameters.Add(new SqlParameter() { ParameterName = "@token", Value = token });

                        connection.Open();
                        int loginid = (Int32)cmd.ExecuteScalar();
                        r = loginid;
                        return r; // return mloginid

                    }
                }
            }
            catch (Exception e)
            {
                return -1;
                // todo: do some errorlogic, this went terrible wrong
            }

          
        }


        public int Authenticate(string token)
        {
            // get the loginid if existing
            var r = 0;

            try
            {

                using (SqlConnection connection = new SqlConnection(DAL.Connection.connectionstring))
                {
                    using (SqlCommand cmd = new SqlCommand("Security_Authenticate") { CommandType = System.Data.CommandType.StoredProcedure, Connection = connection })
                    {
                        cmd.Parameters.Add(new SqlParameter() { ParameterName = "@token", Value = token });

                        connection.Open();
                        int loginid = (Int32)cmd.ExecuteScalar();
                        r = loginid;
                        return r; // return mloginid

                    }
                }
            }
            catch (Exception e)
            {
                return -1;
                // todo: do some errorlogic, this went terrible wrong
            }


        }

    }



}