using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LittleCompany.GUI.helpers
{
    /// <summary>
    /// Summary description for functions
    /// </summary>
    public class functions : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {


            context.Response.ContentType = "application/json";
            var r = new BO.Feedback()
            {
                ispositive = false,
                messages = new List<string>()
            };



            string token = "";
            if (String.IsNullOrEmpty(context.Request.QueryString["token"]))
            {
                r.ispositive = false;
                r.messages.Add("U mist de token in de aanvraag");
                context.Response.Write( r);
                return;
            }


            token = context.Request.QueryString["token"];
            var auth = new BL.Security().Authenticate(new BO.SecurityToken() { token = token });

            if (auth == null || auth.customerid < 1 || auth.loginid < 1)
            {
                r.ispositive = false;
                r.messages.Add("U moet opnieuw inloggen");
                context.Response.Write(r);
                return;
            }






            if (string.IsNullOrEmpty(context.Request.QueryString["action"]))
            {

                r.messages.Add("U mist de action in de querystring");
                context.Response.Write(r);
                return;
            }


            string action = context.Request.QueryString["action"].ToString().ToLower();

            switch (action)
            {
                case "UploadFiles":
  
                    context.Response.Write(UploadFiles(context, auth)); return;
                    break;
                default:
                    r.messages.Add("U mist een geldige action in de querystring");
                    context.Response.Write(r); return;
                    break;
            }




        }



        private BO.Feedback UploadFiles(HttpContext context, BO.AuthenticationInfo auth)
        {

            var r = new BO.Feedback()
            {
                data = "",
                ispositive = false,
                messages = new List<string>()
            };


            // todo: 
            // create guid for identification of the file
            // make sure the guid was not used before (loop)
            // save file to disk (in folder of customer_id/fileuploads) filename  is the guid
            // save file to db (with disk path, with guid)
            
           



            if (context.Request.Files.Count > 0)
            {
                HttpFileCollection files = context.Request.Files;
                foreach (string key in files)
                {
                    HttpPostedFile file = files[key];
                    string fileName = file.FileName;
                    fileName = context.Server.MapPath("~/uploads/" + fileName);
                    file.SaveAs(fileName);
                }
            }


            return new BO.Feedback()
            {
                data = "",
                ispositive = true,
                messages = new List<string>()
            };



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