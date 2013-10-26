using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LittleCompany
{
    public partial class _testdb : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string customername  = "test", username = "user1", password = "g1";
            int mainloginid = new BL.CustomerLogic().RegisterNewCustomer(customername,username,password);

            var token  =   new BL.Security().Login(username, password);

            int loginid = new BL.Security().Authenticate(token);

            // test errorlog
            new BL.LogLogic().Log("TEST", "This is a test");

        }
    }
}