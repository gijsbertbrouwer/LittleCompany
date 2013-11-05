using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LittleCompany.GUI.webforms
{
    public partial class dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var auth = new BL.Security().Authenticate((BO.SecurityToken)Session["token"]);

            var user = new BL.User().GetUser(auth);

            var securitytoken = (BO.SecurityToken)Session["token"];
            SecurityToken.Value = securitytoken.token;  //set token to page
        }

    }
}