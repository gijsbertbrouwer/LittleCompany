using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace LittleCompany.GUI.webmethods
{
    /// <summary>
    /// Summary description for User
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class User : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public BO.Feedback GetUserData(BO.SecurityToken securityToken)
        {
            var fb = new BO.Feedback();

            var auth = new BL.Security().Authenticate(securityToken);
            if (auth == null)
            {
                fb.messages.Add("geen security. loser!");
                return fb;
            }
            
            var u = new BL.User().GetUser(auth);
            if (u == null)
            {
                fb.messages.Add("geen user.");
                return fb;
            }

            fb.data = u;
            fb.ispositive = true;
            return fb;
            
        }


        [WebMethod]
        public BO.Feedback RemoveFavorite(BO.SecurityToken securityToken)
        {
            var fb = new BO.Feedback();
            return fb;
        }
    }
}
