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


            int expirationTimeMin = 0;

            if (!int.TryParse(new DAL.Settings().GetSetting("SecurityToken_ExpirationTimeMIN").value, out expirationTimeMin))
            {
                expirationTimeMin = 20; // default 20 minutes
            }

            var expirationdatetime = DateTime.Now;
            expirationdatetime = expirationdatetime.AddMinutes(expirationTimeMin);

            try
            {

                using (SqlConnection connection = new SqlConnection(DAL.Connection.connectionstring))
                {
                    using (SqlCommand cmd = new SqlCommand("Security_Login") { CommandType = System.Data.CommandType.StoredProcedure, Connection = connection })
                    {

                        cmd.Parameters.Add(new SqlParameter() { ParameterName = "@username", Value = username });
                        cmd.Parameters.Add(new SqlParameter() { ParameterName = "@password", Value = password });
                        cmd.Parameters.Add(new SqlParameter() { ParameterName = "@token", Value = token });
                        cmd.Parameters.Add(new SqlParameter() { ParameterName = "@expirationdate", Value = expirationdatetime });

                        connection.Open();
                        var dbr = cmd.ExecuteScalar();
                       if ( dbr != null)
                       {
                           if (!int.TryParse(dbr.ToString(), out r))
                           {
                               r = -1;
                           }



                       }


                    }
                }
            }
            catch (Exception e)
            {
                new DAL.Logger().Log("Dal.Security", string.Format("(Login) - could not log in with username {0} ", username));
                return -1;
                //  do some errorlogic, this went terrible wrong
            }



            return r;

        }

        public void RemoveOldTokens()
        {

            int expirationTimeMin = 0;
            if (!int.TryParse(new DAL.Settings().GetSetting("SecurityToken_ExpirationTimeMIN").value, out expirationTimeMin))
            {
                expirationTimeMin = 20; // default 20 minutes
            }


            var expirationdatetime = DateTime.Now;
            expirationdatetime = expirationdatetime.AddMinutes(-1 * expirationTimeMin);

            try
            {

                using (SqlConnection connection = new SqlConnection(DAL.Connection.connectionstring))
                {
                    using (SqlCommand cmd = new SqlCommand("Security_RemoveOldTokens") { CommandType = System.Data.CommandType.StoredProcedure, Connection = connection })
                    {

                        cmd.Parameters.Add(new SqlParameter() { ParameterName = "@MinimalTokenDate", Value = expirationdatetime });

                        connection.Open();
                        var removedtokens = (int)cmd.ExecuteScalar();


                    }
                }
            }
            catch (Exception e)
            {
                new DAL.Logger().Log("Dal.Security", string.Format("(RemoveOldTokens) - could not remove the old tokens."));
                //  do some errorlogic, this went terrible wrong
            }


        }
        public BO.AuthenticationInfo Authenticate(string token)
        {
            // get the loginid if existing


            try
            {

                using (SqlConnection connection = new SqlConnection(DAL.Connection.connectionstring))
                {
                    using (SqlCommand cmd = new SqlCommand("Security_Authenticate") { CommandType = System.Data.CommandType.StoredProcedure, Connection = connection })
                    {
                        cmd.Parameters.Add(new SqlParameter() { ParameterName = "@token", Value = token });


                        //customerid, loginid

                        connection.Open();
                        int loginid = 0; //(Int32)cmd.ExecuteScalar();
                        int customerid = 0;
                      
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                loginid = (((int)reader["loginid"]) > 0) ? (int)reader["loginid"] : -1;
                                customerid = (((int)reader["customerid"]) > 0) ? (int)reader["customerid"] : -1;
                           
                            }
                        }

              
                        return new BO.AuthenticationInfo()
                        {
                            customerid = customerid,
                            loginid = loginid
                        };


                    }
                }
            }
            catch (Exception e)
            {

                // todo: do some errorlogic, this went terrible wrong
                new DAL.Logger().Log("Dal.Security", string.Format("(Authenticate) - could not authenticate in with token {0} ", token));
                return null;
            }


        }

    }



}