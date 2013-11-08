using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LittleCompany.DAL
{
    public class Captions
    {

        public Dictionary<string, BO.Language> Get_CaptionsAll()
        {

            var r = new Dictionary<string, BO.Language>();
       
            try
            {
                using (SqlConnection connection = new SqlConnection(DAL.Connection.connectionstring))
                {
                    using (SqlCommand cmd = new SqlCommand("Captions_GetAll") { CommandType = System.Data.CommandType.StoredProcedure, Connection = connection })
                    {

                        connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {

                          var currentlanguage = new BO.Language();


                            while (reader.Read())
                            {

                                string code = (string)reader["code"];
                                string lcode = (string)reader["languagecode"];

                                if(currentlanguage.languagecode != lcode){
                                    currentlanguage = new BO.Language()
                                    {
                                        languagecode = lcode,
                                        captions = new Dictionary<string,BO.Caption>()
                                    };
                                    r.Add(lcode, currentlanguage);
                                }

                                currentlanguage.captions.Add(code, new BO.Caption()
                                {
                                    id = (int)reader["id"],
                                    caption = (string)reader["caption"],
                                    code = code
                                });
                            }
                        }
                        else
                        {
                            // todo: error this, no result means a broken link..
                            return null;

                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception)
            {
                return null;
                // todo: do some errorlogic, this went terrible wrong
            }

            return r;
        }
    }
}