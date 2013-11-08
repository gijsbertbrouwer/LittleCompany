using System;
using System.Collections.Generic;
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
                default:
                    r.messages.Add("U mist een geldige action in de querystring");
                    
                    break;
            }

           
            context.Response.Write(json.Serialize(r)); 

            return;

        }



        private BO.Feedback UploadFiles(HttpContext context, BO.AuthenticationInfo auth)
        {

            var r = new BO.Feedback()
            {
                data = "",
                ispositive = false,
                messages = new List<string>()
            };

            int succesUploadedFiles = 0;

            if (context.Request.Files.Count > 0)
            {
                HttpFileCollection files = context.Request.Files;
                foreach (string key in files)
                {
                    HttpPostedFile file = files[key];
                    string fileName =  System.IO.Path.GetFileName(file.FileName);

                    var fupload = new BL.Files().CreateNewFileb(file.InputStream, fileName, 0, auth.customerid, 0, DateTime.Now);
                    if (fupload != null)
                    {
                        succesUploadedFiles++;
                    }


                }
            }


            r.ispositive = true;
            r.messages.Add(string.Format("{0} files succesfully uploaded (TODO CAPTION)", succesUploadedFiles));

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