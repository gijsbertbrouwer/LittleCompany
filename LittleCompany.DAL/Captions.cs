using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LittleCompany.DAL
{
    public class Captions
    {
        //Captions_GetAllByLanguageCode

        private Dictionary<string, BO.Caption> captions;

        public BO.Caption GetCaption(string code, string languagecode)
        {
            if (captions == null)
            {
                // get teh captions (first time)
                captions = Get_Captions(languagecode);
            }


            if (captions.ContainsKey(code))
            {
                return captions[code];
            }
            else
            {
                // caption could not be found
                //log error, missing caption
                new DAL.Logger().Log("Dal.Captions", string.Format("(GetCaption) - The caption: {0} was not found in language {1}", code, languagecode));
                
                return null;
            }


        }


        private Dictionary<string, BO.Caption> Get_Captions(string languagecode)
        {
            var r = new Dictionary<string, BO.Caption>();

            try
            {
                using (SqlConnection connection = new SqlConnection(DAL.Connection.connectionstring))
                {
                    using (SqlCommand cmd = new SqlCommand("Captions_GetAllByLanguageCode") { CommandType = System.Data.CommandType.StoredProcedure, Connection = connection })
                    {
                        cmd.Parameters.Add(new SqlParameter() { ParameterName = "@languagecode", Value = languagecode });


                        connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {

                                string code = (string)reader["code"];

                                r.Add(code, new BO.Caption()
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