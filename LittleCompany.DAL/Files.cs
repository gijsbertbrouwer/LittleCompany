using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.IO;

namespace LittleCompany.DAL
{
    public class Files
    {




        public BO.File CreateNewFile(System.IO.Stream inputstream, string name, int organisationid, int customerid, int personid, DateTime dateupload)
        {
            string customerpath = string.Format("Customer_{0}\\Uploads", customerid);


            var r = new BO.File()
            {
                name = name,
                organisationid = organisationid,
                personid = personid,
                versions = new List<BO.File.Version>()
            };

            // save in db (get back id and guid)
            r = CreateNewFile_DB(r, customerid, customerpath, dateupload);
            if (r == null) { return null; }

            // save in filesystem (with path and guid)
            if (!WriteFileToFileSystem(r.versions.FirstOrDefault().guid, customerpath, inputstream))
            {
                // there was an error saving the file.
                // todo: delete from db

                return null;

            }



            return r;

        }


        private BO.File CreateNewFile_DB(BO.File file, int customerid, string path, DateTime dateupload)
        {


            try
            {


                using (SqlConnection connection = new SqlConnection(DAL.Connection.connectionstring))
                {
                    using (SqlCommand cmd = new SqlCommand("File_Create") { CommandType = System.Data.CommandType.StoredProcedure, Connection = connection })
                    {

                        cmd.Parameters.Add(new SqlParameter() { ParameterName = "@name", Value = file.name });
                        if (file.organisationid > 0) { cmd.Parameters.Add(new SqlParameter() { ParameterName = "@organisationid", Value = file.organisationid }); }
                        if (file.personid > 0) { cmd.Parameters.Add(new SqlParameter() { ParameterName = "@personid", Value = file.personid }); }
                        cmd.Parameters.Add(new SqlParameter() { ParameterName = "@customerid", Value = customerid });
                        cmd.Parameters.Add(new SqlParameter() { ParameterName = "@path", Value = path });
                        cmd.Parameters.Add(new SqlParameter() { ParameterName = "@dateUpload", Value = dateupload });


                        connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {

                                file.versions.Add(new BO.File.Version()
                                {
                                    dateuploaded = dateupload,
                                    guid = ((System.Guid)reader["guid"]).ToString(),
                                    version = 1,
                                    id = (int)reader["id"]
                                });

                            }
                        }



                    }
                }

                return file;

            }
            catch (Exception e)
            {

                // do some errorlogic, this went terrible wrong
                new DAL.Logger().Log("DAL.Files", string.Format("(CreateNewFile_DB) - The file with name: {0}, organisationid: {1}, customerid {2} could not be created on path {3}", file.name, file.organisationid, customerid, path));
                return null;
            }


        }

        public bool WriteFileToFileSystem(string fileguid, string customerpath, System.IO.Stream inputstream)
        {
            string OndorUploadPath = new DAL.Settings().GetSetting("OndorFilePath").value;
            string filepath = System.IO.Path.Combine(OndorUploadPath, customerpath).ToString();

            // create path if possible
            if (!CreatePathIfMising(filepath)) { return false; }

            try
            {

                filepath = System.IO.Path.Combine(filepath,fileguid + ".ondor");

                using (var fileStream = File.Create(filepath))
                {
                    inputstream.CopyTo(fileStream);
                }
            }
            catch (IOException e)
            {
                new DAL.Logger().Log("DAL.Files", string.Format("(WriteFileToFileSystem) - The file could not be stored on path {0} with message {1}", filepath.ToString(), e.Message));
                return false;

            }


            return true;


        }

        private bool CreatePathIfMising(string path)
        {

            try
            {
                if (!Directory.Exists(path))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(path);
                }

            }
            catch (IOException ioex)
            {
                Console.WriteLine(ioex.Message);
                new DAL.Logger().Log("DAL.Files", string.Format("(CreatePathIfMissing) - the following path could not be created {0}, with the following message {1}", path, ioex.Message));
                return false;
            }

            return true;

        }


    }
}