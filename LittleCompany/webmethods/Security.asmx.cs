using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace LittleCompany.webmethods
{
    /// <summary>
    /// Summary description for Security
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class Security : System.Web.Services.WebService
    {

        [WebMethod]
        public BO.Feedback RegisterNewCustomer(string customername, string username, string password)
        {
             var r = new BO.Feedback(){ messages = new List<string>()};
           
            // todo: do basic check on inputs


            int mainloginid = new BL.CustomerLogic().RegisterNewCustomer(customername, username, password);
            if(mainloginid < 1){
                r.messages.Add("there was already someone else using this username or customername."); // TODO: captionize
                return r;
            }

            var token = new BL.Security().Login(username, password);
            if (token == null)
            {
                r.messages.Add("there was a problem logging in."); // TODO: captionize
                return r;
            }

            int loginid = new BL.Security().Authenticate(token);
            if (loginid < 1)
            {
                r.messages.Add("there was a problem logging in. (authentication)"); // TODO: captionize
                return r;
            }

            r.ispositive = true;
            r.data = token;


            return r;
        }
    }
}
