using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace LittleCompany.GUI.webmethods
{
    /// <summary>
    /// Summary description for Sys
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Sys : System.Web.Services.WebService
    {

        [WebMethod]
        public BO.Feedback GetSystemData()
        {
            var fb = new BO.Feedback();

            //TODO: get system data

            fb.ispositive = true;
            return fb;
        }
    }
}
