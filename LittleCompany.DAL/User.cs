using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LittleCompany.DAL
{
    public class User
    {


        #region Public
        public BO.User GetUser(int loginid, int customerid)
        {
            var r = new BO.User() { favorites = new List<BO.User.Favorite>() };


            try
            {
                using (SqlConnection connection = new SqlConnection(DAL.Connection.connectionstring))
                {
                    // Get user info
                    var userinfo = GetUserData(connection, loginid, customerid);
                    if (userinfo != null && userinfo.id > 0)
                    {
                        r = userinfo;


                        // Get favorites
                        var favorites = GetUserFavorites(connection, r.id, customerid);
                        if (favorites != null && favorites.Count > 0)
                        {
                            r.favorites = favorites;
                        }


                    }
                    else
                    {
                        new DAL.Logger().Log("Dal.User", string.Format("(GetUser) - could not find user with loginid {0} and customerid {1}", loginid, customerid));
                        connection.Close();
                        return null;
                    }

                    

                }
            }
            catch (Exception e)
            {
                //  do some errorlogic, this went terrible wrong
                new DAL.Logger().Log("Dal.User", string.Format("(GetUser) - crash on finding user with loginid {0}  and customerid {1}", loginid, customerid));
                return null;
            }


            return r;
        }

        public BO.User.Favorite SetFavorite(BO.User.Favorite favorite, int userid, int customerid)
        {
            // : add favorite to db
            try
            {
                using (SqlConnection connection = new SqlConnection(DAL.Connection.connectionstring))
                {

                    using (SqlCommand cmd = new SqlCommand("Favorites_Set") { CommandType = System.Data.CommandType.StoredProcedure, Connection = connection })
                    {

                        cmd.Parameters.Add(new SqlParameter() { ParameterName = "@userid", Value = userid });
                        cmd.Parameters.Add(new SqlParameter() { ParameterName = "@customerid", Value = customerid });
                        cmd.Parameters.Add(new SqlParameter() { ParameterName = "@datatypeid", Value = favorite.datatypeid });
                        cmd.Parameters.Add(new SqlParameter() { ParameterName = "@name", Value = favorite.name });

                        connection.Open();
                        int id = 0;
                        
                        int.TryParse(cmd.ExecuteScalar().ToString(), out id);

                        if (id < 1)
                        {
                            // something went wrong.
                            connection.Close();
                            new DAL.Logger().Log("Dal.User", string.Format("(SetFavorite) - problem (no id back) on setting favorite with userid {0}  and customerid {1} datatype {2} and name {3}", userid, customerid, favorite.datatypeid, favorite.name));
                            return null;
                        }
                        else
                        {
                            favorite.id = id;
                        }

                        connection.Close();

                    }

                }
            }
            catch (Exception e)
            {
                //  do some errorlogic, this went terrible wrong
                new DAL.Logger().Log("Dal.User", string.Format("(SetFavorite) - crash on setting favorite with userid {0}  and customerid {1} datatype {2} and name {3}", userid, customerid, favorite.datatypeid, favorite.name));
                return null;
            }



            return favorite;
        }

        public void DelFavorite(int favoriteid, int userid, int customerid)
        {
            // delete favorite to db

            try
            {
                using (SqlConnection connection = new SqlConnection(DAL.Connection.connectionstring))
                {

                    using (SqlCommand cmd = new SqlCommand("Favorites_DelForUser") { CommandType = System.Data.CommandType.StoredProcedure, Connection = connection })
                    {

                        cmd.Parameters.Add(new SqlParameter() { ParameterName = "@userid", Value = userid });
                        cmd.Parameters.Add(new SqlParameter() { ParameterName = "@customerid", Value = customerid });
                        cmd.Parameters.Add(new SqlParameter() { ParameterName = "@favoriteid", Value = favoriteid });

                        connection.Open();
                        cmd.ExecuteScalar();
                        connection.Close();

                    }

                }
            }
            catch (Exception e)
            {
                //  do some errorlogic, this went terrible wrong
                new DAL.Logger().Log("Dal.User", string.Format("(DelFavorite) - crash on removing favorite with userid {0}  and customerid {1} favoriteid {2}", userid, customerid, favoriteid));

            }


            return;
        }
        #endregion

        #region GET
        private BO.User GetUserData(SqlConnection context, int loginid, int customerid)
        {
            var r = new BO.User() { favorites = new List<BO.User.Favorite>() };

            // get the user data

            using (SqlCommand cmd = new SqlCommand("User_Get") { CommandType = System.Data.CommandType.StoredProcedure, Connection = context })
            {

                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@loginid", Value = loginid });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@customerid", Value = customerid });

                if (context.State == System.Data.ConnectionState.Closed)
                {
                    context.Open();
                }
              
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        r.id = (((int)reader["id"]) > 0) ? (int)reader["id"] : -1;
                        r.name = ((reader["FirstName"] != DBNull.Value) ? (string)reader["FirstName"] : "");
                        r.creationdate = (DateTime)reader["CreationDate"];
                    }
                }

                reader.Close();





                return r;


            }
        }

        private List<BO.User.Favorite> GetUserFavorites(SqlConnection context, int userid, int customerid)
        {
            var r = new List<BO.User.Favorite>();

            // get the favorites

            using (SqlCommand cmd = new SqlCommand("Favorites_GetForUser") { CommandType = System.Data.CommandType.StoredProcedure, Connection = context })
            {

                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@userid", Value = userid });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@customerid", Value = customerid });

                if (context.State == System.Data.ConnectionState.Closed)
                {
                    context.Open();
                }
          
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        r.Add(new BO.User.Favorite()
                        {
                            id = (((int)reader["id"]) > 0) ? (int)reader["id"] : -1,
                            name = (string)reader["Name"],
                            datatypeid = (((int)reader["DataTypeId"]) > 0) ? (int)reader["DataTypeId"] : -1,
                            datatypecaption = (string)reader["DataTypeCaption"]
                        });
                    }
                }

                reader.Close();




                return r;
            }


        }
        #endregion
    }
}