using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace LittleCompany.GUI.helpers
{
    /// <summary>
    /// Summary description for functions
    /// </summary>
    public class functions : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var r = new BO.Feedback()
            {
                messages = new List<string>()
            };

            context.Response.ContentType = "application/json";
            var json = new JavaScriptSerializer();


            string token = "";
            if (String.IsNullOrEmpty(context.Request.QueryString["token"]))
            {
                r.ispositive = false;
                r.messages.Add("U mist de token in de aanvraag");
                context.Response.Write(json.Serialize(r));
                return;
            }


            token = context.Request.QueryString["token"];
            var auth = new BL.Security().Authenticate(new BO.SecurityToken() { token = token });

            if (auth == null || auth.customerid < 1 || auth.loginid < 1)
            {
                r.ispositive = false;
                r.messages.Add("U moet opnieuw inloggen");
                context.Response.Write(json.Serialize(r));
                return;
            }






            if (string.IsNullOrEmpty(context.Request.QueryString["action"]))
            {

                r.messages.Add("U mist de action in de querystring");
                context.Response.Write(json.Serialize(r));
                return;
            }


            string action = context.Request.QueryString["action"].ToString().ToLower();



            switch (action)
            {
                case "uploadfiles":

                    r = UploadFiles(context, auth);
                    break;
                case "getfile":

                    GetFile(context, auth);
                    return;
                    break;
                default:
                    r.messages.Add("U mist een geldige action in de querystring");

                    break;
            }


            context.Response.Write(json.Serialize(r));

            return;

        }



        private void StreamFile(string filepath, HttpContext context, string filename)
        {


            var Response = context.Response;

            FileStream fs;
            BinaryReader br;
            FileInfo f;

            string showinline = "inline;";


            f = new FileInfo(filepath);
            if (f.Exists)
            {
                fs = new FileStream(filepath, FileMode.Open);

                br = new BinaryReader(fs);
                Byte[] dataBytes = br.ReadBytes((int)(fs.Length - 1));
                if (dataBytes != null)
                {
                  

                    Response.Buffer = true;
                    Response.Clear();
                    Response.ClearContent();
                    Response.ClearHeaders();
                    context.Response.ContentType = "application";
                    Response.AddHeader("content-disposition", showinline + " filename=" + filename);
                    Response.BinaryWrite(dataBytes);
                }

                br.Close();
                fs.Close();
            }
            else
            {

               // ErrorLogic.SaveErrorMessage("Error in download.aspx with filetype: " + filetype + " And fileid: " + fileId + " The file could not be found on the server.");



            }

        }


        private void GetFile(HttpContext context, BO.AuthenticationInfo auth)
        {


            int fileid = 0;

            if (context.Request.QueryString["fileid"] == null)
            {
                // todo errorlogic

            }

            string fileidstr = context.Request.QueryString["fileid"];
            if (!int.TryParse(context.Request.QueryString["fileid"].ToString(), out fileid))
            {
                // todo: errorlogic, no correct fileid
            }


            var file = new BL.Files().GetFile(fileid, auth.customerid);
            var v = file.versions.FirstOrDefault();

            string OndorUploadPath = new DAL.Settings().GetSetting("OndorFilePath").value;
            string filepath = System.IO.Path.Combine(OndorUploadPath, v.path, v.guid + ".ondor").ToString();
            
            
            StreamFile(filepath, context, file.name);



        }





        private BO.Feedback UploadFiles(HttpContext context, BO.AuthenticationInfo auth)
        {

            var r = new BO.Feedback()
            {
                data = "",
                ispositive = false,
                messages = new List<string>()
            };

            var uploadedfils = new List<BO.File>();

            int succesUploadedFiles = 0;

            if (context.Request.Files.Count > 0)
            {
                HttpFileCollection files = context.Request.Files;
                foreach (string key in files)
                {
                    HttpPostedFile file = files[key];
                    string fileName = System.IO.Path.GetFileName(file.FileName);

                    var fupload = new BL.Files().CreateNewFile(file.InputStream, fileName, 0, auth.customerid, 0, DateTime.Now);
                    if (fupload != null)
                    {
                        succesUploadedFiles++; uploadedfils.Add(fupload);
                    }


                }
            }


            r.ispositive = true;
            r.messages.Add(string.Format("{0} files succesfully uploaded (TODO CAPTION)", succesUploadedFiles));
            r.data = uploadedfils;
            return r;

        }



        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}