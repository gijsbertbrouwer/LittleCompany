using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LittleCompany.DAL
{
    public class Search
    {

        public BO.Search.Quick Search_Quick(BO.Search.Quick  search)
        {
            search.searchresults = new List<BO.Search.Quick.item>();
            try
            {


                using (SqlConnection connection = new SqlConnection(DAL.Connection.connectionstring))
                {
                    using (SqlCommand cmd = new SqlCommand("Search_QuickAccess_Main") { CommandType = System.Data.CommandType.StoredProcedure, Connection = connection })
                    {
                        cmd.Parameters.Add(new SqlParameter() { ParameterName = "@query", Value = search.query });
                        cmd.Parameters.Add(new SqlParameter() { ParameterName = "@customerid", Value = search.customerid });
                        cmd.Parameters.Add(new SqlParameter() { ParameterName = "@datatypeid", Value = search.searchDataTypeId });

                        connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                             
                               search.searchresults.Add(new BO.Search.Quick.item()
                                {
                                     id = (int)reader["id"],
                                     datatypeid = (int)reader["datatypeid"],
                                     name = reader["name"].ToString()
                                });

                            }
                        }

                        reader.Close();
                    }
                }
            }
            catch (Exception)
            {
                new DAL.Logger().Log("DAL.Search", string.Format("(Search_Quick) - The quicksearch with query: {0}, customerid{1}, datatypeid{2} could not be executed.", search.query, search.customerid, search.searchDataTypeId));
                return null;
               
            }


            return search;
        }


    }
}