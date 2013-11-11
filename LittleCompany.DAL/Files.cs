using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Web.Security;

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


            string secretpassword = ""; //Membership.GeneratePassword(8,0);


            // save in db (get back id and guid)
            r = CreateNewFile_DB(r, customerid, customerpath, dateupload, secretpassword);
            if (r == null) { return null; }

            // save in filesystem (with path and guid)
            if (!WriteFileToFileSystem(r.versions.FirstOrDefault().guid, customerpath, inputstream, secretpassword, r.name))
            {
                // there was an error saving the file.
                // todo: delete from db

                return null;

            }


            return r;

        }


        private BO.File CreateNewFile_DB(BO.File file, int customerid, string path, DateTime dateupload, string secretpassword)
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
                        cmd.Parameters.Add(new SqlParameter() { ParameterName = "@password", Value = secretpassword });

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

        public bool WriteFileToFileSystem(string fileguid, string customerpath, System.IO.Stream inputstream, string secretpassword, string originalname)
        {
            string OndorUploadPath = new DAL.Settings().GetSetting("OndorFilePath").value;
            string filedir = System.IO.Path.Combine(OndorUploadPath, customerpath).ToString();

            // create path if possible
            if (!CreatePathIfMising(filedir)) { return false; }

            try
            {

                var filepath = System.IO.Path.Combine(filedir, fileguid + ".ondor");
                var filepathEncrypted = System.IO.Path.Combine(filedir, fileguid + ".secureondor");
                //WriteFileEncryptedTo_FileSystem(inputstream, filepathEncrypted, secretpassword);

                //var oldfilepath = System.IO.Path.Combine(filedir, "8af465d2-59ea-469f-a756-f58f14c24572.ondor");
                //var decryptedfile = System.IO.Path.Combine(filedir, originalname);
                //GetFileDecryptedFrom_FileSystem(oldfilepath, decryptedfile, "@bE!NH_ewz#?");


                using (var fileStream = File.Create(filepath))
                {
                    inputstream.CopyTo(fileStream);
                }
            }
            catch (IOException e)
            {
                new DAL.Logger().Log("DAL.Files", string.Format("(WriteFileToFileSystem) - The file could not be stored on path {0} with message {1}", filedir.ToString(), e.Message));
                return false;

            }


            return true;


        }

        public bool CreatePathIfMising(string path)
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

        public BO.File GetFile(int fileid, int customerid)
        {
            // get file info out of database

            var dbfile = GetFile_DB(fileid, customerid);
            // todo: check if the dbfile is there.. could not be found?

            var f = dbfile.versions.FirstOrDefault();

            string OndorUploadPath = new DAL.Settings().GetSetting("OndorFilePath").value;
            string filepath = System.IO.Path.Combine(OndorUploadPath, f.path, f.guid  + ".ondor").ToString();

       

           // f.filestream = GetFileFromFileSystem(filepath);

            return dbfile;

        }

        private BO.File GetFile_DB(int fileid, int customerid)
        {
            var file = new BO.File()
            {
                versions = new List<BO.File.Version>()
            };

            try
            {


                using (SqlConnection connection = new SqlConnection(DAL.Connection.connectionstring))
                {
                    using (SqlCommand cmd = new SqlCommand("File_Get") { CommandType = System.Data.CommandType.StoredProcedure, Connection = connection })
                    {


                        cmd.Parameters.Add(new SqlParameter() { ParameterName = "@fileid", Value = fileid });
                        cmd.Parameters.Add(new SqlParameter() { ParameterName = "@customerid", Value = customerid });


                        connection.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {

                            while (dr.Read())
                            {
                                //id, name, organisationid, personid, customerid, [path], dateupload, [version], [guid], [password]

                                file.name = (string)dr["name"];
                                if (dr["organisationid"]  != System.DBNull.Value  )
                                {
                                    file.organisationid = (int)dr["organisationid"];
                                }

                                if (dr["personid"] != System.DBNull.Value)
                                {
                                    file.personid = (int)dr["personid"];
                                }

                                file.versions.Add(new BO.File.Version()
                                {
                                    id = (int)dr["id"],
                                    guid = ((System.Guid)dr["guid"]).ToString(),
                                    version = (int) dr["version"],
                                    dateuploaded = (DateTime)dr["dateupload"],
                                    path = (string)dr["path"],
                                    password = (string)dr["password"]
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
                new DAL.Logger().Log("DAL.Files", string.Format("(GetFile_DB) - The file with fileid: {0}, customerid {1} could not be found.", fileid,customerid));
                return null;
            }



        }

        //private FileStream GetFileFromFileSystem(string pathSource)
        //{



        //    try
        //    {

        //        //using (FileStream fsSource = new FileStream(pathSource,
        //        //    FileMode.Open, FileAccess.Read))
        //        //{

        //        //    // Read the source file into a byte array. 
        //        //    byte[] bytes = new byte[fsSource.Length];
        //        //    int numBytesToRead = (int)fsSource.Length;
        //        //    int numBytesRead = 0;
        //        //    while (numBytesToRead > 0)
        //        //    {
        //        //        // Read may return anything from 0 to numBytesToRead. 
        //        //        int n = fsSource.Read(bytes, numBytesRead, numBytesToRead);

        //        //        // Break when the end of the file is reached. 
        //        //        if (n == 0) { break; }

        //        //        numBytesRead += n;
        //        //        numBytesToRead -= n;
        //        //    }
        //        //    numBytesToRead = bytes.Length;


        //        using (FileStream fstr = new FileStream(pathSource, FileMode.Open))
        //        {
        //            return fstr;
        //        }


        //        // }
        //    }
        //    catch (FileNotFoundException e)
        //    {
        //        // Console.WriteLine(ioEx.Message);
        //        new DAL.Logger().Log("DAL.Files", string.Format("(GetFileFromFileSystem) - The file could not find the file on path {0} with message {1}", pathSource, e.Message));
        //        return null;
        //    }



        //}


        //private bool WriteFileEncryptedTo_FileSystem(System.IO.Stream inputStream, string outputFile, string password)
        //{

        //    try
        //    {
        //        UnicodeEncoding UE = new UnicodeEncoding();
        //        byte[] key = UE.GetBytes(password);

        //        FileStream fsCrypt = new FileStream(outputFile, FileMode.Create);

        //        RijndaelManaged RMCrypto = new RijndaelManaged();
        //        CryptoStream cs = new CryptoStream(fsCrypt, RMCrypto.CreateEncryptor(key, key), CryptoStreamMode.Write);

        //        int data;
        //        while ((data = inputStream.ReadByte()) != -1)
        //        {
        //            cs.WriteByte((byte)data);
        //            inputStream.Close();
        //            cs.Close();
        //            fsCrypt.Close();
        //        }
        //    }
        //    catch (Exception e)
        //    {

        //        return false;

        //    }

        //    return true;



        //}

        //private System.IO.Stream GetFileDecryptedFrom_FileSystem(string inputFile, string outputFile, string secretpassword)
        //{

        //    UnicodeEncoding UE = new UnicodeEncoding();
        //    byte[] key = UE.GetBytes(secretpassword);

        //    FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);

        //    RijndaelManaged RMCrypto = new RijndaelManaged();

        //    CryptoStream cs = new CryptoStream(fsCrypt,
        //        RMCrypto.CreateDecryptor(key, key),
        //        CryptoStreamMode.Read);

        //    FileStream fsOut = new FileStream(outputFile, FileMode.Create);

        //    int data;
        //    while ((data = cs.ReadByte()) != -1)
        //    {
        //        fsOut.WriteByte((byte)data);
        //    }

        //    fsOut.Close();
        //    cs.Close();
        //    fsCrypt.Close();

        //    return fsOut;
        //}

        //private System.IO.Stream GetFileDecryptedFrom_FileSystem(string inputFile, string outputFile, string secretpassword)
        //{

        //    UnicodeEncoding UE = new UnicodeEncoding();
        //    byte[] key = UE.GetBytes(secretpassword);

        //    FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);

        //    RijndaelManaged RMCrypto = new RijndaelManaged();

        //    CryptoStream cs = new CryptoStream(fsCrypt,
        //        RMCrypto.CreateDecryptor(key, key),
        //        CryptoStreamMode.Read);

        //    FileStream fsOut = new FileStream(outputFile, FileMode.Create);

        //    int data;
        //    while ((data = cs.ReadByte()) != -1)
        //    {
        //        fsOut.WriteByte((byte)data);
        //    }

        //    fsOut.Close();
        //    cs.Close();
        //    fsCrypt.Close();

        //    return fsOut;
        //}



    }
}